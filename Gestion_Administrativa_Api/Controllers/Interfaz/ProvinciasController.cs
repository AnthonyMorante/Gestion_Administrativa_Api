using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {

        private readonly IProvincias _IProvincias;

        public ProvinciasController(IProvincias IProvincias)
        {

            _IProvincias = IProvincias;

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar()
        {

            try
            {

                var consulta = await _IProvincias.listar();
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


    }
}
