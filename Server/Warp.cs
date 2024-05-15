﻿using System;
using System.Collections.Generic;

namespace StellarJadeManager.Server;

public partial class Warp
{
    public int Id { get; set; }

    public int Uid { get; set; }

    public int GachaId { get; set; }

    public int GachaType { get; set; }

    public int ItemId { get; set; }

    public int Count { get; set; }

    public DateTime Time { get; set; }

    public string Name { get; set; } = null!;

    public string Lang { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public int RankType { get; set; }

    public bool? IsGuaranteed { get; set; }

    public virtual Banner Gacha { get; set; } = null!;

    public virtual Profile UidNavigation { get; set; } = null!;
}
