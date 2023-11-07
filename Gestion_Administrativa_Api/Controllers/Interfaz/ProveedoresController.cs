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
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedores _IProveedores;
        private readonly IDbConnection _dapper;

        public ProveedoresController(IProveedores IProveedores, IDbConnection db)
        {
            _IProveedores = IProveedores;
            _dapper = db;
        }

        [HttpPost]
        public async Task<IActionResult> insertar(ProveedoresDto _clientes)
        {
            try
            {
                _clientes.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var consulta = await _IProveedores.insertar(_clientes);

                if (consulta == "ok") return Ok();
                if (consulta == "repetido") return Problem("El documento de identidad ya existe en el sistema");
                return Problem();
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
                string sql = @"SELECT * FROM Proveedores WHERE ""idEmpresa""=uuid(@idEmpresa)";
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
        [Route("{idProveedor}")]
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
        public async Task<IActionResult> actualizar(Proveedores _cliente)
        {
            try
            {
                var consulta = await _IProveedores.editar(_cliente);
                if (consulta == "ok") return Ok();
                if (consulta == "repetido") throw new Exception("El número de identificación ya se encuentra registrado");
                return Problem();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }

        [HttpDelete]
        [Route("{idProveedor}")]
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