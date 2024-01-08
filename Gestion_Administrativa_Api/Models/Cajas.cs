using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Cajas
{
    public int IdCaja { get; set; }

    public Guid? IdEmpresa { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaCierre { get; set; }

    public decimal? TotalApertura { get; set; }

    public decimal? TotalCierre { get; set; }

    public bool? Detallado { get; set; }

    public virtual ICollection<DetallesCajas> DetallesCajas { get; set; } = new List<DetallesCajas>();

    public virtual ICollection<DetallesCajasCierres> DetallesCajasCierres { get; set; } = new List<DetallesCajasCierres>();

    public virtual Empresas? IdEmpresaNavigation { get; set; }
}
