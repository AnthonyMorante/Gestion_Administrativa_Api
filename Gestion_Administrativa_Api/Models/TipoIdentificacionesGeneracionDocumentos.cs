using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TipoIdentificacionesGeneracionDocumentos
{
    public Guid IdTipoIdentificacionesGeneracionDocumentos { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public int? Codigo { get; set; }
}
