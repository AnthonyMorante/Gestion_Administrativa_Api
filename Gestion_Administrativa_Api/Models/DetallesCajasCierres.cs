using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DetallesCajasCierres
{
    public int IdDetalleCajaCierre { get; set; }

    public int? IdCaja { get; set; }

    public int? IdDenominacion { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public virtual Cajas? IdCajaNavigation { get; set; }

    public virtual DenominacionesDinero? IdDenominacionNavigation { get; set; }
}
