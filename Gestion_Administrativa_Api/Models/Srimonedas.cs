using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Srimonedas
{
    public string Moneda { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Srifacturas> Srifacturas { get; set; } = new List<Srifacturas>();
}
