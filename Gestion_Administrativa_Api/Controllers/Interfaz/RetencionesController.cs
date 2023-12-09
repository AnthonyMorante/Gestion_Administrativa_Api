using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
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



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> insertar(RetencionDto? _retencionDto)
        {


            try
            {

                return Ok();

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }



        }

    }
}
