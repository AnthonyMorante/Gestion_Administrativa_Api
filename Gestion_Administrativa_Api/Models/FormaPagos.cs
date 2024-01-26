using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class FormaPagos
{
    public Guid IdFormaPago { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? Codigo { get; set; }

    public virtual ICollection<DetalleFormaPagos> DetalleFormaPagos { get; set; } = new List<DetalleFormaPagos>();
}
