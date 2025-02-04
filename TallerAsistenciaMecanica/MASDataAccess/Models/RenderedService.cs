using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class RenderedService
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int Total { get; set; }

    public DateOnly? Date { get; set; }

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ServiceByRendServ> ServiceByRendServs { get; set; } = new List<ServiceByRendServ>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
