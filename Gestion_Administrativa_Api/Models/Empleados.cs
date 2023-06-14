using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Empleados
{
    public Guid IdEmpleado { get; set; }

    public string? Identificacion { get; set; }

    public string? RazonSocial { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public Guid? IdCiudad { get; set; }

    public Guid? IdTipoIdentificacion { get; set; }

    public Guid? IdEmpresa { get; set; }

    public bool? Activo { get; set; }

    public virtual Ciudades? IdCiudadNavigation { get; set; }

    public virtual Empresas? IdEmpresaNavigation { get; set; }

    public virtual TipoIdentificaciones? IdTipoIdentificacionNavigation { get; set; }
}
