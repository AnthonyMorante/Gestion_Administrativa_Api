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
        private readonly _context _context;
        private readonly IDbConnection _dapper;

        public ProveedoresController(_context context, IDbConnection db)
        {
            _dapper = db;
            _context = context;
        }

        //[HttpPost]
        //public async Task<IActionResult> insertar(ProveedoresDto _clientes)
        //{
        //    try
        //    {
        //        _clientes.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
        //        var consulta = await _IProveedores.insertar(_clientes);

        //        if (consulta == "ok") return Ok();
        //        if (consulta == "repetido") return Problem("El documento de identidad ya existe en el sistema");
        //        return Problem();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = "error", exc = ex });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT identificacion,""razonSocial"",direccion,telefono,proveedor,email
                               FROM ""SriPersonas""
                               WHERE identificacion IN (SELECT ruc FROM ""SriFacturas""
                               WHERE compra=1 AND ""idEmpresa""=CAST(@idEmpresa AS UNIQUEIDENTIFIER)
                               )";
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

        [HttpGet("{identificacion}")]
        public async Task<IActionResult> cargar(string identificacion)
        {
            try
            {
                return Ok(await _context.SriPersonas.FindAsync(identificacion));
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> actualizar(SriPersonas _data)
        {
            try
            {
                _data.Proveedor = true;
                _context.SriPersonas.Update(_data);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        //[HttpDelete]
        //[Route("{idProveedor}")]
        //public async Task<IActionResult> eliminar(Guid idProveedor)
        //{
        //    try
        //    {
        //        var consulta = await _IProveedores.eliminar(idProveedor);

        //        if (consulta == "ok")
        //        {
        //            return StatusCode(200, consulta);
        //        }
        //        return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = "error", exc = ex });
        //    }
        //}
    }
}