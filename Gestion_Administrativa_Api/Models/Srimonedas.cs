using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriMonedas
{
    public string Moneda { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<SriFacturas> SriFacturas { get; set; } = new List<SriFacturas>();
}
