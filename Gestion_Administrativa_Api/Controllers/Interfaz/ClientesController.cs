using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientes _IClientes;
        private readonly IDbConnection _dapper;

        public ClientesController(IClientes IClientes, _context context)
        {
            _IClientes = IClientes;
            _dapper = context.Database.GetDbConnection();
        }

        [HttpPost]
        public async Task<IActionResult> insertar(ClientesDto _clientes)
        {
            try
            {
                _clientes.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var consulta = await _IClientes.insertar(_clientes);

                if (consulta == "ok")
                {
                    return StatusCode(200, consulta);
                }

                if (consulta == "repetido") return Problem("El documento de identidad ya existe en el sistema");

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
                string sql = @"SELECT ""idCliente"", identificacion, ""razonSocial"", direccion, email, telefono,""idEmpresa"", activo
                               FROM Clientes WHERE ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER) AND activo=1";
                return Ok(await Tools.DataTableSql(new Tools.DataTableParams
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
                var consulta = await _IClientes.cargar(idCliente);
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }

        [HttpGet]
        [Route("{identificacion}")]
        public async Task<IActionResult> cargarPorIdentificacion(string identificacion)
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
        public async Task<IActionResult> actualizar(Clientes _cliente)
        {
            try
            {
                var consulta = await _IClientes.editar(_cliente);
                if (consulta == "ok") return Ok();
                if (consulta == "repetido") throw new Exception("El número de identificación ya se encuentra registrado.");
                return Problem();
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