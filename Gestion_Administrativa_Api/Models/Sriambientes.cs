using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriAmbientes
{
    public string Codigo { get; set; } = null!;

    public string? Ambiente { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SriFacturas> SriFacturas { get; set; } = new List<SriFacturas>();
}
