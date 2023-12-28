using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriTarifasImpuestos
{
    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public decimal? Tarifa { get; set; }

    public decimal? Valor { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SriDetallesFacturasImpuestos> SriDetallesFacturasImpuestos { get; set; } = new List<SriDetallesFacturasImpuestos>();

    public virtual ICollection<SriPrecios> SriPrecios { get; set; } = new List<SriPrecios>();

    public virtual ICollection<SriTotalesConImpuestos> SriTotalesConImpuestos { get; set; } = new List<SriTotalesConImpuestos>();
}
