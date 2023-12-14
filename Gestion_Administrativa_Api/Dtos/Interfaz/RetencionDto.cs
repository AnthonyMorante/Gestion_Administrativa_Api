using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class RetencionDto
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
        public IEnumerable<retencion_info_Dto>? infoCompRetencion { get; set; }
        public IEnumerable<retencion_impuesto_Dto>? impuestos { get; set; }
        public IEnumerable<retencion_inf_adicional_Dto>? infoAdicional { get; set; }

    }


    public class retencion_info_Dto
    {

        public string? fechaEmision { get; set; }
        public string? obligadoContabilidad { get; set; }
        public string? tipoIdentificacionSujetoRetenido { get; set; }
        public string? razonSocialSujetoRetenido { get; set; }
        public string? identificacionSujetoRetenido { get; set; }
        public string? periodoFiscal { get; set; }

    }

    public class retencion_impuesto_Dto
    {
        public int? codigo { get; set; }
        public int? codigoRetencion { get; set; }
        public decimal? baseImponible { get; set; }
        public decimal? porcentajeRetener { get; set; }
        public decimal? valorRetenido { get; set; }
        public decimal? codDocSustento { get; set; }
        public decimal? numDocSustento { get; set; }
        public decimal? fechaEmisionDocSustento { get; set; }
    }

    public class retencion_inf_adicional_Dto
    {
        
        public string? nombre { get; set; }
        public string? valor { get; set; }
    }

}
