using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class ErrorLogs
{
    public int IdError { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Error { get; set; }
}
