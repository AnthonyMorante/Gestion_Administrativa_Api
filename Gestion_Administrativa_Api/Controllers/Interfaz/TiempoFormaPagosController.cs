using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiempoFormaPagosController : ControllerBase
    {
        private readonly ITiempoFormaPagos _ITiempoFormaPagos;

        public TiempoFormaPagosController(ITiempoFormaPagos ITiempoFormaPagos)
        {
            _ITiempoFormaPagos = ITiempoFormaPagos;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar()
        {
            try
            {
                var consulta = await _ITiempoFormaPagos.listar();
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }
    }
}