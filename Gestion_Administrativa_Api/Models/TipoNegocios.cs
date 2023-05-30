using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TipoNegocios
{
    public Guid IdTipoNegocio { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public string? Codigo { get; set; }

    public virtual ICollection<Empresas> Empresas { get; set; } = new List<Empresas>();
}
