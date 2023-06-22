using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DetallePrecioProductos
{
    public Guid IdDetallePrecioProducto { get; set; }

    public decimal? TotalIva { get; set; }

    public decimal? Porcentaje { get; set; }

    public decimal? Total { get; set; }

    public Guid? IdProducto { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }
}
