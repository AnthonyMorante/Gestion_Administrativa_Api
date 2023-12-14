using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Facturas
{
    public Guid IdFactura { get; set; }

    public bool? AgenteRetencion { get; set; }

    public int? Ambiente { get; set; }

    public string? ClaveAcceso { get; set; }

    public int? CodigoDocModificado { get; set; }

    public bool? ContribuyenteRimpe { get; set; }

    public string? ContribuyenteEspecial { get; set; }

    public string? DireccionEstablecimiento { get; set; }

    public string? DireccionMatriz { get; set; }

    public string? EmisorRazonSocial { get; set; }

    public string? EmisorRuc { get; set; }

    public int? Establecimiento { get; set; }

    public int? PuntoEmision { get; set; }

    public int? Secuencial { get; set; }

    public DateTime? FechaAutorizacion { get; set; }

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public decimal? ExentoIva { get; set; }

    public decimal? Ice { get; set; }

    public decimal? Irbpnr { get; set; }

    public decimal? Isd { get; set; }

    public decimal? Iva12 { get; set; }

    public decimal? Subtotal0 { get; set; }

    public string? Moneda { get; set; }

    public bool? ObligadoContabilidad { get; set; }

    public string? ReceptorRazonSocial { get; set; }

    public string? ReceptorRuc { get; set; }

    public int? ReceptorTipoIdentificacion { get; set; }

    public bool? RegimenMicroempresas { get; set; }

    public int? TipoDocumento { get; set; }

    public int? TipoEmision { get; set; }

    public decimal? TotalDescuento { get; set; }

    public decimal? TotalImporte { get; set; }

    public decimal? TotalSinImpuesto { get; set; }

    public string? VersionXml { get; set; }

    public int IdTipoEstadoDocumento { get; set; }

    public Guid IdUsuario { get; set; }

    public string? ReceptorTelefono { get; set; }

    public string? ReceptorCorreo { get; set; }

    public string? ReceptorDireccion { get; set; }

    public Guid IdCliente { get; set; }

    public Guid IdCiudad { get; set; }

    public Guid IdDocumentoEmitir { get; set; }

    public Guid IdEstablecimiento { get; set; }

    public Guid IdPuntoEmision { get; set; }

    public decimal? Subtotal12 { get; set; }

    public bool? RegimenRimpe { get; set; }

    public string? Ruta { get; set; }

    public string? ResolucionAgenteRetencion { get; set; }

    public decimal? Saldo { get; set; }

    public decimal? ValorRecibido { get; set; }

    public decimal? Cambio { get; set; }

    public int? IdTipoEstadoSri { get; set; }

    public string? Mensaje { get; set; }

    public bool? CorreoEnviado { get; set; }

    public virtual ICollection<DetalleFacturas> DetalleFacturas { get; set; } = new List<DetalleFacturas>();

    public virtual ICollection<DetalleFormaPagos> DetalleFormaPagos { get; set; } = new List<DetalleFormaPagos>();

    public virtual TipoEstadoDocumentos IdTipoEstadoDocumentoNavigation { get; set; } = null!;

    public virtual TipoEstadoSri? IdTipoEstadoSriNavigation { get; set; }

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<InformacionAdicional> InformacionAdicional { get; set; } = new List<InformacionAdicional>();
}
