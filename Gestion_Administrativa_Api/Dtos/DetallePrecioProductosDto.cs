namespace Gestion_Administrativa_Api.Dtos
{
    public class DetallePrecioProductosDto
    {

        public Guid IdDetallePrecioProducto { get; set; }

        public decimal? TotalIva { get; set; }

        public decimal? Porcentaje { get; set; }

        public decimal? Utilidad { get; set; }

        public Guid? IdProducto { get; set; }

    }
}
