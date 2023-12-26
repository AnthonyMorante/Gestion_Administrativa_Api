using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Sriporcentajesimpuestos
{
    public string Codigoporcentaje { get; set; } = null!;

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public decimal? Porcentaje { get; set; }

    public decimal? Valor { get; set; }

    public bool? Activo { get; set; }
}
