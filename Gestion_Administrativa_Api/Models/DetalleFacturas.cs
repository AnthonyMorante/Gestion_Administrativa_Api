using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DetalleFacturas
{
    public Guid IdDetalleFactura { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Precio { get; set; }

    public Guid IdIva { get; set; }

    public Guid IdProducto { get; set; }

    public decimal? Porcentaje { get; set; }

    public decimal? ValorPorcentaje { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public Guid IdFactura { get; set; }

    public virtual Facturas IdFacturaNavigation { get; set; } = null!;

    public virtual Ivas IdIvaNavigation { get; set; } = null!;

    public virtual Productos IdProductoNavigation { get; set; } = null!;
}
