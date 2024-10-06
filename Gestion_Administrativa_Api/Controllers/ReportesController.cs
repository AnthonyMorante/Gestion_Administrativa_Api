using ArrayToExcel;
using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Administrativa_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly _context _contexto;

        public ReportesController(_context contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelFacturas()
        {
            try
            {
                var facturas = await (from factura in _contexto.Facturas
                                      join cliente in _contexto.Clientes
                                      on factura.IdCliente equals cliente.IdCliente
                                      join estado in _contexto.TipoEstadoSri
                                      on factura.IdTipoEstadoSri equals estado.IdTipoEstadoSri
                                      join tipo in _contexto.DocumentosEmitir
                                      on factura.IdDocumentoEmitir equals tipo.IdDocumentoEmitir
                                      where tipo.Nombre == "FACTURA"
                                      select new
                                      {
                                          factura.ClaveAcceso,
                                          ambiente = factura.Ambiente == 1 ? "PRUEBAS" : "PRODUCCIÓN",
                                          fechaEmision = factura.FechaEmision,
                                          Estado = estado.Nombre,
                                          cliente.Identificacion,
                                          cliente.RazonSocial,
                                          factura.Iva12,
                                          Descuento = factura.TotalDescuento,
                                          Subtotal = factura.TotalSinImpuesto,
                                          Total = factura.TotalImporte,
                                          factura.ValorRecibido,
                                          factura.Cambio,
                                          Detalle = string.Join(Environment.NewLine, _contexto.DetalleFacturas.Include(x => x.IdProductoNavigation)
                                    .Where(x => x.IdFactura == factura.IdFactura)
                                    .Select(x => $"{(int)x.Cantidad.GetValueOrDefault()} {x.IdProductoNavigation.Nombre} ${x.Precio}"))
                                      }).ToListAsync();
                var excel = facturas.ToExcel(x =>
                {
                    x.SheetName("Facturas");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteFacturas {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelProformas()
        {
            try
            {
                var facturas = await (from factura in _contexto.Facturas
                                      join cliente in _contexto.Clientes
                                      on factura.IdCliente equals cliente.IdCliente
                                      join estado in _contexto.TipoEstadoSri
                                      on factura.IdTipoEstadoSri equals estado.IdTipoEstadoSri
                                      join tipo in _contexto.DocumentosEmitir
                                      on factura.IdDocumentoEmitir equals tipo.IdDocumentoEmitir
                                      where tipo.Nombre == "PROFORMA"
                                      select new
                                      {
                                          factura.ClaveAcceso,
                                          ambiente = factura.Ambiente == 1 ? "PRUEBAS" : "PRODUCCIÓN",
                                          fechaEmision = factura.FechaEmision,
                                          Estado = estado.Nombre,
                                          cliente.Identificacion,
                                          cliente.RazonSocial,
                                          factura.Iva12,
                                          Descuento = factura.TotalDescuento,
                                          Subtotal = factura.TotalSinImpuesto,
                                          Total = factura.TotalImporte,
                                          Detalle = string.Join(Environment.NewLine, _contexto.DetalleFacturas.Include(x => x.IdProductoNavigation)
                                    .Where(x => x.IdFactura == factura.IdFactura)
                                    .Select(x => $"{(int)x.Cantidad.GetValueOrDefault()} {x.IdProductoNavigation.Nombre} ${x.Precio}"))
                                      }).ToListAsync();
                var excel = facturas.ToExcel(x =>
                {
                    x.SheetName("Proformas");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteProformas {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelProductos()
        {
            try
            {
                var productos = await _contexto.Productos.Select(x => new
                {
                    x.Codigo,
                    Producto = x.Nombre,
                    Stock = x.Cantidad,
                    Precios = string.Join(Environment.NewLine, _contexto.DetallePrecioProductos.Where(y => y.IdProducto == x.IdProducto).Select(y => $"Precio: ${y.Total}"))
                }).ToListAsync();
                var excel = productos.ToExcel(x =>
                {
                    x.SheetName("Productos");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteProductos {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelClientes()
        {
            try
            {
                var clientes = await _contexto.Clientes.Select(x => new
                {
                    x.Identificacion,
                    Cliente = x.RazonSocial,
                    x.Direccion,
                    x.Telefono,
                    x.Email
                }).ToListAsync();
                var excel = clientes.ToExcel(x =>
                {
                    x.SheetName("Clientes");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteClientes {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelProveedores()
        {
            try
            {
                var proveedores = await _contexto.SriPersonas
                    .Where(x => x.Proveedor==true)
                    .Select(x => new
                {
                    x.Identificacion,
                    Proveedor = x.RazonSocial,
                    x.Direccion,
                    x.Telefono,
                    x.Email
                }).ToListAsync();
                var excel = proveedores.ToExcel(x =>
                {
                    x.SheetName("Proveedores");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteProveedores {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelFacturasProveedores()
        {
            try
            {
                var facturasProveedores = await (from factura in _contexto.SriFacturas
                                                 select new
                                                 {
                                                     factura.ClaveAcceso,
                                                     fechaEmision = factura.FechaEmision,
                                                     Estado = factura.RazonSocial,
                                                     factura.Ruc,
                                                     Descuento = factura.TotalDescuento,
                                                     Subtotal = factura.TotalSinImpuesto,
                                                     Total = factura.ImporteTotal,
                                                     Detalle = string.Join(Environment.NewLine, _contexto.SriDetallesFacturas.Include(x => x.IdProductoNavigation)
                                               .Where(x => x.IdFactura == factura.IdFactura)
                                               .Select(x => $"{(int)x.Cantidad.GetValueOrDefault()} {x.Descripcion} ${x.PrecioTotalConImpuesto}"))
                                                 }).ToListAsync();
                var excel = facturasProveedores.ToExcel(x =>
                {
                    x.SheetName("FacturasProveedores");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteFacturasProveedores {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExcelCajas()
        {
            try
            {
                var cajas = await (from caja in _contexto.Cajas
                                   select new
                                   {
                                       FechaApertura = caja.FechaRegistro,
                                       FechaCierre = caja.FechaCierre,
                                       caja.TotalApertura,
                                       caja.TotalCierre,
                                       DetalleApertura = string.Join(Environment.NewLine, _contexto.DetallesCajas
                                                              .Include(x => x.IdDenominacionNavigation)
                                                              .Where(x => x.IdCaja == caja.IdCaja).Select(x => $"{x.Cantidad} ${x.IdDenominacionNavigation.Valor}")),
                                       DetalleCierre = string.Join(Environment.NewLine, _contexto.DetallesCajas.Include(x => x.IdDenominacionNavigation)
                                                              .Where(x => x.IdCaja == caja.IdCaja).Select(x => $"{x.Cantidad} ${x.IdDenominacionNavigation.Valor}"))
                                   }).ToListAsync();
                var excel = cajas.ToExcel(x =>
                {
                    x.SheetName("Cajas");
                });
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteCajas {DateTime.Now}.xlsx");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}