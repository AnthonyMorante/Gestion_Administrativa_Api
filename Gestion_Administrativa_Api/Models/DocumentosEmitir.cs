using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DocumentosEmitir
{
    public Guid IdDocumentoEmitir { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public Guid? IdTipoDocumento { get; set; }

    public virtual TipoDocumentos? IdTipoDocumentoNavigation { get; set; }

    public virtual ICollection<Retenciones> Retenciones { get; set; } = new List<Retenciones>();
}
