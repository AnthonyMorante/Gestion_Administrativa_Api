namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class DetallePrecioProductosDto
    {

        public Guid IdDetallePrecioProducto { get; set; }

        public decimal? TotalIva { get; set; }

        public decimal? Porcentaje { get; set; }

        public decimal? Utilidad { get; set; }

        public Guid? IdProducto { get; set; }

        public Guid? IdIva { get; set; }


    }
}
