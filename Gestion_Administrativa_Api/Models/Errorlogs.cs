using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Errorlogs
{
    public int Iderror { get; set; }

    public DateOnly? Fecharegistro { get; set; }

    public string? Error { get; set; }
}
