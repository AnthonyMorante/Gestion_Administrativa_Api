using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Empresas
{
    public Guid IdEmpresa { get; set; }

    public string? Identificacion { get; set; }

    public string? RazonSocial { get; set; }

    public string? Telefono { get; set; }

    public bool? LlevaContabilidad { get; set; }

    public bool? RegimenMicroempresas { get; set; }

    public bool? RegimenRimpe { get; set; }

    public bool? AgenteRetencion { get; set; }

    public Guid? IdTipoNegocio { get; set; }

    public virtual ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();

    public virtual ICollection<Empleados> Empleados { get; set; } = new List<Empleados>();

    public virtual Empresas IdEmpresaNavigation { get; set; } = null!;

    public virtual TipoNegocios? IdTipoNegocioNavigation { get; set; }

    public virtual Empresas? InverseIdEmpresaNavigation { get; set; }

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();

    public virtual ICollection<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
}
