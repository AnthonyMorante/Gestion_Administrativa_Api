using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturas _IFacturas;
        private readonly IUtilidades _IUtilidades;
        private readonly _context _context;
        private readonly IDbConnection _dapper;

        public FacturasController(IFacturas IFacturas, _context context, IUtilidades IUtilidades)
        {
            _IFacturas = IFacturas;
            _context = context;
            _IUtilidades = IUtilidades;
            _dapper = context.Database.GetDbConnection();
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
                                pe.""nombre"" AS ""puntoEmision"",td.codigo,f.correoEnviado
                                FROM facturas f
                                INNER JOIN ""tipoEstadoSri"" tes ON tes.""idTipoEstadoSri"" = f.""idTipoEstadoSri""
                                INNER JOIN ""tipoEstadoDocumentos"" ted ON ted.""idTipoEstadoDocumento"" = f.""idTipoEstadoDocumento""
                                INNER JOIN ""establecimientos"" e ON e.""idEstablecimiento"" = f.""idEstablecimiento""
                                INNER JOIN ""puntoEmisiones"" pe ON pe.""idPuntoEmision"" = f.""idPuntoEmision""
                                INNER JOIN ""tipoDocumentos"" td ON td.""codigo"" = f.""tipoDocumento""
                                WHERE (DATEPART(year, f.""fechaEmision"") - DATEPART(year, getdate())) * 12 +
                                (DATEPART(MONTH, f.""fechaEmision"") - DATEPART(MONTH, getdate()))<=3
                                AND e.""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)";
                return Ok(await Tools.DataTableSql(new Tools.DataTableParams
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> secuenciales()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @" SELECT s.nombre,s.""idTipoDocumento""
                                FROM secuenciales s
                                INNER JOIN ""tipoDocumentos"" td ON s.""idTipoDocumento"" = td.""idTipoDocumento""
                                WHERE s.activo =1 AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                                UNION ALL
                                SELECT nombre,""idTipoDocumento"" FROM ""secuencialesProformas""
                                WHERE ""activo""= 1
                                AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
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
                var res = await _IFacturas.guardar(_facturaDto);
                if (_facturaDto.idDocumentoEmitir == Guid.Parse("6741a8d2-2e5b-4281-b188-c04e2c909049")) res = Ok("proforma");
                return res;
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
                var enviado = await _IFacturas.enviarSri(claveAcceso);
                if (enviado)
                {
                    var factura = await (from item in _context.Facturas
                                         where item.ClaveAcceso == claveAcceso
                                         select new
                                         {
                                             item.IdTipoEstadoSri,
                                             item.ReceptorCorreo,
                                             item.CorreoEnviado,
                                             item.TipoDocumento
                                         }).FirstOrDefaultAsync();
                    
                    var estado = await _IUtilidades.verificarEstadoSRI(claveAcceso);

                    string sqlU = $@"UPDATE facturas SET ""idTipoEstadoSri""=@idTipoEstadoSri
                                    WHERE ""claveAcceso"" = @claveAcceso;
                                    ";
                    if (estado.idTipoEstadoSri == 2)
                    {
                        if (factura.CorreoEnviado == false)
                        {
                            try
                            {
                                var ride = await _IFacturas.generaRide(ControllerContext, claveAcceso,factura.TipoDocumento==0);
                                await _IFacturas.enviarCorreo(factura.ReceptorCorreo, ride, claveAcceso);
                                sqlU += @"UPDATE facturas SET ""correoEnviado""=1,""fechaAutorizacion""=@fechaAutorizacion WHERE ""claveAcceso"" =@claveAcceso;";
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }
                        }
                    }
                    await _dapper.ExecuteAsync(sqlU, new { claveAcceso, estado.fechaAutorizacion, estado.idTipoEstadoSri });
                }
                else
                {
                    string sql = @"UPDATE facturas SET ""idTipoEstadoSri""=5 WHERE ""claveAcceso""=@claveAcceso";
                    await _dapper.ExecuteAsync(sql, new { claveAcceso });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return Ok();
            }
        }

        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> descargarXml(string claveAcceso)
        {
            try
            {
                var xmlFirmado = await _IFacturas.firmarXml(claveAcceso);
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
                var proforma=await _context.Facturas.AsNoTracking().Where(x=>x.ClaveAcceso== claveAcceso).Select(x=>x.TipoDocumento).FirstOrDefaultAsync()==0;
                var pdfBytes = await _IFacturas.generaRide(ControllerContext, claveAcceso,proforma);
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
            try
            {
                _facturaDto.idEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var empresa = await _context.Clientes.AsNoTracking().Where(x => x.IdEmpresa == _facturaDto.idEmpresa).FirstOrDefaultAsync();
                string sql = @"SELECT * FROM ""clientes"" WHERE ""identificacion""=@identificacion AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)";
                string ciudadDefecto = "Pelileo";
                var cliente = await _dapper.QueryFirstOrDefaultAsync<Clientes>(sql, _facturaDto);
                if (cliente == null)
                {
                    cliente = new Clientes();
                    sql = $@"SELECT TOP 1 ""idCiudad""
                        FROM ""ciudades""
                        WHERE upper(""nombre"") LIKE '%{ciudadDefecto.ToUpper()}%'
                        AND ""activo""=1";
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
                        WHERE ""idEstablecimiento""=@idEstablecimiento
                        ";
                _facturaDto.establecimiento = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
                sql = @"SELECT ""nombre""
                    FROM ""puntoEmisiones""
                    WHERE ""idPuntoEmision""=@idPuntoEmision
                    ";
                _facturaDto.puntoEmision = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
                sql = @"SELECT ""nombre""
                    FROM ""secuenciales""
                    WHERE ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER) AND ""idTipoDocumento"" IN (SELECT ""idTipoDocumento"" FROM ""documentosEmitir"" WHERE ""idDocumentoEmitir""=@idDocumentoEmitir)
                    ";
                _facturaDto.secuencial = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D9");
                _facturaDto.idUsuario = new Guid(Tools.getIdUsuario(HttpContext));
                _facturaDto.idFormaPago = _facturaDto.formaPago.FirstOrDefault().idFormaPago;
                sql = @"SELECT codigo FROM clientes c
                   INNER JOIN ""tipoIdentificaciones"" ti ON ti.""idTipoIdentificacion""=c.""idTipoIdentificacion""
                   WHERE ""idCliente""=@idCliente;";
                _facturaDto.codigoTipoIdentificacion = await _dapper.ExecuteScalarAsync<int>(sql, _facturaDto);
                return _facturaDto;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<string> generarRide(string claveAcceso, string email)
        {
            try
            {
                var proforma = await _context.Facturas.AsNoTracking().Where(x => x.ClaveAcceso == claveAcceso).Select(x => x.TipoDocumento).FirstOrDefaultAsync() == 0;
                var consulta = await _IFacturas.generaRide(ControllerContext, claveAcceso,proforma);
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
                            AND activo=1
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
                                WHERE ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                                ORDER BY ~ predeterminado;
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
                                WHERE ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                                ORDER BY ~ predeterminado;

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
                               WHERE ""activo""=1
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
                               WHERE ""activo""=1
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
                                AND dt.""idDetallePrecioProducto"" in(SELECT TOP 1 ""idDetallePrecioProducto"" FROM ""detallePrecioProductos"" dpp
                                WHERE dpp.""idProducto""=dt.""idProducto""
                                ORDER BY dpp.""fechaRegistro"" ASC)
                                WHERE p.""activo""=1
                                AND p.""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                                ORDER BY p.nombre;
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
                                WHERE p.""activo"" =1 AND dp.""activo""=1
                                AND p.""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)

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
                            AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER);
                            ";
                return Ok(await _dapper.QueryFirstOrDefaultAsync(sql, new { idEmpresa, identificacion }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult verificarEstados()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT COUNT(""claveAcceso"")
                                FROM facturas f
                                INNER JOIN ""usuarioEmpresas"" ue ON ue.""idUsuario"" = f.""idUsuario""
                                WHERE (""idTipoEstadoSri"" NOT IN(2,3,4,5) OR (""correoEnviado""=0 AND ""idTipoEstadoSri""=2))
                                AND ""idEmpresa""= CAST(@idEmpresa AS UNIQUEIDENTIFIER)";
                if (_dapper.ExecuteScalar<int>(sql, new { idEmpresa }) == 0) return Ok("empty");
                sql = @"SELECT ""claveAcceso""
                              FROM facturas f
                              INNER JOIN establecimientos e ON e.""idEstablecimiento"" = f.""idEstablecimiento""
                              WHERE ""idTipoEstadoSri"" IN (1,6,0) OR (""correoEnviado""=0 AND ""idTipoEstadoSri""=2)
                              AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                            ;
                            ";
                var lista = _dapper.Query<string>(sql, new { idEmpresa });
                if (lista.Count() == 0) return Ok("empty");
                string sqlA = "";
                foreach (var claveAcceso in lista)
                {
                    try
                    {
                        var factura = (from item in _context.Facturas
                                       where item.ClaveAcceso == claveAcceso
                                       select new
                                       {
                                           item.IdTipoEstadoSri,
                                           item.ReceptorCorreo,
                                           item.CorreoEnviado,
                                           item.TipoDocumento
                                       }).AsNoTracking().FirstOrDefault();
                        if (factura.IdTipoEstadoSri == 0 || factura.IdTipoEstadoSri == null)
                        {
                            var enviado = _IFacturas.enviarSri(claveAcceso).Result;
                            if (enviado == true)
                            {
                                string sqlEnvio = $@"UPDATE facturas SET ""idTipoEstadoSri""=6
                                                           WHERE ""claveAcceso"" = @claveAcceso;";
                                _dapper.Execute(sqlEnvio, new { claveAcceso });
                            }
                        }
                        else
                        {
                            if (factura.TipoDocumento != 0)
                            {
                                var estado = _IUtilidades.verificarEstadoSRI(claveAcceso).Result;
                                sqlA = $@"UPDATE facturas SET ""idTipoEstadoSri""=@idTipoEstadoSri
                                      WHERE ""claveAcceso"" = @claveAcceso;";
                                if (estado.idTipoEstadoSri == 2)
                                {
                                    if (factura.CorreoEnviado == false)
                                    {
                                        try
                                        {
                                            var ride = _IFacturas.generaRide(ControllerContext, claveAcceso, factura.TipoDocumento == 0).Result;
                                            var correoEnviado = _IFacturas.enviarCorreo(factura.ReceptorCorreo, ride, claveAcceso).Result;
                                            if (correoEnviado)
                                            {
                                                sqlA += @"UPDATE facturas SET ""correoEnviado""=1,""fechaAutorizacion""=@fechaAutorizacion WHERE ""claveAcceso"" =@claveAcceso;";
                                                _dapper.Execute(sqlA, new { claveAcceso, estado.fechaAutorizacion, estado.idTipoEstadoSri });
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    var ride = _IFacturas.generaRide(ControllerContext, claveAcceso, factura.TipoDocumento == 0).Result;
                                    var correoEnviado = _IFacturas.enviarCorreo(factura.ReceptorCorreo, ride, claveAcceso).Result;
                                    if (correoEnviado)
                                    {
                                        sqlA = @"UPDATE facturas SET ""correoEnviado""=1 WHERE ""claveAcceso"" =@claveAcceso;";
                                        _dapper.Execute(sqlA, new { claveAcceso});
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    continue;
                                }
                            }
     
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}