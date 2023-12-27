using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriUnidadesTiempos
{
    public string UnidadTiempo { get; set; } = null!;

    public string? Codigo { get; set; }

    public virtual ICollection<SriPagos> SriPagos { get; set; } = new List<SriPagos>();
}
