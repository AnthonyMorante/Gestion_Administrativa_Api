using Gestion_Administrativa_Api.Interfaces;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClientes _IClientes;

        public ClientesController(IClientes IClientes)
        {

            _IClientes = IClientes;

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> insertar(Clientes _clientes)
        {

            try
            {

                var consulta = await _IClientes.insertar(_clientes);
                 return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



    }
}
