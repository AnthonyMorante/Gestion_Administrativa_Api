using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagosController : ControllerBase
    {


        private readonly IFormaPagos _IFormaPagos;

        public FormaPagosController(IFormaPagos IFormaPagos)
        {

            _IFormaPagos = IFormaPagos;

        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar()
        {
            try
            {

                var consulta = await _IFormaPagos.listar();
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }

    }
}
