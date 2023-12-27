using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriDetallesFacturas
{
    public int IdDetalleFactura { get; set; }

    public int? IdFactura { get; set; }

    public decimal? Cantidad { get; set; }

    public string? CodigoPrincipal { get; set; }

    public int? IdProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? PrecioTotalSinImpuesto { get; set; }

    public decimal? PrecioTotalConImpuesto { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual SriFacturas? IdFacturaNavigation { get; set; }

    public virtual SriProductos? IdProductoNavigation { get; set; }

    public virtual ICollection<SriDetallesFacturasImpuestos> SriDetallesFacturasImpuestos { get; set; } = new List<SriDetallesFacturasImpuestos>();
}
