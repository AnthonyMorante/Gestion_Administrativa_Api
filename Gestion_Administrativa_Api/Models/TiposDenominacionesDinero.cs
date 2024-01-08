using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class TiposDenominacionesDinero
{
    public string IdTipoDenominacion { get; set; } = null!;

    public string? Tipo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DenominacionesDinero> DenominacionesDinero { get; set; } = new List<DenominacionesDinero>();
}
