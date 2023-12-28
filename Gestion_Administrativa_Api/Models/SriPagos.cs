using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriPagos
{
    public int IdPago { get; set; }

    public int? IdFactura { get; set; }

    public string? FormaPago { get; set; }

    public int? Plazo { get; set; }

    public string? UnidadTiempo { get; set; }

    public decimal? Total { get; set; }

    public virtual SriFormasPagos? FormaPagoNavigation { get; set; }

    public virtual SriFacturas? IdFacturaNavigation { get; set; }

    public virtual SriUnidadesTiempos? UnidadTiempoNavigation { get; set; }
}
