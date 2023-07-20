using Gestion_Administrativa_Api.Interfaces;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionesController : ControllerBase
    {

        private readonly ITipoIdentificaciones _ITipoIdentificaciones;

        public TipoIdentificacionesController(ITipoIdentificaciones ITipoIdentificaciones)
        {

            _ITipoIdentificaciones = ITipoIdentificaciones;

        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar()
        {

            try
            {

                var consulta = await _ITipoIdentificaciones.listar();
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }

    }
}

