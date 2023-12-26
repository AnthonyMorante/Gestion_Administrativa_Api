using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Sriambientes
{
    public string Codigo { get; set; } = null!;

    public string? Ambiente { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Srifacturas> Srifacturas { get; set; } = new List<Srifacturas>();
}
