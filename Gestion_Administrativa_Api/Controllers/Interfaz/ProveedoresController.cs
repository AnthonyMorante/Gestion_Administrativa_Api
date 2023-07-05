using Gestion_Administrativa_Api.Dtos;
using Gestion_Administrativa_Api.Interfaces;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {


        private readonly IProveedores _IProveedores;

        public ProveedoresController(IProveedores IProveedores)
        {

            _IProveedores = IProveedores;

        }

        [HttpPost]
        [Route("[action]")]


        public async Task<IActionResult> insertar(ProveedoresDto _clientes)
        {

            try
            {

                var consulta = await _IProveedores.insertar(_clientes);

                if (consulta == "ok")
                {
                    return StatusCode(200, consulta);
                }

                if (consulta == "repetido")
                {
                    return StatusCode(200, consulta);
                }

                return BadRequest();


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> listar()
        {

            try
            {

                var consulta = await _IProveedores.listar();
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpGet]
        [Route("[action]/{idProveedor}")]
        public async Task<IActionResult> cargar(Guid idProveedor)
        {

            try
            {

                var consulta = await _IProveedores.cargar(idProveedor);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> actualizar(Proveedores _cliente)
        {

            try
            {

                var consulta = await _IProveedores.editar(_cliente);
                if (consulta == "ok")
                {

                    return StatusCode(200, consulta);

                }
                if (consulta == "repetido")
                {

                    return StatusCode(200, consulta);

                }

                return BadRequest();



            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpDelete]
        [Route("[action]/{idProveedor}")]
        public async Task<IActionResult> eliminar(Guid idProveedor)
        {

            try
            {

                var consulta = await _IProveedores.eliminar(idProveedor);

                if (consulta == "ok")
                {
                    return StatusCode(200, consulta);

                }
                return BadRequest();



            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }

    }
}
