namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class ClientesDto
    {

        public Guid? IdCliente { get; set; }

        public string? Identificacion { get; set; }

        public string? RazonSocial { get; set; }

        public string? Representante { get; set; }

        public string? Direccion { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public Guid? IdCiudad { get; set; }

        public Guid? IdTipoIdentificacion { get; set; }

        public Guid? IdEmpresa { get; set; }
    }
}
