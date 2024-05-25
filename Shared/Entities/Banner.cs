﻿using System;
using System.Collections.Generic;

namespace StellarJadeManager.Shared;

public partial class Banner
{
    public int Id { get; set; }

    public int PatchId { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<BannerItem> BannerItems { get; set; } = new List<BannerItem>();

    public virtual Patch Patch { get; set; } = null!;

    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();
}
