using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class Color
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
