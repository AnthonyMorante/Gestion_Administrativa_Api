using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Personas
{
    public DateTime? Fecharegistro { get; set; }

    public string Identificacion { get; set; } = null!;

    public string? Tipoidentificacion { get; set; }

    public string? Razonsocial { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Srifacturas> Srifacturas { get; set; } = new List<Srifacturas>();

    public virtual Sritiposidentificaciones? TipoidentificacionNavigation { get; set; }
}
