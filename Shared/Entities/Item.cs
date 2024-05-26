using System;
using System.Collections.Generic;

namespace StellarJadeManager.Shared;

public partial class Item
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public short Rarity { get; set; }

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();
}
