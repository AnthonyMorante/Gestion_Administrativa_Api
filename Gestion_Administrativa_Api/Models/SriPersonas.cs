using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriPersonas
{
    public DateTime? FechaRegistro { get; set; }

    public string Identificacion { get; set; } = null!;

    public string? TipoIdentificacion { get; set; }

    public string? RazonSocial { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public bool? Proveedor { get; set; }

    public virtual ICollection<ProductosProveedores> ProductosProveedores { get; set; } = new List<ProductosProveedores>();

    public virtual ICollection<SriProductos> SriProductos { get; set; } = new List<SriProductos>();

    public virtual SriTiposIdentificaciones? TipoIdentificacionNavigation { get; set; }
}
