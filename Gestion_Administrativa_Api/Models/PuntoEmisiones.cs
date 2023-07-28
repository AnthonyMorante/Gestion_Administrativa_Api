using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class PuntoEmisiones
{
    public Guid IdPuntoEmision { get; set; }

    public string? Nombre { get; set; }

    public bool? Predeterminado { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public Guid? IdEmpresa { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Empresas? IdEmpresaNavigation { get; set; }
}
