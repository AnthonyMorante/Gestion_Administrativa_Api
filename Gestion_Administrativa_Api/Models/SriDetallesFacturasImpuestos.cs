using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriDetallesFacturasImpuestos
{
    public int IdDetalleFacturaImpuesto { get; set; }

    public int? IdDetalleFactura { get; set; }

    public decimal? BaseImponible { get; set; }

    public string? Codigo { get; set; }

    public string? CodigoPorcentaje { get; set; }

    public decimal? Tarifa { get; set; }

    public decimal? Valor { get; set; }

    public virtual SriTarifasImpuestos? CodigoPorcentajeNavigation { get; set; }

    public virtual SriDetallesFacturas? IdDetalleFacturaNavigation { get; set; }
}
