using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class Model
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
