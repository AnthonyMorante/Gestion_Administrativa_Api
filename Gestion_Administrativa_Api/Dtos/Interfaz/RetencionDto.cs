

namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class RetencionDto
    {

        public int? idFactura { get; set; }
        public Guid? idUsuario { get; set; }
        public Guid? idEmpresa { get; set; }
        public Guid? idTipoDocumento { get; set; } = Guid.Parse("13edf7b1-0a10-4eef-a095-11519bd15138");
        public Guid? idTipoIdenticacion { get; set; }
        public Guid? idDocumentoEmitir { get; set; } = Guid.Parse("0fc1eb22-a134-41ac-8c4c-59e6e011b56c");
        public Guid? idEstablecimiento { get; set; }
        public Guid? idPuntoEmision { get; set; }
        public string? ambiente { get; set; }
        public string? tipoEmision { get; set; } = "1";
        public string? emisorRazonSocial { get; set; }
        public string? emisorNombreComercial { get; set; }
        public string? emisorRuc { get; set; }
        public string? claveAcceso { get; set; }
        public int? tipoDocumento { get; set; } = 7;
        public int? establecimiento { get; set; }
        public int? puntoEmision { get; set; }
        public int? secuencial { get; set; }
        public string? direccionMatriz { get; set; }
        public string? agenteRetencion { get; set; }
        public DateTime? fechaEmision { get; set; }
        public DateTime? fechaEmisionDocSustento { get; set; }
        public string? obligadoContabilidad { get; set; }
        public string? tipoIdentificacionSujetoRetenido { get; set; }
        public string? razonSocialSujetoRetenido { get; set; }
        public string? identificacionSujetoRetenido { get; set; }
        public string? periodoFiscal { get; set; }
        public string? numAutDocSustento { get; set; }
        public string? versionXml { get; set; } = "1.0.0";
        public List<Impuesto>? impuestos { get; set; }
        public List<InfoAdicional>? infoAdicional { get; set; }


        public class Impuesto
        {
            public string? tipoRetencion { get; set; }
            public int? codigo { get; set; }
            public int? codigoRetencion { get; set; }
            public decimal? baseImponible { get; set; }
            public decimal? porcentajeRetener { get; set; }
            public decimal? valorRetenido { get; set; }
            public string? codDocSustento { get; set; } = "01";
            public string? numDocSustento { get; set; }
            public string? fechaEmisionDocSustento { get; set; }
            public Guid idPorcentajeImpuestoRetencion { get; set; }
        }

        public class InfoAdicional
        {
            public string? nombre { get; set; }
            public string? valor { get; set; }
        }






    }

}
