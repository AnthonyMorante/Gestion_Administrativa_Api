using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Proformas
{
    public Guid IdProforma { get; set; }

    public int? Ambiente { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Moneda { get; set; }

    public string? ReceptorRazonSocial { get; set; }

    public string? ReceptorRuc { get; set; }

    public int? ReceptorTipoIdentificacion { get; set; }

    public string? ReceptorTelefono { get; set; }

    public string? ReceptorCorreo { get; set; }

    public string? ReceptorDireccion { get; set; }

    public decimal? TotalDescuento { get; set; }

    public decimal? TotalImporte { get; set; }

    public decimal? TotalSinImpuesto { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdEstablecimiento { get; set; }

    public Guid IdPuntoEmision { get; set; }

    public decimal? Subtotal12 { get; set; }

    public int? Establecimiento { get; set; }

    public int? PuntoEmision { get; set; }

    public int? Secuencial { get; set; }

    public virtual ICollection<DetalleProformas> DetalleProformas { get; set; } = new List<DetalleProformas>();

    public virtual Establecimientos IdEstablecimientoNavigation { get; set; } = null!;

    public virtual PuntoEmisiones IdPuntoEmisionNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
