using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Provincias
{
    public Guid IdProvincia { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Ciudades> Ciudades { get; set; } = new List<Ciudades>();
}
