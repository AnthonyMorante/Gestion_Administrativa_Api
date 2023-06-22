using Gestion_Administrativa_Api.Models;

namespace Gestion_Administrativa_Api.Dtos
{
    public class ProductosDto
    {
        public Guid ?IdProducto { get; set; }

        public string? Codigo { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }

        public decimal? Precio { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public Guid? IdIva { get; set; }

        public Guid? IdEmpresa { get; set; }
        public decimal? TotalIva { get; set; }

        public virtual ICollection<DetallePrecioProductosDto> DetallePrecioProductos { get; set; } = new List<DetallePrecioProductosDto>();
    }
}
