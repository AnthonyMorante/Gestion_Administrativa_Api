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
        public async Task<IActionResult> unDato(string claveAcceso)
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

                return Ok(await _dapper.QueryFirstAsync(sql, new { claveAcceso }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [AllowAnonymous]
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
