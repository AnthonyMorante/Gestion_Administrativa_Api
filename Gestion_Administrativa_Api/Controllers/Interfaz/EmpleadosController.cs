using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {


        private readonly IEmpleados _IEmpleados;

        public EmpleadosController(IEmpleados IEmpleados)
        {

            _IEmpleados = IEmpleados;

        }

        [HttpPost]
        [Route("[action]")]


        public async Task<IActionResult> insertar(EmpleadosDto _empleados)
        {

            try
            {

                var consulta = await _IEmpleados.insertar(_empleados);

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

                var consulta = await _IEmpleados.listar(idEmpresa);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpGet]
        [Route("[action]/{idCliente}")]
        public async Task<IActionResult> cargar(Guid idCliente)
        {

            try
            {

                var consulta = await _IEmpleados.cargar(idCliente);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> actualizar(Empleados _cliente)
        {

            try
            {

                var consulta = await _IEmpleados.editar(_cliente);
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
        [Route("[action]/{idCliente}")]
        public async Task<IActionResult> eliminar(Guid idCliente)
        {

            try
            {

                var consulta = await _IEmpleados.eliminar(idCliente);

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
