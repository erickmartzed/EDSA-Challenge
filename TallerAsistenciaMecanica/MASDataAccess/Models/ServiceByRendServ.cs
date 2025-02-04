using System;
using System.Collections.Generic;

namespace MASDataAccess.Models;

public partial class ServiceByRendServ
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int RenderedServiceId { get; set; }

    public int Price { get; set; }

    public string Annotation { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public bool Valid { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual RenderedService RenderedService { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
