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
        [Route("[action]/{idEmpresa}")]
        public async Task<IActionResult> listar(Guid idEmpresa)
        {
            try
            {
                var consulta = await _IPuntoEmisiones.listar(idEmpresa);
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }
    }
}