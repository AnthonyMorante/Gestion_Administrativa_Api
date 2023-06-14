using Gestion_Administrativa_Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : ControllerBase
    {


        private readonly ICiudades _ICiudades;

        public CiudadesController(ICiudades ICiudades)
        {

            _ICiudades = ICiudades;

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar(Guid idProvincia)
        {

            try
            {

                var consulta = await _ICiudades.listar(idProvincia);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }
    }

}
