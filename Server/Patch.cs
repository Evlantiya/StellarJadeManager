using System;
using System.Collections.Generic;

namespace StellarJadeManager.Server;

public partial class Patch
{
    public int Id { get; set; }

    public string? Version { get; set; }

    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? WeeksCount { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Banner> Banners { get; set; } = new List<Banner>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
