using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Net.Http;
using System.Text;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturas _IFacturas;
        private readonly IDbConnection _dapper;
        private readonly _context _context;

        public FacturasController(IFacturas IFacturas, IDbConnection db, _context context)
        {
            _IFacturas = IFacturas;
            _dapper = db;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT f.""idFactura"",""ambiente"",""claveAcceso"",""secuencial"",
                                f.""receptorRazonSocial"" AS ""cliente"",tes.""nombre"" AS ""estadoSri"",
                                ted.""nombre"" AS estado,f.""fechaRegistro"",f.""fechaEmision"",f.""fechaAutorizacion"",
                                f.""receptorTelefono"" AS ""telefonoCliente"", f.""receptorCorreo"" AS ""emailCliente"",
                                f.""idTipoEstadoSri"",f.""idTipoEstadoDocumento"",""establecimiento"",f.""tipoEmision"",f.""idPuntoEmision"",
                                pe.""nombre"" AS ""puntoEmision""
                                FROM facturas f
                                INNER JOIN ""tipoEstadoSri"" tes ON tes.""idTipoEstadoSri"" = f.""idTipoEstadoSri""
                                INNER JOIN ""tipoEstadoDocumentos"" ted ON ted.""idTipoEstadoDocumento"" = f.""idTipoEstadoDocumento""
                                INNER JOIN ""establecimientos"" e ON e.""idEstablecimiento"" = f.""idEstablecimiento""
                                INNER JOIN ""puntoEmisiones"" pe ON pe.""idPuntoEmision"" = f.""idPuntoEmision""
                                WHERE (DATE_PART('year', f.""fechaEmision""::date) - DATE_PART('year', current_date::date)) * 12 +
                                (DATE_PART('month', f.""fechaEmision""::date) - DATE_PART('month', current_date::date))<=3
                                AND e.""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY ""secuencial"" desc";
                return Ok(await Tools.DataTablePostgresSql(new Tools.DataTableParams
                {
                    parameters = new { idEmpresa },
                    query = sql,
                    dapperConnection = _dapper,
                    dataTableModel = _params
                }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> secuenciales()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @" SELECT ""nombre"",""idTipoDocumento"" FROM secuenciales
                                WHERE ""activo""= TRUE
                                AND ""idEmpresa""=uuid(@idEmpresa)
                                UNION ALL
                                SELECT nombre,""idTipoDocumento"" FROM ""secuencialesProformas""
                                WHERE ""activo""= TRUE
                                AND ""idEmpresa""=uuid(@idEmpresa)
                                ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> insertar(FacturaDto? _facturaDto)
        {
            try
            {
                _facturaDto = await procesarFactura(_facturaDto);
                if (_facturaDto.idDocumentoEmitir == Guid.Parse("6741a8d2-2e5b-4281-b188-c04e2c909049"))
                {
                    var proforma = await _IFacturas.guardar(_facturaDto);
                    return Ok("proforma");
                }
                var consulta = await _IFacturas.guardar(_facturaDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> reenviar(string claveAcceso)
        {
            try
            {
               var enviado= await _IFacturas.enviarSri(claveAcceso);

                if (enviado)
                {
                    var listaEstados = new List<dynamic>();
                    listaEstados.Add(new
                    {
                        estado = "<estado>NO AUTORIZADO</estado>",
                        codigo = 3
                    });
                    listaEstados.Add(new
                    {
                        estado = "<estado>AUTORIZADO</estado>",
                        codigo = 2
                    });
                    listaEstados.Add(new
                    {
                        estado = "<numeroComprobantes>0</numeroComprobantes>",
                        codigo = 0
                    });
                    var xml = await _IFacturas.firmarXml(claveAcceso);
                    var content = new StringContent(xml.InnerXml, Encoding.ASCII, "text/xml");
                    var httpClient = new HttpClient();
                    var peticion = await httpClient.PostAsync(Tools.config["SRI:urlEstado"], content);
                    peticion.EnsureSuccessStatusCode();
                    var consulta = await peticion.Content.ReadAsStringAsync();
                    foreach (var estado in listaEstados)
                    {
                        string sql= $@"UPDATE facturas SET ""idTipoEstadoSri""={estado.codigo}
                                    WHERE ""claveAcceso"" = '{claveAcceso}';
                                    ";
                        if (estado.codigo == 2)
                        {

                            sql+= @"SELECT ""correoEnviado""  FROM facturas 
                                            WHERE ""claveAcceso"" =@claveAcceso";
                            if (!(await _dapper.ExecuteScalarAsync<bool>(sql, new { claveAcceso })))
                            {
                                try
                                {
                                    sql += @"SELECT ""receptorCorreo""  FROM facturas 
                                            WHERE ""claveAcceso"" =@claveAcceso";
                                    var email = await _dapper.ExecuteScalarAsync<string>(sql, new { claveAcceso });
                                    var f100 = await _IFacturas._Factura_V1_0_0(claveAcceso);
                                    var ride = await _IFacturas.generaRide(ControllerContext, claveAcceso);
                                    await _IFacturas.enviarCorreo(email, ride, claveAcceso);
                                    sql+=  @"UPDATE facturas SET ""correoEnviado""=TRUE WHERE ""claveAcceso"" =@claveAcceso";
                                }
                                catch (Exception ex)
                                {
                                    await Console.Out.WriteLineAsync(ex.Message);
                                    continue;
                                }
                            }

                        }
                        await _dapper.ExecuteAsync(sql, new { claveAcceso });
                    }
                }
                else
                {
                    string sql = @"UPDATE facturas SET ""idTipoEstadoSri""=6 WHERE ""claveAcceso""=@claveAcceso";
                    await _dapper.ExecuteAsync(sql, new { claveAcceso });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLineAsync(ex.Message);
                return Ok();
            }
        }
        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> descargarXml(string claveAcceso)
        {
            try
            {
                var xmlFirmado=await _IFacturas.firmarXml(claveAcceso);
                string linea = xmlFirmado.InnerXml;
                var xmlByte = Encoding.UTF8.GetBytes(linea);
                var archivo = new FileContentResult(xmlByte, "application/xml");
                archivo.FileDownloadName = $"REPORTE_{DateTime.Now.Ticks}.xml";
                return archivo;
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> descargarPdf(string claveAcceso)
        {
            try
            {
                var pdfBytes = await _IFacturas.generaRide(ControllerContext, claveAcceso);
                var archivo = new FileContentResult(pdfBytes, "application/pdf");
                archivo.FileDownloadName = $"REPORTE_{DateTime.Now.Ticks}.pdf";
                return archivo;
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        private async Task<FacturaDto> procesarFactura(FacturaDto? _facturaDto)
        {
            _facturaDto.idEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
            var empresa = await _context.Clientes.AsNoTracking().Where(x => x.IdEmpresa == _facturaDto.idEmpresa).FirstOrDefaultAsync();
            string sql = @"SELECT * FROM ""clientes"" WHERE ""identificacion""=@identificacion AND ""idEmpresa""=uuid(@idEmpresa)";
            string ciudadDefecto = "Pelileo";
            var cliente = await _dapper.QueryFirstOrDefaultAsync<Clientes>(sql, _facturaDto);
            if (cliente == null)
            {
                cliente = new Clientes();
                sql = $@"SELECT ""idCiudad""
                        FROM ""ciudades""
                        WHERE upper(""nombre"") LIKE '%{ciudadDefecto.ToUpper()}%'
                        AND ""activo""=TRUE
                        LIMIT 1";
                cliente.IdCiudad = await _dapper.ExecuteScalarAsync<Guid>(sql);
                _facturaDto.idCiudad = Tools.toGuid(cliente.IdCiudad);
                cliente.Activo = true;
                cliente.IdTipoIdentificacion = _facturaDto.idTipoIdenticacion;
                cliente.RazonSocial = _facturaDto.razonSocial;
                cliente.FechaRegistro = DateTime.Now;
                cliente.Direccion = _facturaDto.direccion;
                cliente.Identificacion = _facturaDto.identificacion;
                cliente.Telefono = _facturaDto.telefono;
                cliente.Email = _facturaDto.email;
                cliente.IdEmpresa = _facturaDto.idEmpresa;
                _context.Clientes.Add(cliente);
            }
            else
            {
                cliente = await _context.Clientes.Where(x => x.IdEmpresa == _facturaDto.idEmpresa && x.Identificacion == _facturaDto.identificacion).FirstOrDefaultAsync();
                cliente.RazonSocial = _facturaDto.razonSocial;
                cliente.Direccion = _facturaDto.direccion;
                cliente.Telefono = _facturaDto.telefono;
                cliente.Email = _facturaDto.email;
                _facturaDto.idCiudad = Tools.toGuid(cliente.IdCiudad);
                _context.Clientes.Update(cliente);
            }

            await _context.SaveChangesAsync();
            _facturaDto.idCliente = cliente.IdCliente;
            _facturaDto.idDocumentoEmitir = (await _context.DocumentosEmitir.AsNoTracking().Where(x => x.IdTipoDocumento == _facturaDto.idTipoDocumento).FirstOrDefaultAsync()).IdDocumentoEmitir;
            sql = @"SELECT ""nombre""
                        FROM ""establecimientos""
                        WHERE ""idEstablecimiento""=uuid(@idEstablecimiento)
                        ";
            _facturaDto.establecimiento = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
            sql = @"SELECT ""nombre""
                    FROM ""puntoEmisiones""
                    WHERE ""idPuntoEmision""=uuid(@idPuntoEmision)
                    ";
            _facturaDto.puntoEmision = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
            sql = @"SELECT ""nombre""
                    FROM ""secuenciales""
                    WHERE ""idEmpresa""=uuid(@idEmpresa)
                    ";
            _facturaDto.secuencial = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D9");
            _facturaDto.idUsuario = new Guid(Tools.getIdUsuario(HttpContext));
            _facturaDto.idFormaPago = _facturaDto.formaPago.FirstOrDefault().idFormaPago;
            sql = @"SELECT codigo FROM clientes c
                   INNER JOIN ""tipoIdentificaciones"" ti ON ti.""idTipoIdentificacion""=c.""idTipoIdentificacion""
                   WHERE ""idCliente""=uuid(@idCliente);";
            _facturaDto.codigoTipoIdentificacion = await _dapper.ExecuteScalarAsync<int>(sql, _facturaDto);
            return _facturaDto;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<string> generarRide(string claveAcceso, string email)
        {
            try
            {
                var consulta = await _IFacturas.generaRide(ControllerContext, claveAcceso);
                return "ok";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        [HttpGet]
        public async Task<IActionResult> tiposDocumentos()
        {
            try
            {
                string sql = @"
                            SELECT * FROM ""tipoDocumentos""
                            WHERE codigo in(1,0)
                            AND activo=true
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> puntosEmisiones()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""puntoEmisiones""
                                WHERE ""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY NOT predeterminado;
                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> establecimientos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""establecimientos""
                                WHERE ""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY NOT predeterminado;

                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> formaPagos()
        {
            try
            {
                string sql = @"SELECT * FROM ""formaPagos""
                               WHERE ""activo""=true
                               ORDER BY codigo;
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> tiempoFormaPagos()
        {
            try
            {
                string sql = @"SELECT * FROM ""tiempoFormaPagos""
                               WHERE ""activo""=true
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> listaProductos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT p.""idProducto"", p.codigo, p.nombre, p.descripcion, p.activo,
                                ROUND((dt.""totalIva""+total)/ (1+i.""valor""),2) AS ""precio"",
                                p.""fechaRegistro"", dt.""idIva"", ""idEmpresa"", ""activoProducto"", dt.""totalIva""+dt.total AS ""totalIva"", cantidad,
                                i.valor AS ""iva"",i.nombre AS ""nombreIva"" ,i.""descripcion"" as ""tarifaPorcentaje""
                                FROM productos p
                                INNER JOIN ""detallePrecioProductos"" dt ON dt.""idProducto"" =p.""idProducto""
                                INNER JOIN ""ivas"" i ON i.""idIva"" = dt.""idIva""
                                AND dt.""idDetallePrecioProducto"" in(SELECT ""idDetallePrecioProducto"" FROM ""detallePrecioProductos"" dpp
                                WHERE dpp.""idProducto""=dt.""idProducto""
                                ORDER BY dpp.""fechaRegistro"" ASC
                                LIMIT 1)
                                WHERE p.""activo""=TRUE
                                AND p.""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY UPPER(REPLACE(regexp_replace(CAST(p.""nombre"" AS varchar),'\t|\n|\r|\s',''),' ',''));
                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> listaPreciosProductos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @" SELECT ""idDetallePrecioProducto"",dp.""idProducto"",p.""nombre"" AS ""producto"",
                                p.""codigo"" AS ""codigoProducto"",ROUND((dp.""totalIva""+total)/ (1+i.""valor""),2) AS ""precio"",
                                i.""codigo"" AS ""codigoIva"",
                                i.""idIva"",i.""nombre"" AS ""nombreIva"",i.""valor"" as iva,i.""descripcion"" as ""tarifaPorcentaje""
                                FROM ""detallePrecioProductos"" dp
                                INNER JOIN ""productos"" p ON dp.""idProducto"" = p.""idProducto""
                                INNER JOIN ""ivas"" i ON i.""idIva"" = dp.""idIva""
                                WHERE p.""activo"" =TRUE AND dp.""activo""=true
                                AND p.""idEmpresa""=uuid(@idEmpresa)

                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{identificacion}")]
        public async Task<IActionResult> buscarCliente(string identificacion)
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""clientes""
                            WHERE ""identificacion""=@identificacion
                            AND ""idEmpresa""=uuid(@idEmpresa);
                            ";
                return Ok(await _dapper.QueryFirstOrDefaultAsync(sql, new { idEmpresa, identificacion }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> verificarEstados()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT ""claveAcceso""
                              FROM facturas f
                              INNER JOIN establecimientos e ON e.""idEstablecimiento"" = f.""idEstablecimiento""
                              WHERE ""idTipoEstadoSri"" IN (1,6,0) OR ""correoEnviado""=FALSE
                              AND ""idEmpresa""=uuid(@idEmpresa)
                            ;
                            ";
                var lista = await _dapper.QueryAsync<string>(sql, new { idEmpresa });
                if (lista.Count() == 0) return Ok("empty");
                var listaEstados = new List<dynamic>();
                listaEstados.Add(new
                {
                    estado = "<estado>NO AUTORIZADO</estado>",
                    codigo = 3
                });
                listaEstados.Add(new
                {
                    estado = "<estado>AUTORIZADO</estado>",
                    codigo = 2
                });                
                listaEstados.Add(new
                {
                    estado = "<numeroComprobantes>0</numeroComprobantes>",
                    codigo = 0
                });
                string sqlA = "";
                foreach (var claveAcceso in lista)
                {
                    var xml = $@"
                                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.autorizacion"">
                                       <soapenv:Header/>
                                       <soapenv:Body>
                                          <ec:autorizacionComprobante>
                                             <!--Optional:-->
                                             <claveAccesoComprobante>{claveAcceso}</claveAccesoComprobante>
                                          </ec:autorizacionComprobante>
                                       </soapenv:Body>
                                    </soapenv:Envelope>
                                    ";
                    try
                    {
                        var content = new StringContent(xml, Encoding.ASCII, "text/xml");
                        var httpClient = new HttpClient();
                        var peticion = await httpClient.PostAsync(Tools.config["SRI:urlEstado"], content);
                        peticion.EnsureSuccessStatusCode();
                        var consulta = await peticion.Content.ReadAsStringAsync();
                        foreach (var estado in listaEstados)
                        {
                            if (consulta.Contains(estado.estado))
                            {
                                sqlA += $@"UPDATE facturas SET ""idTipoEstadoSri""={estado.codigo}
                                    WHERE ""claveAcceso"" = '{claveAcceso}';
                                    ";
                                if (estado.codigo==2)
                                {
                                    
                                    sql = @"SELECT ""correoEnviado""  FROM facturas 
                                            WHERE ""claveAcceso"" =@claveAcceso";
                                    if(!(await _dapper.ExecuteScalarAsync<bool>(sql, new { claveAcceso })))
                                    {
                                        try
                                        {
                                            sql+= @"SELECT ""receptorCorreo""  FROM facturas 
                                            WHERE ""claveAcceso"" =@claveAcceso";
                                            var email = await _dapper.ExecuteScalarAsync<string>(sql, new { claveAcceso });
                                            var f100 = await _IFacturas._Factura_V1_0_0(claveAcceso);
                                            var ride = await _IFacturas.generaRide(ControllerContext, claveAcceso);
                                            await _IFacturas.enviarCorreo(email, ride, claveAcceso);
                                            sql+= @"UPDATE facturas SET ""correoEnviado""=TRUE WHERE ""claveAcceso"" =@claveAcceso";
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                            continue;
                                        }
                                    }
                       
                                }
                                await _dapper.ExecuteAsync(sql, new { claveAcceso });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.Message);
                        continue;
                    }
                }
                if (sqlA != "") await _dapper.ExecuteAsync(sqlA);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}