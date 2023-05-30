using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TipoIdentificaciones
{
    public Guid IdTipoIdentificacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public int? Codigo { get; set; }

    public virtual ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();

    public virtual ICollection<Empleados> Empleados { get; set; } = new List<Empleados>();

    public virtual ICollection<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
}
