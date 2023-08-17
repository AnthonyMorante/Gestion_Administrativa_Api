using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class InformacionFirmas
{
    public Guid IdInformacionFirma { get; set; }

    public string? RazonSocial { get; set; }

    public string? Identificacion { get; set; }

    public bool? Activo { get; set; }

    public string? Ruta { get; set; }

    public string? Codigo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Empresas> Empresas { get; set; } = new List<Empresas>();
}
