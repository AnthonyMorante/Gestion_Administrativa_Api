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
        public RetencionesI(_context context, IMapper mapper, IUtilidades iUtilidades)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
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
                //var claveAcceso = await _IUtilidades.claveAccesoRetencion(retenciones);
                //retenciones.ClaveAcceso = claveAcceso;
                //retenciones.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
                //retenciones.EmisorRuc = consultaEmpresa.Identificacion;
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
                await generarXml("140120240710010010000000011206520812");
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
                    .Include(x => x.InformacionAdicionalRetencion)
                    .Include(x=>x.IdFacturaNavigation)
                    .Where(x=> x.ClaveAcceso == claveAcceso).FirstOrDefaultAsync();

                var retencion = new retencion_V1_0_0();
                var consultaEmpresa = await _context.Empresas.FindAsync(consulta?.IdEmpresa);
                var retenciones = _mapper.Map<retencion_infoTributaria_V1_0_0>(consulta);
                var infoRetendio = _mapper.Map<retencion_info_V1_0_0>(consulta?.IdFacturaNavigation);
                //var impuestoRetencion = _mapper.Map<ICollection<retencion_impuesto_V1_0_0>>(consulta?.ImpuestoRetenciones);


                retenciones.ruc = consultaEmpresa.Identificacion;
                retenciones.razonSocial = consultaEmpresa.RazonSocial;
                retenciones.nombreComercial = consultaEmpresa.RazonSocial;
                retenciones.dirMatriz = consultaEmpresa.DireccionMatriz;
                retenciones.estab = consulta?.Establecimiento?.ToString().PadLeft(3, '0');
                retenciones.ptoEmi = consulta?.PuntoEmision?.ToString().PadLeft(3, '0');
                retenciones.secuencial = consulta?.Secuencial?.ToString().PadLeft(9, '0');
                retenciones.agenteRetencion = consultaEmpresa.AgenteRetencion==true ? consultaEmpresa.ResolucionAgenteRetencion:null;
                retenciones.regimenMicroempresas = consultaEmpresa.RegimenMicroEmpresas == true ? "CONTRIBUYENTE RÉGIMEN MICROEMPRESAS" : null;


                var retenciones2 = _mapper.Map<retencion_V100>(consulta);

                //var _facturaDto = await _FacturaDto(claveAcceso);
                //var _factura = await _Facturas(claveAcceso);
                //var factura = await _Factura_V1_0_0(claveAcceso);
                //XmlSerializerNamespaces serialize = new XmlSerializerNamespaces();
                //serialize.Add("", "");
                //XmlSerializer oXmlSerializar = new XmlSerializer(typeof(factura_V1_0_0));
                //string xmlFactura = "";
                //using (var stream = new StringWriter())
                //{
                //    using (XmlWriter writter = XmlWriter.Create(stream))
                //    {
                //        oXmlSerializar.Serialize(writter, factura, serialize);

                //        xmlFactura = stream.ToString();
                //    }
                //}
                XDocument doc = XDocument.Parse("");
                doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                return doc;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }



    }
}
