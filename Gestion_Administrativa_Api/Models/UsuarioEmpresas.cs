using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class UsuarioEmpresas
{
    public Guid IdUsuarioEmpresas { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdEmpresa { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public virtual Empresas IdEmpresaNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
