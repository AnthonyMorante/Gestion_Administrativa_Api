using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Sriformaspagos
{
    public string Codigo { get; set; } = null!;

    public string? Formapago { get; set; }

    public bool? Activo { get; set; }
}
