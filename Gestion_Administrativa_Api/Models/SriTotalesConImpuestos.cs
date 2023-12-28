using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriTotalesConImpuestos
{
    public int IdTotalConImpuesto { get; set; }

    public int? IdFactura { get; set; }

    public decimal? BaseImponible { get; set; }

    public string? Codigo { get; set; }

    public string? CodigoPorcentaje { get; set; }

    public decimal? DescuentoAdicional { get; set; }

    public decimal? Valor { get; set; }

    public virtual SriTarifasImpuestos? CodigoPorcentajeNavigation { get; set; }

    public virtual SriFacturas? IdFacturaNavigation { get; set; }
}
