using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriProductos
{
    public int IdProducto { get; set; }

    public Guid? IdEmpresa { get; set; }

    public string? Identificacion { get; set; }

    public string? CodigoPrincipal { get; set; }

    public string? Producto { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? Stock { get; set; }

    public bool? Activo { get; set; }

    public bool? Disponible { get; set; }

    public virtual Empresas? IdEmpresaNavigation { get; set; }

    public virtual SriPersonas? IdentificacionNavigation { get; set; }

    public virtual ICollection<SriDetallesFacturas> SriDetallesFacturas { get; set; } = new List<SriDetallesFacturas>();

    public virtual ICollection<SriPrecios> SriPrecios { get; set; } = new List<SriPrecios>();
}
