using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class Banner
{
    public int Id { get; set; }

    public int PatchId { get; set; }

    public int TypeId { get; set; }
    [JsonIgnore]
    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

    [JsonIgnore]
    public virtual Patch Patch { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();
}
