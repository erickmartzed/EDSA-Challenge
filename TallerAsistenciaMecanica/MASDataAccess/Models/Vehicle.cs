using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public int ModelId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string ChassisNumber { get; set; } = null!;

    public string EngineNumber { get; set; } = null!;

    public int ColorId { get; set; }

    public short ManufacturingYear { get; set; }

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<RenderedService> RenderedServices { get; set; } = new List<RenderedService>();
}