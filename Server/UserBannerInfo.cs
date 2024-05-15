using System;
using System.Collections.Generic;

namespace StellarJadeManager.Server;

public partial class UserBannerInfo
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public int BannerTypeId { get; set; }

    public int CurrentLegendaryPity { get; set; }

    public int CurrentEpicPity { get; set; }

    public bool GuaranteedLegendary { get; set; }

    public bool GuaranteedEpic { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
