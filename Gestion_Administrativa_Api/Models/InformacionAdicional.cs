using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class InformacionAdicional
{
    public Guid IdInformacionAdicional { get; set; }

    public string? Nombre { get; set; }

    public string? Valor { get; set; }

    public Guid IdFactura { get; set; }

    public virtual Facturas IdFacturaNavigation { get; set; } = null!;
}
