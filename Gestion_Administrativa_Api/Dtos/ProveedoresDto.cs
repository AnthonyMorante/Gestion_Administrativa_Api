﻿namespace Gestion_Administrativa_Api.Dtos
{
    public class ProveedoresDto
    {

        public Guid? IdProveedor { get; set; }

        public string? Identificacion { get; set; }

        public string? RazonSocial { get; set; }

        public string? Representante { get; set; }

        public string? Direccion { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? PaginaWeb { get; set; }

        public string? Observacion { get; set; }

        public Guid? IdCiudad { get; set; }

        public Guid? IdTipoIdentificacion { get; set; }

        public Guid? IdEmpresa { get; set; }

        public bool? Activo { get; set; }

    }
}
