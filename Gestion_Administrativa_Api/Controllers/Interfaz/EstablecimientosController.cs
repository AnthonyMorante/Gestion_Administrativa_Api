using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablecimientosController : ControllerBase
    {
        private readonly IEstablecimientos _IEstablecimientos;

        public EstablecimientosController(IEstablecimientos IEstablecimientos)
        {
            _IEstablecimientos = IEstablecimientos;
        }

        [HttpGet]
        [Route("[action]/{idEmpresa}")]
        public async Task<IActionResult> listar(Guid idEmpresa)
        {
            try
            {
                var consulta = await _IEstablecimientos.listar(idEmpresa);
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }
    }
}