using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Ivas
{
    public Guid IdIva { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public decimal? Valor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? Codigo { get; set; }

    public virtual ICollection<DetalleFacturas> DetalleFacturas { get; set; } = new List<DetalleFacturas>();

    public virtual ICollection<DetallePrecioProductos> DetallePrecioProductos { get; set; } = new List<DetallePrecioProductos>();

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}
