using Dapper;
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
    public class LotesController : ControllerBase
    {
        private readonly _context _context;
        private readonly IDbConnection _dapper;

        public LotesController(_context context, IDbConnection dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT l.*,u.nombre as usuario,p.codigo,p.nombre as producto,p.cantidad AS stock
                               FROM ""lotes"" l
                               INNER JOIN productos p ON p.""idProducto"" = l.""idProducto""
                               INNER JOIN usuarios u ON u.""idUsuario"" = l.""idUsuario""
                               WHERE p.""idEmpresa""=uuid(@idEmpresa)
                               ORDER BY ""fechaRegistro"" desc
                                ";
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
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> comboProductos()
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT p.""idProducto"", p.codigo, p.nombre,p.cantidad
                                FROM productos p
                                WHERE p.""activo""=TRUE
                                AND p.""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY UPPER(REPLACE(regexp_replace(CAST(p.""nombre"" AS varchar),'\t|\n|\r|\s',''),' ',''))";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardar(Lotes _data)
        {
            try
            {
                _data.FechaRegistro = DateTime.Now;
                _data.IdUsuario = Guid.Parse(Tools.getIdUsuario(HttpContext));
                var producto = await _context.Productos.FindAsync(_data.IdProducto);
                if (_data.IdLote > 0)
                {
                    string sql = @"SELECT cantidad FROM lotes WHERE ""idLote""=@idLote";
                    var cantidadAnterior = await _dapper.ExecuteScalarAsync<decimal>(sql, _data);
                    producto!.Cantidad -= cantidadAnterior;
                    producto!.Cantidad += _data.Cantidad;
                    _context.Lotes.Update(_data);
                }
                else
                {
                    producto!.Cantidad += _data.Cantidad;
                    _context.Lotes.Add(_data);
                }
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{idLote}")]
        public async Task<IActionResult> unDato(int idLote)
        {
            try
            {
                string sql = @"SELECT * FROM lotes WHERE ""idLote""=@idLote";
                return Ok(await _dapper.QueryFirstOrDefaultAsync(sql, new { idLote }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{idLote}")]
        public async Task<IActionResult> eliminar(int idLote)
        {
            try
            {
                var lote = await _context.Lotes.FindAsync(idLote);
                if (lote == null) throw new Exception("No se ha encontrado el lote o ya ha sido eliminado");
                var producto = await _context.Productos.FindAsync(lote.IdProducto);
                producto!.Cantidad -= lote.Cantidad;
                _context.Lotes.Remove(lote);
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}