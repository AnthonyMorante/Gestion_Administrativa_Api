using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DenominacionesDinero
{
    public int IdDenominacion { get; set; }

    public string? Nombre { get; set; }

    public decimal? Valor { get; set; }

    public string? IdTipoDenominacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetallesCajas> DetallesCajas { get; set; } = new List<DetallesCajas>();

    public virtual ICollection<DetallesCajasCierres> DetallesCajasCierres { get; set; } = new List<DetallesCajasCierres>();

    public virtual TiposDenominacionesDinero? IdTipoDenominacionNavigation { get; set; }
}
