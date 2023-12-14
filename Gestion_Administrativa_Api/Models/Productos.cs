using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Productos
{
    public Guid IdProducto { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public Guid? IdIva { get; set; }

    public Guid? IdEmpresa { get; set; }

    public bool? ActivoProducto { get; set; }

    public decimal? TotalIva { get; set; }

    public decimal? Cantidad { get; set; }

    public virtual ICollection<DetallePrecioProductos> DetallePrecioProductos { get; set; } = new List<DetallePrecioProductos>();

    public virtual Empresas? IdEmpresaNavigation { get; set; }

    public virtual Ivas? IdIvaNavigation { get; set; }

    public virtual ICollection<Lotes> Lotes { get; set; } = new List<Lotes>();
}
