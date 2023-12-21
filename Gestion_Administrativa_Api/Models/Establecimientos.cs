using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Establecimientos
{
    public Guid IdEstablecimiento { get; set; }

    public bool? Predeterminado { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public Guid? IdEmpresa { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public long? Nombre { get; set; }

    public string? Direccion { get; set; }

    public virtual Empresas? IdEmpresaNavigation { get; set; }

    public virtual ICollection<Proformas> Proformas { get; set; } = new List<Proformas>();

    public virtual ICollection<Retenciones> Retenciones { get; set; } = new List<Retenciones>();
}
