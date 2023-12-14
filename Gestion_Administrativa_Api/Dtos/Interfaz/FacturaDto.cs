namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class FacturaDto
    {

        public string versionXml { get; set; } = "1.0.0";
        public int? TipoDocumento { get; set; } = 1;
        public int? codigoTipoIdentificacion { get; set; } = 1;
        public string? establecimiento { get; set; }
        public decimal? valorRecibido { get; set; }
        public decimal? cambio { get; set; }
        public decimal? saldo { get; set; }
        public string? puntoEmision { get; set; }
        public string? secuencial { get; set; }
        public DateTime fechaEmision { get; set; }
        public Guid idUsuario { get; set; }
        public Guid idCiudad { get; set; }
        public Guid idEmpresa { get; set; }
        public Guid? idCliente { get; set; }
        public Guid idTipoDocumento { get; set; } = Guid.Parse("b973cdf3-3d69-444b-8a09-a8ef9f0aab55");
        public string? claveAcceso { get; set; }
        public Guid idTipoIdenticacion { get; set; }
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
        public decimal? totDescuento { get; set; }

        public IEnumerable<formaPagoDto>? formaPago { get; set; }
        public IEnumerable<informacionAdicionalDto>? informacionAdicional { get; set; }
        public IEnumerable<DetalleDto>? detalleFactura { get; set; }


    }



    public class formaPagoDto
    {
        public Guid? idFormaPago { get; set; }
        public Guid? idTiempoFormaPago { get; set; }
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
        public string? tarifaPorcentaje { get; set; }
        public decimal? valorProductoSinIva { get; set; }

        



    }



}
