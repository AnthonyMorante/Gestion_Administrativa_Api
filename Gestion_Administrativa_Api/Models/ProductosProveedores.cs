using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class ProductosProveedores
{
    public int IdProductoProveedor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public Guid? IdProducto { get; set; }

    public string? CodigoPrincipal { get; set; }

    public string? Identificacion { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }

    public virtual SriPersonas? IdentificacionNavigation { get; set; }
}
