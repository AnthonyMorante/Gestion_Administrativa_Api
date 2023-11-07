using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleados _IEmpleados;
        private readonly IDbConnection _dapper;

        public EmpleadosController(IEmpleados IEmpleados, IDbConnection dapper)
        {
            _IEmpleados = IEmpleados;
            _dapper = dapper;
        }

        [HttpPost]
        public async Task<IActionResult> insertar(EmpleadosDto _empleados)
        {
            try
            {
                _empleados.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));

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

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT ""idEmpleado"", identificacion, ""razonSocial"", direccion, email, telefono,""idEmpresa"", activo
                               FROM Empleados WHERE ""idEmpresa""=uuid(@idEmpresa) AND activo=true";
                return Ok(await Tools.DataTablePostgresSql(new Tools.DataTableParams
                {
                    parameters = new { idEmpresa },
                    query = sql,
                    dapperConnection = _dapper,
                    dataTableModel = _params
                }));
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpGet]
        [Route("{idCliente}")]
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
        [Route("{idCliente}")]
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