using AutoMapper;
using Gestion_Administrativa_Api.Documents_Models.Factura;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;
using System.Xml;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using System.Drawing.Printing;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IFacturas
    {
        Task<dynamic> guardar(FacturaDto? _facturaDto);
        Task<bool> generaRide(ActionContext ac, factura_V1_0_0 _factura, string email);
        Task<string> generaRecibo(ActionContext ac, factura_V1_0_0 factura_V1_0_0, FacturaDto facturaDto);

    }

    public class FacturasI : IFacturas
    {


        private readonly _context _context;
        private readonly IMapper _mapper;
        private readonly IUtilidades _IUtilidades;
        private readonly IConfiguration _configuration;
        private factura_V1_0_0? _factura_V1_0_0;


        public FacturasI(_context context, IMapper mapper, IUtilidades iUtilidades, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
            _configuration = configuration;
        }






        public async Task<dynamic> guardar(FacturaDto? _facturaDto)
        {
            try
            {
                var consultaEmpresa = await _context.Empresas.FindAsync(_facturaDto.idEmpresa);
                var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_facturaDto.idEstablecimiento);

                if (consultaEmpresa == null || consultaEstablecimiento == null)
                {

                    return "null";
                }

                var factura = _mapper.Map<Facturas>(_facturaDto);
                var detalle = _mapper.Map<IEnumerable<DetalleFacturas>>(_facturaDto.detalleFactura);

                factura.EmisorRuc = consultaEmpresa.Identificacion;
                var claveAcceso = await _IUtilidades.claveAcceso(factura);
                factura.ClaveAcceso = claveAcceso;
                factura.TipoEmision = Convert.ToInt16(_configuration["SRI:tipoEmision"]);
                factura.Ambiente = Convert.ToInt16(_configuration["SRI:ambiente"]);
                factura.Moneda = _configuration["SRI:moneda"];
                factura.EmisorRuc = consultaEmpresa.Identificacion;
                factura.EmisorRazonSocial = consultaEmpresa.RazonSocial;
                factura.RegimenMicroempresas = consultaEmpresa.RegimenMicroEmpresas;
                factura.ObligadoContabilidad = consultaEmpresa.LlevaContabilidad;
                factura.ResolucionAgenteRetencion = consultaEmpresa.ResolucionAgenteRetencion;
                factura.RegimenRimpe = consultaEmpresa.RegimenRimpe;
                factura.IdTipoEstadoDocumento = 1;
                factura.ExentoIva = 0;
                factura.Ice = 0;
                factura.Irbpnr = 0;
                factura.Isd = 0;
                factura.DireccionMatriz = consultaEmpresa.DireccionMatriz;
                factura.DireccionEstablecimiento = consultaEstablecimiento.Direccion;
                factura.IdFactura = Guid.NewGuid();
                await _context.Facturas.AddAsync(factura);
                await _context.DetalleFacturas.AddRangeAsync(detalle);

                if (_facturaDto.idDocumentoEmitir == Guid.Parse("246e7fef-4260-4522-9861-b38c7499ce67"))
                {

                    foreach (var item in detalle)
                    {
                        var consultaProducto = await _context.Productos.FindAsync(item.IdProducto);

                        if (consultaProducto != null)
                        {
                            consultaProducto.Cantidad -= item.Cantidad;
                            _context.Entry(consultaProducto).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (_facturaDto.formaPago.ToList().Count > 0)
                    {
                        var formaPago = _mapper.Map<IEnumerable<DetalleFormaPagos>>(_facturaDto.formaPago);
                        formaPago = formaPago.Select(x =>
                        {
                            x.IdFactura = factura.IdFactura;
                            return x;
                        }).ToList();
                        await _context.DetalleFormaPagos.AddRangeAsync(formaPago);

                    }


                    var consultaSecuencial = await _context.Secuenciales.FirstOrDefaultAsync(x => x.IdEmpresa == consultaEmpresa.IdEmpresa && x.IdTipoDocumento == _facturaDto.idTipoDocumento);
                    consultaSecuencial.Nombre = consultaSecuencial.Nombre + 1;
                    _context.Secuenciales.Update(consultaSecuencial);
                    await _context.SaveChangesAsync();
                    var ruta = await generarXml(factura, _facturaDto) ?? throw new Exception("Error al generar Documento");
                    var firmar = await firmarXml(factura.IdFactura, ruta?.documento) ?? throw new Exception("Error al firmar y guardar XML");
                    var enviar = await enviarSri(factura.ClaveAcceso); if (enviar == null) throw new Exception("Error al enviar XML");


                }

                return _factura_V1_0_0;


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> generarXml(Facturas? _factura, FacturaDto _facturaDto)
        {
            try
            {
                var factura = new factura_V1_0_0();
                var totalImpuestoList = new List<totalImpuesto_V1_0_0>();
                var totalImpuesto = new totalImpuesto_V1_0_0();
                totalImpuesto.codigo = 2;
                totalImpuesto.codigoPorcentaje = 2;
                totalImpuesto.baseImponible = _factura.Subtotal12;
                totalImpuesto.valor = _factura.Iva12;
                totalImpuestoList.Add(totalImpuesto);
                var infoTributaria = _mapper.Map<infoTributaria_V1_0_0>(_factura);
                var infoFactura = _mapper.Map<infoFactura_V1_0_0>(_factura);
                var detalleFactura = _mapper.Map<List<detalle_V1_0_0>>(_facturaDto.detalleFactura);
                var infoAdicional = _mapper.Map<List<detAdicional_V1_0_0>>(_factura.InformacionAdicional);
                var formaPago = _mapper.Map<List<pago_V1_0_0>>(_factura.DetalleFormaPagos);
                factura.infoTributaria = infoTributaria;
                factura.infoFactura = infoFactura;
                factura.infoFactura.totalConImpuestos = totalImpuestoList;
                factura.infoFactura.pagos = formaPago;
                factura.detalles = detalleFactura;
                factura.infoAdicional = infoAdicional;

                XmlSerializerNamespaces serialize = new XmlSerializerNamespaces();
                serialize.Add("", "");
                XmlSerializer oXmlSerializar = new XmlSerializer(typeof(factura_V1_0_0));
                string xmlFactura = "";
                using (var stream = new System.IO.StringWriter())
                {
                    using (XmlWriter writter = XmlWriter.Create(stream))
                    {


                        oXmlSerializar.Serialize(writter, factura, serialize);

                        xmlFactura = stream.ToString();
                    }
                }
                XDocument doc = XDocument.Parse(xmlFactura);
                doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                var ruta = $"{_configuration["Pc:disco"]}\\Facturacion\\Xml\\{_factura.ClaveAcceso}.xml";
                _factura.Ruta = ruta;
                _factura_V1_0_0 = factura;
                return new { estado = true, documento = doc };


            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<bool> firmarXml(Guid idFactura, XDocument documento)
        {
            try
            {
                var consultaFactura = await _context.Facturas.FindAsync(idFactura);
                if (consultaFactura == null) return false;
                var consultaUsuarioEmpresa = await _context.UsuarioEmpresas
                    .Include(x => x.IdEmpresaNavigation)
                    .Include(x => x.IdEmpresaNavigation.IdInformacionFirmaNavigation)
                    .FirstOrDefaultAsync(x => x.IdUsuario == consultaFactura.IdUsuario);
                if (consultaFactura == null) return false;
                var firmar = await _IUtilidades.firmar(consultaFactura.ClaveAcceso, consultaUsuarioEmpresa.IdEmpresaNavigation.IdInformacionFirmaNavigation.Codigo, consultaUsuarioEmpresa.IdEmpresaNavigation.IdInformacionFirmaNavigation.Ruta, documento);

                if (firmar == true)
                {
                    consultaFactura.IdTipoEstadoDocumento = 2;
                    _context.Entry(consultaFactura).Property("IdTipoEstadoDocumento").IsModified = true;
                    _context.SaveChanges();

                }

                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<bool> enviarSri(string? claveAcceso)
        {
            try
            {

                var enviar = await _IUtilidades.envioXmlSRI(claveAcceso, null);


                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public async Task<bool> generaRide(ActionContext ac, factura_V1_0_0 factura_V1_0_0, string email)
        {

            try
            {


                var pdf = new ViewAsPdf("~/Views/Factura/FacturaV1_1_0.cshtml", factura_V1_0_0);
                byte[] pdfBytes = await pdf.BuildFile(ac);
                var envairRide = await _IUtilidades.envioCorreo(email, pdfBytes, factura_V1_0_0.infoTributaria.claveAcceso);
                string base64 = Convert.ToBase64String(pdfBytes);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<string> generaRecibo(ActionContext ac, factura_V1_0_0 factura_V1_0_0, FacturaDto facturaDto)
        {

            try
            {
                string telefono = facturaDto.telefono;

                var pdf = new ViewAsPdf("~/Views/Factura/ReciboFacturaV1_1_0.cshtml", new { factura_V1_0_0, telefono })
                {
                    PageWidth = 80,
                    PageHeight = 297,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    CustomSwitches = "--margin-top 0 --margin-right 0 --margin-bottom 0 --margin-left 0"

                };
                byte[] pdfBytes = await pdf.BuildFile(ac);
                string base64 = Convert.ToBase64String(pdfBytes);
                return base64;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
