using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class Item
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public short Rarity { get; set; }

    [JsonIgnore]
    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();
    [JsonIgnore]
    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();
}
