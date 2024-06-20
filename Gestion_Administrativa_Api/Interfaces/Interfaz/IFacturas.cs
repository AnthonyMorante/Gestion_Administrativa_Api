using AutoMapper;
using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBarcode;
using Rotativa.AspNetCore;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

namespace Gestion_Administrativa_Api.Interfaces.Interfaz
{
    public interface IFacturas
    {
        Task<IActionResult> guardar(FacturaDto? _facturaDto);

        Task<byte[]> generaRide(ActionContext ac, string claveAcceso,bool proforma);

        Task<bool> enviarCorreo(string email, byte[] archivo, string nombreArchivo);

        Task<factura_V1_0_0?> _Factura_V1_0_0(string claveAcceso);

        Task<XDocument> generarXml(string claveAcceso);

        Task<XmlDocument> firmarXml(string claveAcceso);

        Task<bool> enviarSri(string claveAcceso);

        Task<byte[]> imprimirComprobante(string claveAcceso, ControllerContext cc);
    }

    public class FacturasI : IFacturas
    {
        private readonly _context _context;
        private readonly IMapper _mapper;
        private readonly IUtilidades _IUtilidades;
        private readonly IConfiguration _configuration;
        private factura_V1_0_0? _factura_V1_0_0;
        private readonly IDbConnection _dapper;

        public FacturasI(_context context, IMapper mapper, IUtilidades iUtilidades, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _IUtilidades = iUtilidades;
            _configuration = configuration;
            _dapper = context.Database.GetDbConnection();
        }

        public async Task<IActionResult> guardar(FacturaDto? _facturaDto)
        {
            var result = new ObjectResult("");
            try
            {
                var consultaEmpresa = await _context.Empresas.FindAsync(_facturaDto.idEmpresa);
                var consultaEstablecimiento = await _context.Establecimientos.FindAsync(_facturaDto.idEstablecimiento);

                if (consultaEmpresa == null) throw new Exception("No se ha encontrado la empresa");
                if (consultaEstablecimiento == null) throw new Exception("No se ha encontrado el establecimiento");

                var factura = _mapper.Map<Facturas>(_facturaDto);
                var detalle = _mapper.Map<List<DetalleFacturas>>(_facturaDto.detalleFactura);
                var detallePagos = _mapper.Map<List<DetalleFormaPagos>>(_facturaDto.formaPago);
                var detalleAdicional = _mapper.Map<List<InformacionAdicional>>(_facturaDto.informacionAdicional);
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
                factura.AgenteRetencion = consultaEmpresa.AgenteRetencion;
                factura.RegimenRimpe = consultaEmpresa.RegimenRimpe;
                factura.IdTipoEstadoDocumento = 1;
                factura.IdTipoEstadoSri = 1;
                factura.ExentoIva = 0;
                factura.Ice = 0;
                factura.Irbpnr = 0;
                factura.Isd = 0;
                factura.DireccionMatriz = consultaEmpresa.DireccionMatriz;
                factura.DireccionEstablecimiento = consultaEstablecimiento.Direccion;
                factura.DetalleFormaPagos = detallePagos;
                factura.InformacionAdicional = detalleAdicional;
                factura.DetalleFacturas = detalle;
                factura.FechaAutorizacion = null;
                _context.Facturas.Add(factura);
                //await _context.SaveChangesAsync();
                //await _context.DetalleFacturas.AddRangeAsync(detalle);

                if (_facturaDto.idDocumentoEmitir == Guid.Parse("246e7fef-4260-4522-9861-b38c7499ce67"))
                {
                    foreach (var item in detalle)
                    {
                        var consultaProducto = await _context.Productos.FindAsync(item.IdProducto);

                        if (consultaProducto != null)
                        {
                            consultaProducto.Cantidad -= item.Cantidad;
                            _context.Productos.Update(consultaProducto);
                        }
                    }
                    var consultaSecuencial = await _context.Secuenciales.FirstOrDefaultAsync(x => x.IdEmpresa == consultaEmpresa.IdEmpresa && x.IdTipoDocumento == _facturaDto.idTipoDocumento);
                    consultaSecuencial.Nombre = consultaSecuencial.Nombre + 1;
                    _context.Secuenciales.Update(consultaSecuencial);
                    factura.IdTipoEstadoSri = 1;
                    await _context.SaveChangesAsync();
                    //var enviadoSri = await enviarSri(factura.ClaveAcceso);
                    //var idTipoEstadoSri = enviadoSri ? 6 : 0;
                    //string sql = @"UPDATE facturas SET ""idTipoEstadoSri""=@idTipoEstadoSri
                    //               WHERE ""claveAcceso"" = @claveAcceso;";
                    //await _dapper.ExecuteScalarAsync(sql, new { claveAcceso = factura.ClaveAcceso, idTipoEstadoSri });
                }
                else
                {
                    var consultaSecuencial = await _context.Secuenciales.FirstOrDefaultAsync(x => x.IdEmpresa == consultaEmpresa.IdEmpresa && x.IdTipoDocumento == _facturaDto.idTipoDocumento);
                    consultaSecuencial.Nombre = consultaSecuencial.Nombre + 1;
                    factura.IdTipoEstadoSri = 2;
                    factura.TipoDocumento = 0;
                    _context.Secuenciales.Update(consultaSecuencial);
                    await _context.SaveChangesAsync();
                }
                result.StatusCode = 200;
                result.Value=factura.ClaveAcceso;
                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Value = ex.Message;
                return result;
            }
        }

        private async Task<Facturas> _Facturas(string claveAcceso)
        {
            try
            {
                string sql = @"SELECT * FROM facturas
                              WHERE ""claveAcceso"" =@claveAcceso";
                var factura = await _dapper.QueryFirstOrDefaultAsync<Facturas>(sql, new { claveAcceso });
                sql = @"SELECT * FROM ""detalleFacturas""
                       WHERE ""idFactura"" =@idFactura";
                factura.DetalleFacturas = (await _dapper.QueryAsync<DetalleFacturas>(sql, factura)).ToList();
                sql = @"SELECT * FROM ""informacionAdicional""
                         WHERE ""idFactura"" =@idFactura
                        ";
                factura.InformacionAdicional = (await _dapper.QueryAsync<InformacionAdicional>(sql, factura)).ToList();
                sql = @"SELECT * FROM ""detalleFormaPagos"" dfp
                        WHERE ""idFactura"" =@idFactura";
                factura.DetalleFormaPagos = (await _dapper.QueryAsync<DetalleFormaPagos>(sql, factura)).ToList();
                return factura;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new Facturas();
            }
        }

        private async Task<FacturaDto> _FacturaDto(string claveAcceso)
        {
            try
            {
                string sql = @"SELECT ""tipoDocumento"" AS ""TipoDocumento"",
                                ""versionXml"",ti.codigo AS ""codigoTipoIdentificacion"",
                                establecimiento,""puntoEmision"",
                                secuencial,f.""fechaEmision"",f.""idUsuario"",f.""idCiudad"",
                                e.""idEmpresa"",f.""idCliente"",td.""idTipoDocumento"",f.""claveAcceso"",
                                ti.""idTipoIdentificacion"",f.""receptorRuc"" AS identificacion,
                                f.""receptorRazonSocial"" AS ""razonSocial"",
                                f.""receptorTelefono"" AS telefono,f.""receptorDireccion"" AS direccion,
                                f.""receptorCorreo"" AS email,
                                f.""idDocumentoEmitir"",e.""idEstablecimiento"",
                                f.""idPuntoEmision"",f.""idPuntoEmision"",
                                f.subtotal12,f.subtotal0,f.iva12,f.""totalSinImpuesto"" AS subtotal,
                                f.""totalImporte"" AS totalFactura,f.""totalDescuento"" AS ""totDescuento""
                                FROM facturas f
                                INNER JOIN clientes c ON c.""idCliente"" =f.""idCliente""
                                INNER JOIN ""tipoIdentificaciones"" ti ON ti.""codigo"" =f.""receptorTipoIdentificacion""
                                INNER JOIN establecimientos e  ON e.""idEstablecimiento"" =f.""idEstablecimiento""
                                INNER JOIN ""tipoDocumentos"" td ON td.""codigo"" = f.""tipoDocumento""
                                WHERE f.""claveAcceso"" =@claveAcceso";
                var factura = await _dapper.QueryFirstOrDefaultAsync<FacturaDto>(sql, new { claveAcceso });
                sql = @"SELECT ""idFactura"" FROM facturas WHERE ""claveAcceso""=@claveAcceso";
                var idFactura = await _dapper.ExecuteScalarAsync<Guid>(sql, new { claveAcceso });
                sql = @"SELECT dfp.""idFormaPago"",
                        tfp.""idTiempoFormaPago"",
                        fp.descripcion AS ""formPago"",
                        dfp.plazo, tfp.nombre AS ""tiempo"",
                        dfp.valor
                        FROM ""detalleFormaPagos"" dfp
                        INNER JOIN ""formaPagos"" fp ON fp.""idFormaPago"" =dfp.""idFormaPago""
                        INNER JOIN ""tiempoFormaPagos"" tfp ON dfp.""idTiempoFormaPago"" =tfp.""idTiempoFormaPago""
                        WHERE dfp.""idFactura""=@idFactura;";
                factura.formaPago = await _dapper.QueryAsync<formaPagoDto>(sql, new { idFactura });
                sql = @"SELECT cast(""idDetalleFactura"" AS varchar(max)) AS ""idDetallePrecioProducto"",
                        df.""idIva"",df.""idProducto"",
                        p.nombre,i.nombre  AS ""nombrePorcentaje"",
                        df.cantidad,i.codigo,df.descuento,
                        i.valor  AS porcentaje ,df.total,df.subtotal AS ""totalSinIva"",
                        df.""porcentaje"" AS ""valorPorcentaje"",
                        df.total AS ""valor"",
                        i.descripcion  AS ""tarifaPorcentaje"",
                        ROUND(df.subtotal/df.cantidad,2) AS ""valorProductoSinIva""
                        FROM ""detalleFacturas"" df
                        INNER JOIN  productos p ON p.""idProducto"" = df.""idProducto""
                        INNER JOIN ivas i ON i.""idIva"" = df.""idIva""
                        WHERE ""idFactura""=@idFactura;
                       ";
                factura.detalleFactura = await _dapper.QueryAsync<DetalleDto>(sql, new { idFactura });
                sql = @"SELECT * FROM ""informacionAdicional"" WHERE ""idFactura""=@idFactura; ";
                factura.informacionAdicional = await _dapper.QueryAsync<informacionAdicionalDto>(sql, new { idFactura });
                return factura;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task<factura_V1_0_0?> _Factura_V1_0_0(string claveAcceso)
        {
            try
            {
                var _factura = await _Facturas(claveAcceso);
                var _facturaDto = await _FacturaDto(claveAcceso);
                var factura = new factura_V1_0_0();
                var totalImpuestoList = new List<totalImpuesto_V1_0_0>();
                var totalImpuesto = new totalImpuesto_V1_0_0();
                totalImpuesto.codigo = 2;
                totalImpuesto.codigoPorcentaje = 3;
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
                factura.infoTributaria.agenteRetencion = _factura.AgenteRetencion == true ? factura.infoTributaria.agenteRetencion = "1" : null;

                return factura;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task<XDocument> generarXml(string claveAcceso)
        {
            try
            {
                var _facturaDto = await _FacturaDto(claveAcceso);
                var _factura = await _Facturas(claveAcceso);
                var factura = await _Factura_V1_0_0(claveAcceso);
                XmlSerializerNamespaces serialize = new XmlSerializerNamespaces();
                serialize.Add("", "");
                XmlSerializer oXmlSerializar = new XmlSerializer(typeof(factura_V1_0_0));
                string xmlFactura = "";
                using (var stream = new StringWriter())
                {
                    using (XmlWriter writter = XmlWriter.Create(stream))
                    {
                        oXmlSerializar.Serialize(writter, factura, serialize);

                        xmlFactura = stream.ToString();
                    }
                }
                XDocument doc = XDocument.Parse(xmlFactura);
                doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                return doc;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public async Task<XmlDocument?> firmarXml(string claveAcceso)
        {
            try
            {
                var documento = await generarXml(claveAcceso);
                var consultaFactura = await _Facturas(claveAcceso);
                if (consultaFactura == null) return null;
                string sql = @"SELECT i.codigo,i.ruta FROM Facturas f
                              INNER JOIN ""usuarioEmpresas"" ue ON ue.""idUsuario""=f.""idUsuario""
                              INNER JOIN ""empresas"" e ON e.""idEmpresa""=ue.""idEmpresa""
                              INNER JOIN ""informacionFirmas"" i ON i.""identificacion""=e.""identificacion""
                              WHERE f.""claveAcceso""=@claveAcceso";
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




        public async Task<byte[]> imprimirComprobante(string claveAcceso, ControllerContext cc)
        {
            try
            {
                var consultaFactura = await _context.Facturas
                    .Include(x => x.DetalleFacturas)
                    .ThenInclude(x=>x.IdProductoNavigation)
                    .ThenInclude(x=>x.IdIvaNavigation)
                    .Include(x => x.DetalleFormaPagos)
                    .ThenInclude(x=>x.IdFormaPagoNavigation)
                    .Where(x => x.ClaveAcceso == claveAcceso).FirstOrDefaultAsync();
                    
                    ;
                if (consultaFactura == null) return null;


                var pdf = new ViewAsPdf("~/Views/Factura/ReciboFacturaV1_1_0.cshtml", new { consultaFactura })
                {
                    PageWidth = 80,
                    PageHeight = 297,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    CustomSwitches = "--margin-top 0 --margin-right 0 --margin-bottom 0 --margin-left 0"
                };
                return await pdf.BuildFile(cc);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<bool> enviarSri(string claveAcceso)
        {
            try
            {
                var xmlFirmado = await firmarXml(claveAcceso);
                if (!await _IUtilidades.envioXmlSriComprobacion(xmlFirmado)) throw new Exception("Error al enviar al SRI");
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        public string getBarcode(string claveAcceso)
        {
            try
            {
                var bar = new Barcode(claveAcceso);
                return bar.GetBase64Image();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<byte[]> generaRide(ActionContext ac, string claveAcesso,bool proforma)
        {
            try
            {
                var factura_V1_0_0 = await _Factura_V1_0_0(claveAcesso);
                var barCode = getBarcode(claveAcesso);
                var pdf = new ViewAsPdf(proforma? "~/Views/Factura/Proforma.cshtml":"~/Views/Factura/FacturaV1_1_0.cshtml", new { factura_V1_0_0, barCode });
                return await pdf.BuildFile(ac);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
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
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> enviarCorreo(string email, byte[] archivo, string nombreArchivo)
        {
            var xml = await firmarXml(nombreArchivo);
            return await _IUtilidades.envioCorreo(email, archivo, Encoding.ASCII.GetBytes(xml.OuterXml), nombreArchivo);
        }
    }
}