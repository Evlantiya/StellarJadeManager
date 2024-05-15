using System;
using System.Collections.Generic;

namespace StellarJadeManager.Server;

public partial class Event
{
    public int Id { get; set; }

    public int PatchId { get; set; }

    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? StellarJadesAmount { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Patch Patch { get; set; } = null!;
}
