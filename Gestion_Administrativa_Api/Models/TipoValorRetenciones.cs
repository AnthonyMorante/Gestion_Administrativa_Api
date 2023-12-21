using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TipoValorRetenciones
{
    public Guid IdTipoValorRetencion { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? Codigo { get; set; }

    public virtual ICollection<ImpuestoRetenciones> ImpuestoRetenciones { get; set; } = new List<ImpuestoRetenciones>();

    public virtual ICollection<PorcentajeImpuestosRetenciones> PorcentajeImpuestosRetenciones { get; set; } = new List<PorcentajeImpuestosRetenciones>();
}
