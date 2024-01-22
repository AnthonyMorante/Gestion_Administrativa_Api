using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using System.Text;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Microsoft.EntityFrameworkCore;
using static Gestion_Administrativa_Api.Utilities.Tools;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Gestion_Administrativa_Api.Documents_Models.Retencion;
using static Gestion_Administrativa_Api.Documents_Models.Retencion.retencion_V100;
using System.Data;
using Dapper;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IRetenciones
    {

        Task<IActionResult> guardar(RetencionDto? _retencionDto);


    }


    public class RetencionesI : IRetenciones
    {


        private readonly _context _context;
        private readonly IMapper _mapper;
        private readonly IUtilidades _IUtilidades;
        private readonly IDbConnection _dapper;
        public RetencionesI(_context context, IMapper mapper, IUtilidades iUtilidades)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
            _dapper = context.Database.GetDbConnection();
        }


        public async Task<IActionResult> guardar(RetencionDto? _retencionDto)
        {
            try
            {
                var result = new ObjectResult(null);
                //var consultaEmpresa = await _context.Empresas.FindAsync(_retencionDto?.idEmpresa);
                //var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_retencionDto?.idEstablecimiento);
                //if (consultaEmpresa == null) throw new Exception("No se ha encontrado la empresa");
                //if (consultaEstablecimiento == null) throw new Exception("No se ha encontrado el establecimiento");
                //var retenciones = _mapper.Map<Retenciones>(_retencionDto);
                //retenciones.EmisorRuc = consultaEmpresa.Identificacion;
                //var claveAcceso = await _IUtilidades.claveAccesoRetencion(retenciones);
                //retenciones.ClaveAcceso = claveAcceso;
                //retenciones.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
                //retenciones.DireccionMatriz = consultaEmpresa.DireccionMatriz;
                //retenciones.EmisorNombreComercial = consultaEmpresa.RazonSocial;
                //retenciones.EmisorRazonSocial = consultaEmpresa.RazonSocial;
                //retenciones.PeriodoFiscal = _retencionDto?.fechaEmisionDocSustento!.Value.ToString("MM-yyyy");
                //var informacionAdicionales = _mapper.Map<List<InformacionAdicionalRetencion>>(_retencionDto?.infoAdicional);
                //var impuestos = _mapper.Map<List<ImpuestoRetenciones>>(_retencionDto?.impuestos);
                //retenciones.InformacionAdicionalRetencion = informacionAdicionales;
                //retenciones.ImpuestoRetenciones = impuestos;
                //await _context.AddAsync(retenciones);
                //var facturaSri = await _context.SriFacturas.Where(x => x.ClaveAcceso == _retencionDto!.numAutDocSustento).FirstOrDefaultAsync();
                //facturaSri!.RetencionGenerada = true;
                //await _context.SaveChangesAsync();
                var xml=await generarXml("2101202407180224787200110010010000000014664310513") ;
                var xmlFirmado = await firmarXml("2101202407180224787200110010010000000014664310513", xml);
                await _IUtilidades.envioXmlSriComprobacion(xmlFirmado);
                result.StatusCode = 200;

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<XDocument> generarXml(string claveAcceso)
        {
            try
            {

                var consulta = await _context.Retenciones
                    .Include(x => x.ImpuestoRetenciones)
                    .ThenInclude(x => x.IdPorcentajeImpuestoRetencionNavigation)
                    .ThenInclude(x => x.IdTipoValorRetencionNavigation)
                    .Include(x => x.InformacionAdicionalRetencion)
                    .Include(x => x.IdFacturaNavigation)
                    .Where(x => x.ClaveAcceso == claveAcceso).FirstOrDefaultAsync();

                var retencion = new retencion_V1_0_0();
                var consultaEmpresa = await _context.Empresas.FindAsync(consulta?.IdEmpresa);
                var infoTributaria = _mapper.Map<retencion_infoTributaria_V1_0_0>(consulta);
                var infoCompRetencion = _mapper.Map<retencion_info_V1_0_0>(consulta?.IdFacturaNavigation);
                var impuestos = _mapper.Map<List<retenciones_impuestos_V1_0_0>>(consulta?.ImpuestoRetenciones);
                var infoAdicional = _mapper.Map<List<retencion_inf_adicional_V1_0_0>>(consulta?.InformacionAdicionalRetencion);
                infoCompRetencion.fechaEmision = consulta.FechaEmision.Value.ToString("dd/MM/yyyy");
                infoCompRetencion.periodoFiscal = consulta.FechaEmision.Value.ToString("MM/yyyy");
                infoCompRetencion.dirEstablecimiento = consulta.DireccionMatriz;
                infoCompRetencion.obligadoContabilidad = consulta.ObligadoContabilidad == true ?"SI":"";
                infoTributaria.ruc = consultaEmpresa?.Identificacion;
                infoTributaria.razonSocial = consultaEmpresa.RazonSocial;
                infoTributaria.nombreComercial = consultaEmpresa.RazonSocial;
                infoTributaria.dirMatriz = consultaEmpresa.DireccionMatriz;
                infoTributaria.estab = consulta?.Establecimiento?.ToString().PadLeft(3, '0');
                infoTributaria.ptoEmi = consulta?.PuntoEmision?.ToString().PadLeft(3, '0');
                infoTributaria.secuencial = consulta?.Secuencial?.ToString().PadLeft(9, '0');
                infoTributaria.agenteRetencion = consultaEmpresa.AgenteRetencion==true ? consultaEmpresa.ResolucionAgenteRetencion:null;
                infoTributaria.regimenMicroempresas = consultaEmpresa.RegimenMicroEmpresas == true ? "CONTRIBUYENTE RÉGIMEN MICROEMPRESAS" : null;
                retencion.infoTributaria= infoTributaria;
                retencion.infoCompRetencion = infoCompRetencion;
                retencion.impuestos = impuestos;
                retencion.infoAdicional = infoAdicional;
                XmlSerializerNamespaces serialize = new XmlSerializerNamespaces();
                serialize.Add("", "");
                XmlSerializer oXmlSerializar = new XmlSerializer(typeof(retencion_V1_0_0));
                string xmlRetencion = "";
                using (var stream = new StringWriter())
                {
                    using (XmlWriter writter = XmlWriter.Create(stream))
                    {
                        oXmlSerializar.Serialize(writter, retencion, serialize);

                        xmlRetencion = stream.ToString();
                    }
                }
                XDocument doc = XDocument.Parse(xmlRetencion);
                doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                return doc;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }


        public async Task<XmlDocument?> firmarXml(string claveAcceso, XDocument documento)
        {
            try
            { 
                string sql = @"

                                  SELECT i.codigo,i.ruta FROM retenciones r 
								  INNER JOIN ""usuarioEmpresas"" ue ON ue.""idUsuario""=r.""idUsuario""
								  INNER JOIN ""empresas"" e ON e.""idEmpresa""=ue.""idEmpresa""
	                              INNER JOIN ""informacionFirmas"" i ON i.""identificacion""=e.""identificacion""
	                              WHERE r.""claveAcceso""=@claveAcceso	 
                             ";
                var firma = await _dapper.QueryFirstOrDefaultAsync(sql, new { claveAcceso });
                var documentoFirmado = await _IUtilidades.firmar(firma.codigo, $"{Tools.rootPath}{firma.ruta}", documento);
                if (documentoFirmado == null) throw new Exception("Error al firmar el documento");
                return documentoFirmado.Document;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



    }
}
