using System;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using StellarJadeManager.Server;
using StellarJadeManager.Shared;

public class WarpService : IWarpService
{
    private HttpClient _httpClient;
    private readonly PostgresContext _db;
    private readonly AutoMapper.IMapper _mapper;


    public WarpService(HttpClient httpClient, AutoMapper.IMapper mapper, PostgresContext db)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _db = db;
    }

    //God forgive me. Refactor this bullshit. I wanna KILL MYSELF :) ASYNC FOR ALL BANNERS
    public async Task<List<UserBannerInfo>> GetWarpHistoryAsync(string warpUrl, Profile? profile = null)
    {
        CheckUrl(warpUrl);

        var bannerInfos = GetProfileBannerInfos(profile);

        foreach (var info in bannerInfos)
        {
            bool isDefaultBanner = 
                info.BannerTypeId == (int)BannerTypeEnum.STANDART ||
                info.BannerTypeId == (int)BannerTypeEnum.DEPARTURE;

            GetGachaLogResponse? warpPage;
            var lastWarpId = info.Warps.LastOrDefault()?.Id ?? "0";
            var warpDTOList = new List<WarpDTO>();
            var endId = "0";
            do
            {
                var query = $"&page=1&size=20&gacha_type={info.BannerTypeId}&end_id={endId}";
                var hren = warpUrl + query;
                var test = await _httpClient.GetStringAsync(hren);
                warpPage = JsonConvert.DeserializeObject<GetGachaLogResponse>(test);

                // warpPage = await _httpClient.GetFromJsonAsync<GetGachaLogResponse>(hren);
                List<WarpDTO> warpsDTO = warpPage?.data?.list ?? new List<WarpDTO>();

                if (warpsDTO.FindLastIndex(w => w.uid == lastWarpId) != -1)
                {
                    var index = warpsDTO.FindLastIndex(w => w.uid == lastWarpId);
                    warpsDTO.RemoveRange(index, warpsDTO.Count - index);
                    warpDTOList.AddRange(warpsDTO);
                    break;
                }

                warpDTOList.AddRange(warpsDTO);
                endId = warpsDTO.LastOrDefault()?.id ?? "0";
            }
            while (warpPage != null && warpPage.data.list.Count > 0);


            var warps = _mapper.Map<List<WarpDTO>, List<Warp>>(warpDTOList);
            warps.Reverse();

            var epicPity = info?.CurrentEpicPity ?? 0;
            var isEpicGuaranteed = info?.GuaranteedEpic ?? false;

            var legendaryPity = info?.CurrentLegendaryPity ?? 0;
            var isLegendaryGuaranteed = info?.GuaranteedLegendary ?? false;

            foreach (var warp in warps)
            {
                epicPity += 1;
                legendaryPity += 1;
                if (warp.RankType == 4 || warp.RankType == 5)
                {

                    warp.Item = await _db.Items.FindAsync(warp.ItemId);
                    warp.Gacha = await _db.Banners.Include(g => g.BannerItems).FirstOrDefaultAsync(gacha => gacha.Id == warp.GachaId);

                    if(warp.RankType == 4){
                        if(!isDefaultBanner){
                            SetGuaranteeType(ref isEpicGuaranteed, warp);
                        }
                        warp.Pity = epicPity;
                        epicPity = 0;
                    }
                    else if(warp.RankType == 5){
                        if(!isDefaultBanner){
                            SetGuaranteeType(ref isLegendaryGuaranteed, warp);
                        }
                        warp.Pity = legendaryPity;
                        legendaryPity = 0;
                    }
                }
            }

            info.CurrentEpicPity = epicPity;
            info.CurrentLegendaryPity = legendaryPity;

            info.GuaranteedEpic = isEpicGuaranteed;
            info.GuaranteedLegendary = isLegendaryGuaranteed;

            info.Warps.AddRange(warps);
            
            info.Profile = profile;
            //xd
            if(warps.Count > 0){
                info.Uid = warps.First().Uid;
                if( profile != null ){
                    if(profile.UserBannerInfos.Count == 0){
                        profile.UserBannerInfos = bannerInfos;
                        await _db.UserBannerInfos.AddRangeAsync(bannerInfos);
                    }
                    await _db.Warps.AddRangeAsync(warps);
                    await _db.SaveChangesAsync();

                }
            }
        }
        return bannerInfos.ToList();

    }

    private void SetGuaranteeType(ref bool isGuaranteed, Warp? warp)
    {
        if (!IsWarpItemIsRateUp(warp))
        {
            warp.Guarantee = GuaranteeType.Loss;
            isGuaranteed = true;
        }
        else if (isGuaranteed)
        {
            warp.Guarantee = GuaranteeType.Guarantee;
            isGuaranteed = false;
        }
        else
        {
            warp.Guarantee = GuaranteeType.Win;
        }
    }

    private ICollection<UserBannerInfo> GetProfileBannerInfos(Profile? profile)
    {
        ICollection<UserBannerInfo> bannerInfos;
        if (profile == null || profile.UserBannerInfos == null || profile.UserBannerInfos.Count == 0)
        {
            bannerInfos = new List<UserBannerInfo>()
        {
            new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=1 },
            new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=2 },
            new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=11 },
            new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=12 }
        };
        }
        else
        {
            bannerInfos = profile.UserBannerInfos;
        }

        return bannerInfos;
    }


    private bool IsWarpItemIsRateUp(Warp warp){
        return warp.Gacha.BannerItems.Any(i=> i.ItemId == warp.ItemId);
    }

    private void CheckUrl(string warpUrl)
    {
        if (string.IsNullOrEmpty(warpUrl))
        {
            throw new ArgumentException("Empty warp url");
        }
        var warpUri = new Uri(warpUrl);

        if (warpUri.Host != "api-os-takumi.mihoyo.com")
        {
            throw new ArgumentException("Wrong warp url domain. Current version supports api-os-takumi.mihoyo.com");
        }
    }
}
