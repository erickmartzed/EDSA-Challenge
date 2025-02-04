using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
