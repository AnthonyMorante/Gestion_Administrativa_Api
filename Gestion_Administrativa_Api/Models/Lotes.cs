using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Lotes
{
    public int IdLote { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public decimal? Cantidad { get; set; }

    public Guid? IdProducto { get; set; }

    public Guid? IdUsuario { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }

    public virtual Usuarios? IdUsuarioNavigation { get; set; }
}
