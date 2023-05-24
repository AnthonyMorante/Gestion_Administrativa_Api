using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Ciudades
{
    public Guid IdCiudad { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public Guid IdProvincia { get; set; }

    public virtual ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();

    public virtual ICollection<Empleados> Empleados { get; set; } = new List<Empleados>();

    public virtual Provincias IdProvinciaNavigation { get; set; } = null!;

    public virtual ICollection<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
}
