using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriFormasPagos
{
    public string Codigo { get; set; } = null!;

    public string? FormaPago { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SriPagos> SriPagos { get; set; } = new List<SriPagos>();
}
