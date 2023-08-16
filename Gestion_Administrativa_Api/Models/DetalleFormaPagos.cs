using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class DetalleFormaPagos
{
    public Guid IdDetalleFormaPago { get; set; }

    public decimal Plazo { get; set; }

    public decimal Valor { get; set; }

    public Guid IdTiempoFormaPago { get; set; }

    public Guid IdFactura { get; set; }

    public Guid IdFormaPago { get; set; }

    public virtual Facturas IdFacturaNavigation { get; set; } = null!;

    public virtual TiempoFormaPagos IdTiempoFormaPagoNavigation { get; set; } = null!;
}
