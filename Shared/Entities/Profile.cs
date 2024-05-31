using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class Profile
{
    public int Id { get; set; }

    public string? ProfileName { get; set; }

    public int UserId { get; set; }

    public int CurrentJades { get; set; }

    public DateTime? SelectedDate { get; set; }

    public int MoCStars { get; set; }

    public int PfStars { get; set; }

    public bool SupplyPass { get; set; }

    public string? Uid { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserBannerInfo> UserBannerInfos { get; set; } = new List<UserBannerInfo>();
}
