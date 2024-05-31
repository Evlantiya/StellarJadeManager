using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class BannerItem
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int BannerId { get; set; }

    [JsonIgnore]
    public virtual Banner Banner { get; set; } = null!;

    [JsonIgnore]
    public virtual Item Item { get; set; } = null!;
}
