using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class PorcentajeImpuestosRetenciones
{
    public Guid IdPorcentajeImpuestoRetencion { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public decimal? Valor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? Codigo { get; set; }

    public Guid IdTipoValorRetencion { get; set; }

    public virtual TipoValorRetenciones IdTipoValorRetencionNavigation { get; set; } = null!;

    public virtual ICollection<ImpuestoRetenciones> ImpuestoRetenciones { get; set; } = new List<ImpuestoRetenciones>();
}
