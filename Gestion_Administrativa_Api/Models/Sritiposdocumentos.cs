using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Sritiposdocumentos
{
    public string Coddoc { get; set; } = null!;

    public string? Tipodocumento { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Srifacturas> Srifacturas { get; set; } = new List<Srifacturas>();
}
