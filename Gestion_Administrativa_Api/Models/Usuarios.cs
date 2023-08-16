using System;
using System.Collections.Generic;

namespace Gestion_Administrativa_Api.Models;

public partial class Usuarios
{
    public Guid IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Clave { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Facturas> Facturas { get; set; } = new List<Facturas>();

    public virtual ICollection<UsuarioEmpresas> UsuarioEmpresas { get; set; } = new List<UsuarioEmpresas>();
}
