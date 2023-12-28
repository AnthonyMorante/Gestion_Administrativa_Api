using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriTiposDocumentos
{
    public string CodDoc { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SriFacturas> SriFacturas { get; set; } = new List<SriFacturas>();
}
