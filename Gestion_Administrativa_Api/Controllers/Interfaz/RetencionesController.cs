using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace Gestion_Administrativa_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RetencionesController : ControllerBase
    {
        private readonly IRetenciones _IRetenciones;
        private readonly IDbConnection _dapper;
        private readonly _context _context;
        private readonly IUtilidades _IUtilidades;

        public RetencionesController(IRetenciones IRetenciones, _context context, IUtilidades iUtilidades)
        {
            _IRetenciones = IRetenciones;
            _dapper = context.Database.GetDbConnection();
            _context = context;
            _IUtilidades = iUtilidades;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> porcentajeImpuestosRetenciones()
        {
            try
            {
                string sql = @"
                        select ""idPorcentajeImpuestoRetencion"",nombre,valor,codigo,""idTipoValorRetencion""
                        from ""porcentajeImpuestosRetenciones"" pir
                        where activo =1
                            ";
                return Ok(await _dapper.QueryAsync(sql));
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

                string sql = @"
                            select td.""idTipoDocumento"",td.nombre,s.nombre  from secuenciales s
                            join ""tipoDocumentos"" td
                            on td.""idTipoDocumento"" = s.""idTipoDocumento""
                            where s.""idEmpresa"" =CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                            and td.codigo  ='7'
                              ";

                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> unDato(string claveAcceso, Int32 idFactura)
        {
            try
            {
                string sql = @"

                               select sf.""idUsuario"",sf.""idFactura"",sf.""totalSinImpuesto"",sf.""importeTotal"",sf.""totalDescuento"",
                               sf.""razonSocialComprador"",sf.""identificacionComprador"",sf.estab,sf.secuencial,sf.""ptoEmi"",
                               sf.""claveAcceso"" ,stci.""baseImponible"",stci.valor,sf.""fechaEmision"",sf.""tipoIdentificacionComprador"",
                               sf.""identificacionComprador""
                               from ""SriTotalesConImpuestos"" stci
                               join ""SriFacturas"" sf on sf.""idFactura"" = stci.""idFactura""
                                where sf.""claveAcceso""  = @claveAcceso

                              ";

                string sqlSubtotales = @"

                         SELECT i.codigo,sum(dt.""precioTotalSinImpuesto"") as sumatoria
                            FROM ""SriDetallesFacturas"" dt
                            INNER JOIN ""SriDetallesFacturasImpuestos"" i ON i.""idDetalleFactura"" = dt.""idDetalleFactura""
                            WHERE ""idFactura"" = @idFactura
                            GROUP BY i.codigo

                     ";

                var res = await _dapper.QueryFirstAsync(sql, new { claveAcceso });
                var resSubtotales = await _dapper.QueryAsync(sqlSubtotales, new { idFactura });
                return Ok(new { res, subtotales = resSubtotales });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> insertar(RetencionDto? _retencionDto)
        {
            try
            {
                _retencionDto.idEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var res = await _IRetenciones.guardar(_retencionDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> reenviar(string claveAcceso)
        {
            try
            {
                var res = await _IRetenciones.reenviar(claveAcceso);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> listarTipoIdentificaciones()
        {
            try
            {
                string sql = @"

                               select ""idTipoIdentificacion"",nombre,codigo
                               from ""tipoIdentificaciones"" ti

                              ";

                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }







        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"
	                              
                                    select sf.idEmpresa ,r.idRetencion ,sf.fechaEmision as fechaEmisionRetencion,sf.fechaRegistro ,
                                    sf.estab + '-' +sf.ptoEmi + '-' + sf.secuencial as documento,
                                    sf.razonSocialComprador,r.claveAcceso,r.idTipoEstadoSri ,
                                    sf.fechaAutorizacion,sp.telefono ,sp.email,tes.nombre as estadoSri 
                                    from retenciones r 
                                    join SriFacturas sf on sf.idFactura = r.idFactura
                                    join SriPersonas sp on sp.identificacion = sf.ruc 
                                    join tipoEstadoSri tes on tes.idTipoEstadoSri = r.idTipoEstadoSri 
	                                where sf.idEmpresa  = @idEmpresa
                             ";
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
                return Tools.handleError(ex);
            }
        }



        [HttpGet("{claveAcceso}")]
        public async Task<IActionResult> descargarXml(string claveAcceso)
        {
            try
            {
                var xmlFirmado = await _IRetenciones.descargarXml(claveAcceso);
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



        [HttpGet]
        public async Task<IActionResult>  verificarEstados()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @" SELECT COUNT(claveAcceso)
                                FROM retenciones f
                                INNER JOIN usuarioEmpresas ue ON ue.idUsuario = f.idUsuario
                                WHERE (idTipoEstadoSri NOT IN(2,3,4,5) OR (correoEnviado=0 AND idTipoEstadoSri=2))
                                AND f.idEmpresa=@idEmpresa";



                if (_dapper.ExecuteScalar<int>(sql, new { idEmpresa }) == 0) return Ok("empty");
                sql = @"
                              SELECT claveAcceso
                              FROM retenciones f
                              INNER JOIN establecimientos e ON e.idEstablecimiento = f.idEstablecimiento
                              WHERE idTipoEstadoSri IN (1,6,0) OR (correoEnviado=0 AND idTipoEstadoSri=2)
                              AND f.idEmpresa=@idEmpresa
                            
                            ";
                var lista = _dapper.Query<string>(sql, new { idEmpresa });
                if (lista.Count() == 0) return Ok("empty");
                string sqlA = "";
                foreach (var claveAcceso in lista)
                {
                    try
                    {
                        var retencion = await _context.Retenciones
                        .Where(x => x.ClaveAcceso == claveAcceso)
                        .Select(x => new
                        {
                            x.IdTipoEstadoSri,
                            x.ReceptorCorreo,
                            x.CorreoEnviado

                        })
                        .FirstOrDefaultAsync();

                        if (retencion.IdTipoEstadoSri == 0 || retencion.IdTipoEstadoSri == null)
                        {
                            var enviado = _IRetenciones.enviarSri(claveAcceso).Result;
                            if (enviado == true)
                            {
                                string sqlEnvio = $@"UPDATE retenciones SET ""idTipoEstadoSri""=6
                                                           WHERE ""claveAcceso"" = @claveAcceso;";
                                _dapper.Execute(sqlEnvio, new { claveAcceso });
                            }
                        }
                        else
                        {
                            var estado = _IUtilidades.verificarEstadoSRI(claveAcceso).Result;
                            sqlA = $@"UPDATE retenciones SET ""idTipoEstadoSri""=@idTipoEstadoSri
                                      WHERE ""claveAcceso"" = @claveAcceso;";
                            if (estado.idTipoEstadoSri == 2)
                            {
                                if (retencion.CorreoEnviado == false)
                                {
                                    //try
                                    //{
                                    //    var ride = _IFacturas.generaRide(ControllerContext, claveAcceso).Result;
                                    //    var correoEnviado = _IFacturas.enviarCorreo(factura.ReceptorCorreo, ride, claveAcceso).Result;
                                    //    if (correoEnviado)
                                    //    {
                                    //        sqlA += @"UPDATE facturas SET ""correoEnviado""=1,""fechaAutorizacion""=@fechaAutorizacion WHERE ""claveAcceso"" =@claveAcceso;";
                                    //        _dapper.Execute(sqlA, new { claveAcceso, estado.fechaAutorizacion, estado.idTipoEstadoSri });
                                    //    }
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    Console.WriteLine(ex.Message);
                                    //    continue;
                                    //}
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