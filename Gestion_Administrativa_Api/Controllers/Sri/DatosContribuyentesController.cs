using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Sri;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Sri
{
    [Route("apiSri/[controller]")]
    [ApiController]
    public class DatosContribuyentesController : ControllerBase
    {


        private readonly IDatosContribuyentes _IDatosContribuyentes;

        public DatosContribuyentesController(IDatosContribuyentes iDatosContribuyentes)
        {

            _IDatosContribuyentes= iDatosContribuyentes;

        }



        [HttpGet]
        [Route("[action]/{identificacion}")]
        public async Task<IActionResult> consultarContribuyente(string identificacion)
        {

            try
            {

                var consulta = await _IDatosContribuyentes.consultarContribuyente(identificacion);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


    }
}
