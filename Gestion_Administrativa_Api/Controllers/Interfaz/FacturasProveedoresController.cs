﻿using Dapper;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasProveedoresController : ControllerBase
    {
        private readonly IDbConnection _dapper;
        private readonly _context _context;

        public FacturasProveedoresController(IDbConnection dapper, _context context)
        {
            _context = context;
            _dapper = dapper;
        }

        [HttpPost]
        public async Task<IActionResult> leerXml([FromForm] IFormFile fileXml)
        {
            try
            {
                //var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                //var factura = Tools.XmlToFacturaDbModel(fileXml.OpenReadStream(), true);
                //var productos=await (from item in _context.Productos
                //                where item.IdEmpresa== idEmpresa && item.Activo==true
                //               select new
                //               {
                //                   item.IdProducto,
                //                   item.Descripcion
                //               }).ToListAsync();
                //var preciosProductos = await (from item in _context.ProductosProveedores
                //                        join pr in _context.Productos on item.IdProducto equals pr.IdProducto
                //                        where pr.IdEmpresa == idEmpresa && item.Identificacion == factura.Ruc
                //                        select new
                //                        {
                //                            item.IdProducto,
                //                            item.CodigoPrincipal
                //                        }
                //                      ).ToListAsync();
                //return Ok(new {factura,productos,preciosProductos});
                var factura = Tools.XmlToFacturaDbModel(fileXml.OpenReadStream(), true);
                await guardarFacturaProveedor(factura);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private async Task guardarFacturaProveedor(SriFacturas _factura)
        {
            try
            {

                string sql = @"SELECT COUNT(""idFactura"") FROM ""SriFacturas"" WHERE ""claveAcceso""=@ClaveAcceso";
                if (await _dapper.ExecuteScalarAsync<int>(sql, _factura) > 0) throw new Exception("La factura seleccionada ya se encuentra registrada");
                _factura.CodigoEstado = 2;
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                sql = @"SELECT * FROM ""SriPersonas"" WHERE identificacion=@Ruc";
                _factura.IdEmpresa = idEmpresa;
                _factura.IdUsuario=Guid.Parse(Tools.getIdUsuario(HttpContext));
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
                    persona.Nombres=_factura.RazonSocial;   
                    persona.Proveedor = true;
                    _context.SriPersonas.Add(persona);
                }
                else
                {
                    persona.RazonSocial = _factura.RazonSocial;
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
                        var impuestos = item.SriDetallesFacturasImpuestos.FirstOrDefault();
                        producto.SriPrecios = new List<SriPrecios>()
                        {
                            new SriPrecios
                            {
                               Tarifa = impuestos.Tarifa,
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
                               Valor = impuestos.Valor,
                               Codigo = impuestos.Codigo,
                               Activo = false,
                            }
                        });
                    }
                }
                _context.SriFacturas.Add(_factura);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}