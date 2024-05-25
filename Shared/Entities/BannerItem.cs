using System;
using System.Collections.Generic;

namespace StellarJadeManager.Shared;

public partial class BannerItem
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int BannerId { get; set; }

    public virtual Banner Banner { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
