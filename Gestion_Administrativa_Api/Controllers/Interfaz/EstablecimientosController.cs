using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Http;
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
        [Route("[action]")]
        public async Task<IActionResult> listar(Establecimientos _establecimientos)
        {

            try
            {

                var consulta = await _IEstablecimientos.listar(_establecimientos);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



    }



}
