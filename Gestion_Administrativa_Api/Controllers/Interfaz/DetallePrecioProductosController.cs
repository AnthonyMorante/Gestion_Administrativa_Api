using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePrecioProductosController : ControllerBase
    {

        private readonly IDetallePrecioProductos _IDetallePrecioProductos;

        public DetallePrecioProductosController(IDetallePrecioProductos IDetallePrecioProductos)
        {

            _IDetallePrecioProductos = IDetallePrecioProductos;

        }



        [HttpGet]
        [Route("[action]/{idProducto}")]
        public async Task<IActionResult> listar(Guid idProducto)
        {

            try
            {

                var consulta = await _IDetallePrecioProductos.listar(idProducto);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }

    }
}
