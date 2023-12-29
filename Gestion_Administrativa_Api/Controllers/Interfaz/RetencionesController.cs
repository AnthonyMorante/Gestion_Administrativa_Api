using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gestion_Administrativa_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RetencionesController : ControllerBase
    {


        private readonly IRetenciones _IRetenciones;
        private readonly IDbConnection _dapper;
        private readonly _context _context;

        public RetencionesController(IRetenciones IRetenciones, IDbConnection db, _context context)
        {
            _IRetenciones = IRetenciones;
            _dapper = db;
            _context = context;
        }
        [Authorize]
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




        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> porcentajeImpuestosRetenciones()
        {
            try
            {

                string sql = @"
                        select ""idPorcentajeImpuestoRetencion"",nombre,valor,codigo,""idTipoValorRetencion""
                        from ""porcentajeImpuestosRetenciones"" pir 
                        where activo =true
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
                            where s.""idEmpresa"" =uuid(@idEmpresa)
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
                           
                                select sf.""idFactura"",sf.""totalSinImpuesto"",sf.""importeTotal"",sf.""totalDescuento"",
                                sf.""razonSocialComprador"",sf.""identificacionComprador"",sf.estab,sf.secuencial,sf.""ptoEmi"",
                                sf.""claveAcceso"" ,stci.""baseImponible"",stci.valor 
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

                var res = await _IRetenciones.guardar(_retencionDto);

                return Ok();

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }



        }

    }
}
