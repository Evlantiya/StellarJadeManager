using System;
using System.Web;
using Newtonsoft.Json;
using StellarJadeManager.Shared;

public class WarpService : IWarpService
{
    private HttpClient _httpClient;
    private readonly AutoMapper.IMapper _mapper;

    public WarpService(HttpClient httpClient, AutoMapper.IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<List<Warp>> GetWarpHistoryAsync(string warpUrl, Profile? profile = null){

        if (string.IsNullOrEmpty(warpUrl)){
            throw new ArgumentException("Empty warp url");
        }
        var warpUri = new Uri(warpUrl);

        if (warpUri.Host!="api-os-takumi.mihoyo.com"){
            throw new ArgumentException("Wrong warp url domain. Current version supports api-os-takumi.mihoyo.com");
        }

        ICollection<UserBannerInfo> bannerInfos = null;

        if(profile == null || profile.UserBannerInfos == null || profile.UserBannerInfos.Count != 4){
            bannerInfos = new List<UserBannerInfo>()
            {
                new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=1 },
                new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=2 },
                new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=11 },
                new UserBannerInfo(){ ProfileId = profile?.Id ?? 0 ,BannerTypeId=12 }
            };
        }
        else{
            bannerInfos = profile.UserBannerInfos;
        }

        GetGachaLogResponse? warpPage;
        foreach(var info in bannerInfos )
        {
            var warpDTOList = new List<WarpDTO>();
            var endId = "0";
            do{
                var query = $"&page=1&size=20&gacha_type={info.BannerTypeId}&end_id={endId}";
                var hren = warpUrl + query;
                var test = await _httpClient.GetStringAsync(hren);
                warpPage = JsonConvert.DeserializeObject<GetGachaLogResponse>(test);

                // warpPage = await _httpClient.GetFromJsonAsync<GetGachaLogResponse>(hren);
                List<WarpDTO> warpsDTO = warpPage?.data?.list ?? new List<WarpDTO>();
                warpDTOList.AddRange(warpsDTO);
                endId = warpsDTO.LastOrDefault()?.id ?? "0";
            } 
            while (warpPage != null && warpPage.data.list.Count > 0);

            var warps = _mapper.Map< List<WarpDTO>, List<Warp> >(warpDTOList);

        }

        //  =  profile?.UserBannerInfos ?? new List<UserBannerInfo>()



        // GetGachaLogResponse? warpPage;
        // var warpsData = new List<WarpDTO>();
        // foreach (int banner in Enum.GetValues(typeof(BannerTypeEnum)))
        // {
        //     var endId = "0";
        //     do{
        //         var query = $"&page=1&size=20&gacha_type=11&end_id={endId}";
        //         var hren = warpUrl + query;
        //         var test = await _httpClient.GetStringAsync(hren);
        //         warpPage = JsonConvert.DeserializeObject<GetGachaLogResponse>(test);

        //         // warpPage = await _httpClient.GetFromJsonAsync<GetGachaLogResponse>(hren);
        //         List<WarpDTO> warps = warpPage?.data?.list ?? new List<WarpDTO>();
        //         warpsData.AddRange(warps);
        //         endId = warps.LastOrDefault()?.id ?? "0";
        //     } 
        //     while (warpPage != null && warpPage.data.list.Count > 0);
        // }
        // string jsonString = JsonConvert.SerializeObject(warpsData, Formatting.Indented);
        // File.WriteAllText($"Data/Warps/warps_{warpsData?.FirstOrDefault()?.uid ?? "0"}", jsonString);
        return null;
    }
}
