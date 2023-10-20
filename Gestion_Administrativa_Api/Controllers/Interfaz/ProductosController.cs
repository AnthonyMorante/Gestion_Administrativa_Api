using AutoMapper;
using Dapper;
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
    public class ProductosController : ControllerBase
    {
        private readonly IProductos _IProductos;
        private readonly IDbConnection _dapper;
        private readonly _context _context;

        public ProductosController(IProductos IProductos, IDbConnection dapper, _context context)
        {
            _IProductos = IProductos;
            _dapper = dapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> guardar(Productos _producto)
        {
            try
            {
                _producto.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var producto = await _context.Productos.AsNoTracking().Where(x => x.IdProducto == _producto.IdProducto).FirstOrDefaultAsync();
                if ( producto!= null)
                {
                    _producto.Activo = producto.Activo;
                    _producto.FechaRegistro = DateTime.Now;
                    _producto.ActivoProducto= producto.ActivoProducto;
                    string sql = @"DELETE FROM ""detallePrecioProductos"" WHERE ""idProducto""=uuid(@idProducto);";
                    await _dapper.ExecuteAsync(sql, _producto);
                    _producto.DetallePrecioProductos.Select(x => { x.FechaRegistro = DateTime.Now; return x; });
                    _context.Productos.Update(_producto);
                }
                else
                {
                    _producto.Activo= true;
                    _producto.ActivoProducto = true;
                    _producto.FechaRegistro=DateTime.Now;
                    _context.Productos.Add(_producto);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel _params)
        {
            try
            {
                var idEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT p.*,i.""nombre"" as iva
                                FROM productos p
                                INNER JOIN ivas i ON i.""idIva"" =p.""idIva""
                                WHERE ""idEmpresa""=uuid(@idEmpresa)";
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
                return BadRequest(new { error = "error", exc = ex });
            }
        }

        [HttpGet]
        [Route("{idProducto}")]
        public async Task<IActionResult> activar(Guid idProducto)
        {
            try
            {
                string sql = @"UPDATE productos
                                SET ""activoProducto""  = not""activoProducto""
                                WHERE ""idProducto"" = uuid(@idProducto);
                                ";
                await _dapper.ExecuteAsync(sql, new { idProducto });
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{idProducto}")]
        public async Task<IActionResult> visualizar(Guid idProducto)
        {
            try
            {
                string sql = @"UPDATE productos
                                SET ""activo""  = not""activo""
                                WHERE ""idProducto"" = uuid(@idProducto);
                                ";
                await _dapper.ExecuteAsync(sql, new { idProducto });
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{idEmpresa}")]
        public async Task<IActionResult> listarFactura(Guid idEmpresa)
        {
            try
            {
                var consulta = await _IProductos.listarFactura(idEmpresa);
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }

        [HttpGet]
        [Route("{idProducto}")]
        public async Task<IActionResult> cargar(Guid idProducto)
        {
            try
            {
                string sql = @"SELECT * FROM Productos
                                 WHERE ""idProducto"" = uuid(@idProducto);
                               ";
                var producto = await _dapper.QueryFirstOrDefaultAsync(sql, new { idProducto });
                sql = @"SELECT * FROM ""detallePrecioProductos""
                        WHERE ""idProducto"" = uuid(@idProducto);
                        ";
                var detallePrecios = await _dapper.QueryAsync(sql, new { idProducto });

                return Ok(new { producto, detallePrecios });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> actualizar(ProductosDto _producto)
        {
            try
            {
                _producto.IdEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
                var consulta = await _IProductos.editar(_producto);
                if (consulta == "repetido") throw new Exception("Ya existe un producto con ese código");
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{idProducto}")]
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

        [HttpDelete]
        [Route("{idDetallePrecioProducto}")]
        public async Task<IActionResult> eliminarPrecio(Guid idDetallePrecioProducto)
        {
            try
            {
                string sql = @"DELETE FROM ""detallePrecioProductos"" WHERE ""idDetallePrecioProducto""=uuid(@idDetallePrecioProducto);";
                await _dapper.ExecuteAsync(sql, new { idDetallePrecioProducto });
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}