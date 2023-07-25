using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
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


        public async Task<IActionResult> insertar(ClientesDto _clientes)
        {

            try
            {

                var consulta = await _IClientes.insertar(_clientes);

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

                var consulta = await _IClientes.listar(idEmpresa);
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

                var consulta = await _IClientes.cargar(idCliente);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }


        [HttpGet]
        [Route("[action]/{identificacion}")]
        public async Task<IActionResult> cargarPorIdentificacion(String identificacion)
        {

            try
            {

                var consulta = await _IClientes.cargarPorIdentificacion(identificacion);
                return StatusCode(200, consulta);


            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> actualizar(Clientes _cliente)
        {

            try
            {

                var consulta = await _IClientes.editar(_cliente);
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

                var consulta = await _IClientes.eliminar(idCliente);

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
