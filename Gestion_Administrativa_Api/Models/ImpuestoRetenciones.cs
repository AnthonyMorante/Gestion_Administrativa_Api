using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class ImpuestoRetenciones
{
    public Guid IdImpuestoRetencion { get; set; }

    public decimal? BaseImponible { get; set; }

    public decimal? PorcentajeRetener { get; set; }

    public decimal? ValorRetenido { get; set; }

    public decimal? CodDocSustento { get; set; }

    public string? NumDocSustento { get; set; }

    public string? FechaEmisionDocSustento { get; set; }

    public Guid IdTipoValorRetencion { get; set; }

    public Guid IdPorcentajeImpuestoRetencion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public Guid IdRetencion { get; set; }

    public virtual PorcentajeImpuestosRetenciones IdPorcentajeImpuestoRetencionNavigation { get; set; } = null!;

    public virtual Retenciones IdRetencionNavigation { get; set; } = null!;

    public virtual TipoValorRetenciones IdTipoValorRetencionNavigation { get; set; } = null!;
}
