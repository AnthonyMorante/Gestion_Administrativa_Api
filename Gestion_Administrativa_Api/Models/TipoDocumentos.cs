using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TipoDocumentos
{
    public Guid IdTipoDocumento { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Codigo { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocumentosEmitir> DocumentosEmitir { get; set; } = new List<DocumentosEmitir>();

    public virtual ICollection<Secuenciales> Secuenciales { get; set; } = new List<Secuenciales>();
}
