using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriPrecios
{
    public int IdPrecio { get; set; }

    public int? IdProducto { get; set; }

    public decimal? BaseImponible { get; set; }

    public string? Codigo { get; set; }

    public decimal? Valor { get; set; }

    public decimal? Tarifa { get; set; }

    public decimal? TotalConImpuestos { get; set; }

    public bool? Activo { get; set; }

    public virtual SriTarifasImpuestos? CodigoNavigation { get; set; }

    public virtual SriProductos? IdProductoNavigation { get; set; }
}
