using Gestion_Administrativa_Api.Dtos.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]


        public async Task<IActionResult> insertar(FacturaDto ?_facturaDto)
        {

            try
            {


                return BadRequest();


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



    }
}
