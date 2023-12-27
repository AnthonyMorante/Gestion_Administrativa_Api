using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriCamposAdicionales
{
    public int IdCampoAdicional { get; set; }

    public int? IdFactura { get; set; }

    public string? Nombre { get; set; }

    public string? Text { get; set; }

    public virtual SriFacturas? IdFacturaNavigation { get; set; }
}
