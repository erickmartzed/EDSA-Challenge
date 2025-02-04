using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ServiceByRendServ> ServiceByRendServs { get; set; } = new List<ServiceByRendServ>();
}
