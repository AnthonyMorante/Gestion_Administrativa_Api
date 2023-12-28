using Dapper;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Xml.Linq;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasProveedoresController : ControllerBase
    {
        private readonly _context _context;
        private readonly string cn;
        public FacturasProveedoresController(_context context)
        {
            _context = context;
            cn = Tools.config!.GetConnectionString("cn")!;
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            var _dapper = new NpgsqlConnection(cn);
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT ""idFactura"",""fechaRegistro"",""fechaEmision"",
                            ""claveAcceso"",ruc,""dirMatriz"",""nombreComercial"",""razonSocial"",
                            ""importeTotal"",
                            (SELECT count(""claveAcceso"") FROM retenciones r WHERE r.""claveAcceso""= f.""claveAcceso"") as ""totalRetenciones""
                            FROM ""SriFacturas"" f
                            WHERE ""idEmpresa"" = @idEmpresa
                            AND compra=true";
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
            finally
            {
                _dapper.Dispose();
            }
        }

        [HttpPost]
        public async Task<IActionResult> leerXml([FromForm] IFormFile fileXml)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                var factura = Tools.XmlToFacturaDbModel(fileXml.OpenReadStream(), true);
                var productos = await (from item in _context.Productos
                                       where item.IdEmpresa == idEmpresa && item.Activo == true
                                       select new
                                       {
                                           item.IdProducto,
                                           Producto=item.Nombre.TrimStart().TrimEnd()
                                       }).OrderBy(x=>x.Producto).ToListAsync();
                var productosProveedores = await (from item in _context.ProductosProveedores
                                              join pr in _context.Productos on item.IdProducto equals pr.IdProducto
                                              where pr.IdEmpresa == idEmpresa && item.Identificacion == factura.Ruc
                                              select new
                                              {
                                                  item.IdProducto,
                                                  item.CodigoPrincipal
                                              }
                                      ).ToListAsync();
                var formasPagos = await (from item in _context.SriFormasPagos
                                         select new
                                         {
                                             item.Codigo,
                                             item.FormaPago
                                         }).ToListAsync();
                return Ok(new { factura, productos, productosProveedores, formasPagos });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        public class FacturaProveedor
        {
            public SriFacturas factura { get; set; }
            public List<ProductosProveedores> listaProductos { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> guardar(FacturaProveedor _data)
        {
            var _dapper = new NpgsqlConnection(cn);
            try
            {
                var _factura = _data.factura;
                string sql = @"SELECT COUNT(""idFactura"") FROM ""SriFacturas"" WHERE ""claveAcceso""=@ClaveAcceso";
                if (await _dapper.ExecuteScalarAsync<int>(sql, _factura) > 0) throw new Exception("La factura seleccionada ya se encuentra registrada");
                _factura.CodigoEstado = 2;
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                sql = @"SELECT * FROM ""SriPersonas"" WHERE identificacion=@Ruc";
                _factura.IdEmpresa = idEmpresa;
                _factura.IdUsuario = Guid.Parse(Tools.getIdUsuario(HttpContext));
                var persona = await _dapper.QueryFirstOrDefaultAsync<SriPersonas>(sql, _factura);
                if (persona == null)
                {
                    persona = new SriPersonas();
                    persona.Identificacion = _factura.Ruc;
                    persona.TipoIdentificacion = "4";
                    persona.RazonSocial = _factura.RazonSocial;
                    persona.Direccion = _factura.DirMatriz;
                    persona.FechaRegistro = DateTime.Now;
                    persona.Apellidos = _factura.RazonSocial;
                    persona.Nombres = _factura.RazonSocial;
                    persona.Proveedor = true;
                    _context.SriPersonas.Add(persona);
                }
                else
                {
                    persona.Direccion = _factura.DirMatriz;
                    _context.SriPersonas.Update(persona);
                }
                foreach (var item in _factura.SriDetallesFacturas)
                {
                    sql = @"SELECT * FROM ""SriProductos"" WHERE ""codigoPrincipal""=@CodigoPrincipal AND ""identificacion""=@Ruc ";
                    var producto = await _dapper.QueryFirstOrDefaultAsync<SriProductos>(sql, new { _factura.Ruc, item.CodigoPrincipal });
                    if (producto == null)
                    {
                        producto = new SriProductos();
                        producto.Disponible = false;
                        producto.Activo = false;
                        producto.Producto = item.Descripcion;
                        producto.PrecioCompra = item.PrecioUnitario;
                        producto.IdEmpresa = idEmpresa;
                        producto.Identificacion = _factura.Ruc;
                        producto.CodigoPrincipal = item.CodigoPrincipal;
                        var impuestos = item.SriDetallesFacturasImpuestos.FirstOrDefault();
                        producto.SriPrecios = new List<SriPrecios>()
                        {
                            new SriPrecios
                            {
                               Tarifa = impuestos.Tarifa,
                               BaseImponible=impuestos.BaseImponible,
                               TotalConImpuestos=impuestos.BaseImponible+impuestos.Valor,
                               Valor = impuestos.Valor,
                               Codigo = impuestos.Codigo,
                               Activo = false,
                            }
                        };
                        persona.SriProductos.Add(producto);
                    }
                    else
                    {
                        sql = @"DELETE FROM ""SriPrecios""  WHERE ""IdProducto""=@IdProducto";
                        await _dapper.ExecuteAsync(sql, producto);
                        var impuestos = item.SriDetallesFacturasImpuestos.FirstOrDefault();
                        _context.SriPrecios.AddRange(new List<SriPrecios>()
                        {
                            new SriPrecios
                            {
                               IdProducto= producto.IdProducto,
                               Tarifa = impuestos.Tarifa,
                               BaseImponible=impuestos.BaseImponible, 
                               TotalConImpuestos=impuestos.BaseImponible+impuestos.Valor,
                               Valor = impuestos.Valor,
                               Codigo = impuestos.Codigo,
                               Activo = false,
                            }
                        });
                    }
                }
                _context.SriFacturas.Add(_factura);
                var productosProveedores = _data.listaProductos.Select(x => { x.FechaRegistro = DateTime.Now; return x; });
                foreach (var item in productosProveedores)
                {
                    var producto=_context.ProductosProveedores.Where(x=>x.IdProducto==item.IdProducto && x.CodigoPrincipal==item.CodigoPrincipal).FirstOrDefault();
                    if (producto==null)
                    {
                        _context.ProductosProveedores.Add(item);
                    }
                    else
                    {
                        producto.IdProducto = item.IdProducto;
                        _context.ProductosProveedores.Update(producto);
                    }
                    var productoStock = await _context.Productos.FindAsync(item.IdProducto);
                    productoStock.Cantidad += _factura.SriDetallesFacturas.Where(x => x.CodigoPrincipal == item.CodigoPrincipal).FirstOrDefault().Cantidad;
                    _context.Productos.Update(productoStock);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
            finally
            {
                _dapper.Close();
            }
        }

        [HttpGet("{idFactura}")]
        public async Task<IActionResult> unDato(int idFactura)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                var factura=await _context.SriFacturas.FindAsync(idFactura);
                factura.SriCamposAdicionales=await _context.SriCamposAdicionales.Where(x=>x.IdFactura == idFactura).ToListAsync();
                factura.SriDetallesFacturas=await _context.SriDetallesFacturas.Include(x=>x.SriDetallesFacturasImpuestos).Where(x=>x.IdFactura == idFactura).ToListAsync();
                factura.SriPagos=await _context.SriPagos.Where(x=>x.IdFactura == idFactura).ToListAsync();
                factura.SriTotalesConImpuestos=await _context.SriTotalesConImpuestos.Where(x=>x.IdFactura == idFactura).ToListAsync();
                var productos = await (from item in _context.Productos
                                       where item.IdEmpresa == idEmpresa && item.Activo == true
                                       select new
                                       {
                                           item.IdProducto,
                                           Producto = item.Nombre.TrimStart().TrimEnd()
                                       }).OrderBy(x => x.Producto).ToListAsync();
                var productosProveedores = await (from item in _context.ProductosProveedores
                                                  join pr in _context.Productos on item.IdProducto equals pr.IdProducto
                                                  where pr.IdEmpresa == idEmpresa && item.Identificacion == factura.Ruc
                                                  select new
                                                  {
                                                      item.IdProducto,
                                                      item.CodigoPrincipal
                                                  }
                                      ).ToListAsync();
                var formasPagos = await (from item in _context.SriFormasPagos
                                         select new
                                         {
                                             item.Codigo,
                                             item.FormaPago
                                         }).ToListAsync();
                return Ok(new { factura, productos, productosProveedores, formasPagos });
            }
            catch (Exception ex)
            {

                return Tools.handleError(ex);
            }
        }
    }
}