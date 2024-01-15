using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriFacturas
{
    public int IdFactura { get; set; }

    public string? Id { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateOnly? FechaEmision { get; set; }

    public bool? Compra { get; set; }

    public DateTime? FechaAutorizacion { get; set; }

    public int? CodigoEstado { get; set; }

    public string? ClaveAcceso { get; set; }

    public Guid? IdEmpresa { get; set; }

    public Guid? IdUsuario { get; set; }

    public string? CodDoc { get; set; }

    public string? ObligadoContabilidad { get; set; }

    public string? Ambiente { get; set; }

    public string? DirMatriz { get; set; }

    public string? Estab { get; set; }

    public string? NombreComercial { get; set; }

    public string? PtoEmi { get; set; }

    public string? RazonSocial { get; set; }

    public string? Ruc { get; set; }

    public string? Secuencial { get; set; }

    public string? Version { get; set; }

    public string? ContribuyenteEspecial { get; set; }

    public string? DirEstablecimiento { get; set; }

    public string? IdentificacionComprador { get; set; }

    public string? RazonSocialComprador { get; set; }

    public string? TipoIdentificacionComprador { get; set; }

    public string? Moneda { get; set; }

    public decimal? Propina { get; set; }

    public decimal? TotalDescuento { get; set; }

    public decimal? TotalSinImpuesto { get; set; }

    public decimal? ImporteTotal { get; set; }

    public bool? RetencionGenerada { get; set; }

    public virtual SriAmbientes? AmbienteNavigation { get; set; }

    public virtual SriTiposDocumentos? CodDocNavigation { get; set; }

    public virtual SriMonedas? MonedaNavigation { get; set; }

    public virtual ICollection<Retenciones> Retenciones { get; set; } = new List<Retenciones>();

    public virtual ICollection<SriCamposAdicionales> SriCamposAdicionales { get; set; } = new List<SriCamposAdicionales>();

    public virtual ICollection<SriDetallesFacturas> SriDetallesFacturas { get; set; } = new List<SriDetallesFacturas>();

    public virtual ICollection<SriPagos> SriPagos { get; set; } = new List<SriPagos>();

    public virtual ICollection<SriTotalesConImpuestos> SriTotalesConImpuestos { get; set; } = new List<SriTotalesConImpuestos>();
}
