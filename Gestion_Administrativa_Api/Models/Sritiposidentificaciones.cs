using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Sritiposidentificaciones
{
    public string Codigo { get; set; } = null!;

    public string? Tipoidentificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Personas> Personas { get; set; } = new List<Personas>();

    public virtual ICollection<Srifacturas> Srifacturas { get; set; } = new List<Srifacturas>();
}
