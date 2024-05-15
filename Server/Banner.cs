﻿using System;
using System.Collections.Generic;

namespace StellarJadeManager.Server;

public partial class Banner
{
    public int Id { get; set; }

    public int PatchId { get; set; }

    public int CharacterId { get; set; }

    public int TypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Patch Patch { get; set; } = null!;

    public virtual ICollection<Warp> Warps { get; set; } = new List<Warp>();
}
