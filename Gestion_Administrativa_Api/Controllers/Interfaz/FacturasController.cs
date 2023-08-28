using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

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
        public async Task<IActionResult> insertar(FacturaDto? _facturaDto)
        {

            try
            {


                var consulta = await _IFacturas.guardar(_facturaDto);
                var ride = await generarRide(consulta,_facturaDto.email);
                return Ok("ok");


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<string> generarRide(factura_V1_0_0 _factura, string email)
        {

            try
            {  
                var consulta = await _IFacturas.generaRide(ControllerContext,_factura,email);
                return "ok";

            }
            catch (Exception ex)
            {
                return "false";
           
            }

        }



    }
}
