﻿using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Retenciones
{
    public Guid IdRetencion { get; set; }

    public int? Ambiente { get; set; }

    public int? TipoEmision { get; set; }

    public string? EmisorRazonSocial { get; set; }

    public string? NombreComercial { get; set; }

    public string? EmisorRuc { get; set; }

    public string? ClaveAcceso { get; set; }

    public int? CodigoDocumento { get; set; }

    public int? Establecimiento { get; set; }

    public int? PuntoEmision { get; set; }

    public int? Secuencial { get; set; }

    public string? DireccionMatriz { get; set; }

    public string? AgenteRetencion { get; set; }

    public DateTime? FechaEmision { get; set; }

    public bool? ObligadoContabilidad { get; set; }

    public string? TipoIdentificacionSujetoRetenido { get; set; }

    public string? RazonSocialSujetoRetenido { get; set; }

    public string? IdentificacionSujetoRetenido { get; set; }

    public string? PeriodoFiscal { get; set; }

    public int? TipoDocumento { get; set; }

    public string? VersionXml { get; set; }

    public int IdTipoEstadoDocumento { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdCliente { get; set; }

    public Guid IdCiudad { get; set; }

    public Guid IdDocumentoEmitir { get; set; }

    public Guid IdEstablecimiento { get; set; }

    public Guid IdPuntoEmision { get; set; }

    public string? Ruta { get; set; }

    public int? IdTipoEstadoSri { get; set; }

    public Guid IdTipoDocumento { get; set; }

    public Guid IdEmpresa { get; set; }

    public Guid IdTipoIdenticacion { get; set; }

    public virtual Ciudades IdCiudadNavigation { get; set; } = null!;

    public virtual Clientes IdClienteNavigation { get; set; } = null!;

    public virtual DocumentosEmitir IdDocumentoEmitirNavigation { get; set; } = null!;

    public virtual Establecimientos IdEstablecimientoNavigation { get; set; } = null!;

    public virtual PuntoEmisiones IdPuntoEmisionNavigation { get; set; } = null!;

    public virtual TipoEstadoDocumentos IdTipoEstadoDocumentoNavigation { get; set; } = null!;

    public virtual TipoEstadoSri? IdTipoEstadoSriNavigation { get; set; }

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ImpuestoRetenciones> ImpuestoRetenciones { get; set; } = new List<ImpuestoRetenciones>();

    public virtual ICollection<InformacionAdicionalRetencion> InformacionAdicionalRetencion { get; set; } = new List<InformacionAdicionalRetencion>();
}
