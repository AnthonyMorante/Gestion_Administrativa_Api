using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuencialesController : ControllerBase
    {



        private readonly ISecuenciales _ISecuenciales;

        public SecuencialesController(ISecuenciales ISecuenciales)
        {

            _ISecuenciales = ISecuenciales;

        }



        [HttpGet]
        [Route("[action]/{idEmpresa}")]
        public async Task<IActionResult> listar(Guid idEmpresa)
        {

            try
            {

                var consulta = await _ISecuenciales.listar(idEmpresa);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


    }
}
