using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class SriEstados
{
    public int Codigo { get; set; }

    public string? Estado { get; set; }

    public bool? Activo { get; set; }
}
