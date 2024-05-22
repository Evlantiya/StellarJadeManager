using System;
using System.Collections.Generic;

namespace StellarJadeManager.Shared;

public partial class BannerItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ItemId { get; set; }

    public string ItemType {get;set;}

    public int BannerId { get; set; }
    public virtual Banner Banner { get; set; } = null!;

    public int RankType { get; set; }
}
