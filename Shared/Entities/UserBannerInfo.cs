using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class UserBannerInfo
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public int BannerTypeId { get; set; }

    public int CurrentLegendaryPity { get; set; }

    public int CurrentEpicPity { get; set; }

    public bool GuaranteedLegendary { get; set; }

    public bool GuaranteedEpic { get; set; }

    public string Uid { get; set; } = null!;
    
    [JsonIgnore]
    public virtual Profile Profile { get; set; } = null!;

    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();


    public static List<UserBannerInfo> CreateDefaultBannerInfos(){
        return new List<UserBannerInfo> 
        {
            new UserBannerInfo(){ BannerTypeId=(int)BannerTypeEnum.EVENT },
            new UserBannerInfo(){ BannerTypeId=(int)BannerTypeEnum.LIGHTCONE },
            new UserBannerInfo(){ BannerTypeId=(int)BannerTypeEnum.STANDART },
            new UserBannerInfo(){ BannerTypeId=(int)BannerTypeEnum.DEPARTURE }
        };
    }

    public double CalculateWinrate(int rarity){
        if(rarity != 4 && rarity != 5){
            return 0;
        }
        var hren = Warps.Where(w=>w.RankType == rarity && (w.Guarantee == GuaranteeType.Win || w.Guarantee==GuaranteeType.Loss) ).ToList();
        if(hren.Count == 0){
            return 0;
        }
        return hren.Count(w=>w.Guarantee==GuaranteeType.Win)*100.0/ hren.Count;
    }

    public double CalculateMedianPity(int rarity){
        if(rarity != 4 && rarity != 5){
            return 0;
        }
        var warpPities = Warps.Where(w=>w.RankType == rarity).Select(w=>w.Pity).ToList();
        if(warpPities.Count == 0){
            return 0;
        }
        warpPities.Sort();
        if(warpPities.Count % 2 == 0){
            return (double)((warpPities[warpPities.Count/2 - 1] + warpPities[warpPities.Count/2]) / 2.0);
        }
        return (double)warpPities[warpPities.Count/2];
    }
}
