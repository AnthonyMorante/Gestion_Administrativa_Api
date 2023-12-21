

namespace Gestion_Administrativa_Api.Dtos.Interfaz
{
    public class RetencionDto
    {



        public Guid? idUsuario { get; set; }
        public Guid? idEmpresa { get; set; }
        public Guid? idTipoDocumento { get; set; } = Guid.Parse("13edf7b1-0a10-4eef-a095-11519bd15138");
        public Guid? idTipoIdenticacion { get; set; }
        public Guid? idDocumentoEmitir { get; set; } = Guid.Parse("0fc1eb22-a134-41ac-8c4c-59e6e011b56c");
        public Guid? idEstablecimiento { get; set; }
        public Guid? idPuntoEmision { get; set; }
        public string? ambiente { get; set; }
        public string? tipoEmision { get; set; }
        public string? emisorRazonSocial { get; set; }
        public string? emisorNombreComercial { get; set; }
        public string? emmisorRuc { get; set; }
        public string? claveAcceso { get; set; }
        public string? tipoDocumento { get; set; }
        public string? establecimiento { get; set; }
        public string? puntoEmision { get; set; }
        public string? secuencial { get; set; }
        public string? direccionMatriz { get; set; }
        public string? agenteRetencion { get; set; }
        public string? fechaEmision { get; set; }
        public string? obligadoContabilidad { get; set; }
        public string? tipoIdentificacionSujetoRetenido { get; set; }
        public string? razonSocialSujetoRetenido { get; set; }
        public string? identificacionSujetoRetenido { get; set; }
        public string? periodoFiscal { get; set; }
        public List<Impuesto>? impuestos { get; set; }
        public List<InfoAdicional>? infoAdicional { get; set; }


        public class Impuesto
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

        public class InfoAdicional
        {
            public string? nombre { get; set; }
            public string? valor { get; set; }
        }






    }

}
