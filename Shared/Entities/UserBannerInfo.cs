using System;
using System.Collections.Generic;

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
}
