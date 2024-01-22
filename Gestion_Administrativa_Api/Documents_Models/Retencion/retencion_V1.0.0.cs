using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Documents_Models.Retencion
{
    public class retencion_V100
    {
        [XmlType("comprobanteRetencion")]
        public class retencion_V1_0_0
        {
            [XmlAttribute]
            public string id { get; set; } = "comprobante";
            [XmlAttribute]
            public string version { get; set; } = "1.0.0";
            public retencion_infoTributaria_V1_0_0? infoTributaria { get; set; }
            public retencion_info_V1_0_0? infoCompRetencion { get; set; }
            public List<retenciones_impuestos_V1_0_0>? impuestos { get; set; }
            public retencion_infoTributaria_V1_0_0? maquinaFiscal { get; set; }
            public List<retencion_inf_adicional_V1_0_0>? infoAdicional { get; set; }
        }


        public class retencion_infoTributaria_V1_0_0
        {
            public string? ambiente { get; set; }
            public string? tipoEmision { get; set; }
            public string? razonSocial { get; set; }
            public string? nombreComercial { get; set; }
            public string? ruc { get; set; }
            public string? claveAcceso { get; set; }
            public string? codDoc { get; set; } = "07";
            public string? estab { get; set; }
            public string? ptoEmi { get; set; }
            public string? secuencial { get; set; }
            public string? dirMatriz { get; set; }
            public string? agenteRetencion { get; set; }
            public string? contribuyenteRimpe { get; set; }
            public string? regimenMicroempresas { get; set; }

        }



        public class retencion_info_V1_0_0
        {

            public string? fechaEmision { get; set; }
            public string? dirEstablecimiento { get; set; }
            public string? contribuyenteEspecial { get; set; }
            public string? obligadoContabilidad { get; set; }
            public string? tipoIdentificacionSujetoRetenido { get; set; } = "04";
            public string? razonSocialSujetoRetenido { get; set; }
            public string? identificacionSujetoRetenido { get; set; }
            public string? periodoFiscal { get; set; }

        }

        [XmlType("impuesto")]
        public class retenciones_impuestos_V1_0_0
        {
            public int? codigo { get; set; }
            public int? codigoRetencion { get; set; }
            public decimal? baseImponible { get; set; }
            public decimal? porcentajeRetener { get; set; }
            public decimal? valorRetenido { get; set; }
            public string? codDocSustento { get; set; }
            public string? numDocSustento { get; set; }
            public string? fechaEmisionDocSustento { get; set; }

        }



   

        public class retencion_maquina_fiscal_V1_0_0
        {
         
            public string? marca { get; set; }
            public string? modelo { get; set; }
            public string? serie { get; set; }
        }



        [XmlType("campoAdicional")]
        public class retencion_inf_adicional_V1_0_0
        {
            [XmlAttribute]
            public string? nombre { get; set; }
            [XmlText]
            public string? valor { get; set; }
        }


    }
}
