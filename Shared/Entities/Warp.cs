using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarJadeManager.Shared;

public partial class Warp
{
    /// <summary>
    /// Warp Id
    /// </summary>
    public string Id { get; set; } = null!;
    /// <summary>
    /// UID of in gmae user.
    /// </summary>
    public string Uid { get; set; } = null!;

    /// <summary>
    /// ID of banner in which warp was made
    /// </summary>
    public int GachaId { get; set; }

    /// <summary>
    /// Type of banner in which warp was made. 
    /// </summary>
    public int GachaType { get; set; }

    /// <summary>
    /// Id of received item 
    /// </summary>
    public int ItemId { get; set; }

    /// <summary>
    /// Count property? Idk purpose of this, ask hoyo api
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Time of warp
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Name of received item
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Language
    /// </summary>
    public string Lang { get; set; } = null!;

    /// <summary>
    /// Type of received item. Light Cone or Character
    /// </summary>
    public string ItemType { get; set; } = null!;

    /// <summary>
    /// Rarity
    /// </summary>
    public int RankType { get; set; }

    /// <summary>
    /// Is 4-star or above item received through guarantee. WIN, LOSE, GUARANTEE
    /// </summary>
    public string? Guarantee { get; set; }

    public int? Pity { get; set; }

    /// <summary>
    /// EF Core navigation property for item.
    /// </summary>
    public virtual Item Item { get; set; } = null!;
    /// <summary>
    /// EF Core navigation property for banner in which warp was made.
    /// </summary>
    [JsonIgnore]
    public virtual Banner Gacha { get; set; } = null!;

    /// <summary>
    /// EF Core navigation property for BannerInfo of user that did warp.
    /// </summary>
    [JsonIgnore]
    public virtual UserBannerInfo UserBannerInfo { get; set; } = null!;
}
