using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {





        private readonly IFacturas _IFacturas;

        public FacturasController(IFacturas IFacturas)
        {

            _IFacturas = IFacturas;

        }





        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> insertar(FacturaDto ?_facturaDto)
        {

            try
            {


                var consulta = await _IFacturas.guardar(_facturaDto);


                return BadRequest();


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



    }
}
