namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class FacturaDto
    {
        public Guid idCliente { get; set; }

        public string ?identificacion { get; set; }
        public string? razonSocial { get; set; }
        public string ?telefono { get; set; }
        public string ?direccion { get; set; }
        public string ? email { get; set; }
        public Guid ? idDocumentoEmitir { get; set; }
        public Guid ? idEstablecimiento { get; set; }
        public Guid ? idFormaPago { get; set; }
        public Guid ? idPuntoEmision { get; set; }
        public decimal? subtotal12 { get; set; }
        public decimal? subtotal0 { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? iva12{ get; set; }
        public decimal? totalFactura{ get; set; }
        public decimal? totalDecuento { get; set; }
        public IEnumerable<formaPagoDto>? formaPago { get; set; }
        public IEnumerable<informacionAdicionalDto>? informacionAdicional { get; set; }
        public IEnumerable<DetalleDto>? detalleFactura { get; set; }


    }



    public class formaPagoDto
    {
        public Guid? idFormaPago { get; set; }
        public string? formaPago { get; set; }
        public int? plazo { get; set; }
        public string? tiempo{ get; set; }
        public decimal? valor { get; set; }

    }


    public class informacionAdicionalDto

    {

        public string? nombre { get; set; }
        public string? valor { get; set; }


    }



    public class DetalleDto

    {
        public string? idDetallePrecioProducto{ get; set; }
        public Guid? idIva { get; set; }
        public Guid? idProducto { get; set; }
        public string? nombre { get; set; }
        public string? nombrePorcentaje{ get; set; }
        public decimal? cantidad    { get; set; }
        public string? codigo { get; set; }
        public decimal? descuento { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? total { get; set; }
        public decimal? totalSinIva{ get; set; }
        public decimal? valor { get; set; }
        public decimal? valorPorcentaje { get; set; }



    }



}
