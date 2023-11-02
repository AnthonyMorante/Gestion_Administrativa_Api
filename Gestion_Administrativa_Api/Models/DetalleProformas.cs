using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DetalleProformas
{
    public Guid IdDetalleProforma { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Precio { get; set; }

    public Guid IdIva { get; set; }

    public Guid IdProducto { get; set; }

    public decimal? Porcentaje { get; set; }

    public decimal? ValorPorcentaje { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public Guid IdProforma { get; set; }

    public virtual Proformas IdProformaNavigation { get; set; } = null!;
}
