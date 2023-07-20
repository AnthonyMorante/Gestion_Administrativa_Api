using Gestion_Administrativa_Api.Dtos;
using Gestion_Administrativa_Api.Interfaces;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly IProductos _IProductos;

        public ProductosController(IProductos IProductos)
        {

            _IProductos = IProductos;

        }

        [HttpPost]
        [Route("[action]")]


        public async Task<IActionResult> insertar(ProductosDto _empleados)
        {

            try
            {

                var consulta = await _IProductos.insertar(_empleados);

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
        [Route("[action]/{idEmpresa}")]
        public async Task<IActionResult> listar(Guid idEmpresa)
        {

            try
            {

                var consulta = await _IProductos.listar(idEmpresa);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpGet]
        [Route("[action]/{idProducto}")]
        public async Task<IActionResult> cargar(Guid idProducto)
        {

            try
            {

                var consulta = await _IProductos.cargar(idProducto);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> actualizar(ProductosDto _producto)
        {

            try
            {

                var consulta = await _IProductos.editar(_producto);
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
        [Route("[action]/{idProducto}")]
        public async Task<IActionResult> eliminar(Guid idProducto)
        {

            try
            {

                var consulta = await _IProductos.eliminar(idProducto);

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



        [HttpPut]
        [Route("[action]/{idProducto}")]
        public async Task<IActionResult> desactivar(Guid idProducto, bool activo)
        {

            try
            {

                var consulta = await _IProductos.desactivar(idProducto, activo);


                return StatusCode(200, consulta);






            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


    }
}
