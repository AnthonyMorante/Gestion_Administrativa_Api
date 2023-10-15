using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Authorization;
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
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}