

using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Documents_Models.Factura
{
    public class factura_V100
    {

        [XmlType("factura")]
        public class factura_V1_0_0
        {
            [XmlAttribute]
            public string id { get; set; } = "comprobante";
            [XmlAttribute]
            public string version { get; set; } = "1.1.0";
            public infoTributaria_V1_0_0? infoTributaria { get; set; }
            public infoFactura_V1_0_0? infoFactura { get; set; }
            public List <detalle_V1_0_0>? detalles { get; set; }
            public List<detAdicional_V1_0_0>? infoAdicional { get; set; }
        }


        public class infoTributaria_V1_0_0
        {
            public string? ambiente { get; set; }
            public string? tipoEmision { get; set; }
            public string? razonSocial { get; set; }
            public string? nombreComercial { get; set; }
            public string? ruc { get; set; }
            public string? claveAcceso { get; set; }
            public string? codDoc { get; set; }
            public string? estab { get; set; }
            public string? ptoEmi { get; set; }
            public string? secuencial { get; set; }
            public string? dirMatriz { get; set; }
            public string? agenteRetencion { get; set; }
            public string? contribuyenteRimpe { get; set; }
            public infoFactura_V1_0_0? infoFactura { get; set; }
            public decimal? propina { get; set; }
            public decimal? fleteInternacional { get; set; }
            public decimal? seguroInternacional { get; set; }
            public decimal? gastosAduaneros { get; set; }
            public decimal? gastosTransporteOtros { get; set; }
            //public decimal? importeTotal { get; set; }
            //public string moneda { get; set; }
            public string placa { get; set; }

        }

        public class infoFactura_V1_0_0
        {

            public string? fechaEmision { get; set; }
            public string? dirEstablecimiento { get; set; }
            public string? contribuyenteEspecial { get; set; }
            public string? obligadoContabilidad { get; set; }
            public string? comercioExterior { get; set; }
            public string? incoTermFactura { get; set; }
            public string? lugarIncoTerm { get; set; }
            public string? paisOrigen { get; set; }
            public string? puertoEmbarque { get; set; }
            public string? tipoIdentificacionComprador { get; set; }
            public string? puertoDestino { get; set; }
            public string? paisDestino { get; set; }
            public string? paisAdquisicion { get; set; }
            public string? guiaRemision { get; set; }
            public string? razonSocialComprador { get; set; }
            public string? identificacionComprador { get; set; }
            public string? direccionComprador { get; set; }
            public decimal? totalSinImpuestos { get; set; }
            public decimal? totalSubsidio { get; set; }
            public string incoTermTotalSinImpuestos { get; set; }
            public decimal? totalDescuento { get; set; }
            public string codDocReembolso { get; set; }
            public decimal? totalComprobantesReembolso { get; set; }
            public decimal? totalBaseImponibleReembolso { get; set; }
            public decimal? totalImpuestoReembolso { get; set; }
            //public decimal? totalConImpuestos { get; set; }
             //public ienumerable<compensacion_v1_0_0>? compensaciones { get; set; }
            //public ienumerable<compensacion_v1_0_0>? compensaciones { get; set; }
  
            public decimal? fleteInternacional { get; set; }
            public decimal? seguroInternacional { get; set; }
            public decimal? gastosAduaneros { get; set; }
            public decimal? gastosTransporteOtros { get; set; }
            public string placa { get; set; }
            //public IEnumerable<pago_V1_0_0>? pagos { get; set; }
            public decimal? valorRetIva { get; set; }
            public decimal? valorRetRenta { get; set; }
            //public IEnumerable<detalle_V1_0_0>? detalles { get; set; }
            public List<totalImpuesto_V1_0_0>? totalConImpuestos { get; set; }
            public string? propina { get; set; }
            public decimal? importeTotal { get; set; }
            public string moneda { get; set; }


            public List<pago_V1_0_0>? pagos { get; set; }


        }

        [XmlType("totalImpuesto")]
        public class totalImpuesto_V1_0_0
        {
            public int? codigo { get; set; }
            public int? codigoPorcentaje { get; set; }
            public decimal? descuentoAdicional { get; set; }
            public decimal? baseImponible { get; set; }
            public decimal? tarifa { get; set; }
            public decimal? valor { get; set; }
            public decimal? valorDevolucionIva { get; set; }
        }


        public class compensacion_V1_0_0
        {
            public string? codigo { get; set; }
            public decimal? tarifa { get; set; }
            public decimal? valor { get; set; }


        }
        [XmlType("pago")]
        public class pago_V1_0_0
        {
            public string? formaPago { get; set; }
            public decimal? total { get; set; }
            public decimal? plazo { get; set; }
            public string? unidadTiempo { get; set; }
        }

        [XmlType("campoAdicional")]
        public class detAdicional_V1_0_0
        {
            [XmlAttribute]
            public string? nombre { get; set; }
            [XmlText]
            public string? valor { get; set; }
        }



        [XmlType("impuesto")]
        public class impuesto_V1_0_0
        {
            public int codigo { get; set; }
            public int? codigoPorcentaje { get; set; }
            public string? tarifa { get; set; }
            public decimal? baseImponible { get; set; }
            public decimal? valor { get; set; }
        }

        [XmlType("detalle")]
        public class detalle_V1_0_0
        {
            public string codigoPrincipal { get; set; }
            public string codigoAuxiliar { get; set; }
            public string descripcion { get; set; }
            public string unidadMedida { get; set; }
            public decimal cantidad { get; set; }
            public decimal precioUnitario { get; set; }
            public decimal? descuento { get; set; }
            public decimal? precioTotalSinImpuesto { get; set; }
            
            public List<impuesto_V1_0_0>? impuestos { get; set; }
        }

    }
}
