using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Gestion_Administrativa_Api.Interfaces.Interfaz.IpuntoEmisiones;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntoEmisionesController : ControllerBase
    {


        private readonly IPuntoEmisiones _IPuntoEmisiones;

        public PuntoEmisionesController(IPuntoEmisiones IPuntoEmisiones)
        {

            _IPuntoEmisiones = IPuntoEmisiones;

        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar(PuntoEmisiones _establecimientos)
        {

            try
            {

                var consulta = await _IPuntoEmisiones.listar(_establecimientos);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }
    }
}
