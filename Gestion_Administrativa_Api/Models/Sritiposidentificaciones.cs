using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriTiposIdentificaciones
{
    public string Codigo { get; set; } = null!;

    public string? TipoIdentificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SriPersonas> SriPersonas { get; set; } = new List<SriPersonas>();
}
