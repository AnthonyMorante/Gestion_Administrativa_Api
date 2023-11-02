﻿using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturas _IFacturas;
        private readonly IDbConnection _dapper;
        private readonly _context _context;

        public FacturasController(IFacturas IFacturas, IDbConnection db, _context context)
        {
            _IFacturas = IFacturas;
            _dapper = db;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel? _params)
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT f.""idFactura"",""ambiente"",""claveAcceso"",""secuencial"",
                                f.""receptorRazonSocial"" AS ""cliente"",tes.""nombre"" AS ""estadoSri"",
                                ted.""nombre"" AS estado,f.""fechaRegistro"",f.""fechaEmision"",f.""fechaAutorizacion"",
                                f.""receptorTelefono"" AS ""telefonoCliente"", f.""receptorCorreo"" AS ""emailCliente"",
                                f.""idTipoEstadoSri"",f.""idTipoEstadoDocumento"",""establecimiento"",f.""tipoEmision"",f.""idPuntoEmision"",
                                pe.""nombre"" AS ""puntoEmision""
                                FROM facturas f
                                INNER JOIN ""tipoEstadoSri"" tes ON tes.""idTipoEstadoSri"" = f.""idTipoEstadoSri""
                                INNER JOIN ""tipoEstadoDocumentos"" ted ON ted.""idTipoEstadoDocumento"" = f.""idTipoEstadoDocumento""
                                INNER JOIN ""establecimientos"" e ON e.""idEstablecimiento"" = f.""idEstablecimiento""
                                INNER JOIN ""puntoEmisiones"" pe ON pe.""idPuntoEmision"" = f.""idPuntoEmision""
                                WHERE (DATE_PART('year', f.""fechaEmision""::date) - DATE_PART('year', current_date::date)) * 12 +
                                (DATE_PART('month', f.""fechaEmision""::date) - DATE_PART('month', current_date::date))<=3
                                AND e.""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY ""secuencial"" desc";
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
        public async Task<IActionResult> secuenciales()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @" SELECT ""nombre"",""idTipoDocumento"" FROM secuenciales
                                WHERE ""activo""= TRUE
                                AND ""idEmpresa""=uuid(@idEmpresa)
                                UNION ALL 
                                SELECT nombre,""idTipoDocumento"" FROM ""secuencialesProformas"" 
                                WHERE ""activo""= TRUE
                                AND ""idEmpresa""=uuid(@idEmpresa)
                                ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> insertar(FacturaDto? _facturaDto)
        {
            try
            {
                _facturaDto = await procesarFactura(_facturaDto);
                if (_facturaDto.idDocumentoEmitir == Guid.Parse("6741a8d2-2e5b-4281-b188-c04e2c909049"))
                {
                    var proforma = await _IFacturas.guardar(_facturaDto);
                    return Ok("proforma");
                }
                var consulta = await _IFacturas.guardar(_facturaDto);
                var ride = await generarRide(consulta, _facturaDto.email);
                var recibo = await _IFacturas.generaRecibo(ControllerContext, consulta, _facturaDto);
                return Ok(recibo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private async Task<FacturaDto> procesarFactura(FacturaDto? _facturaDto)
        {
            _facturaDto.idEmpresa = new Guid(Tools.getIdEmpresa(HttpContext));
            var empresa = await _context.Clientes.AsNoTracking().Where(x => x.IdEmpresa == _facturaDto.idEmpresa).FirstOrDefaultAsync();
            string sql = @"SELECT * FROM ""clientes"" WHERE ""identificacion""=@identificacion AND ""idEmpresa""=uuid(@idEmpresa)";
            string ciudadDefecto = "Pelileo";
            var cliente = await _dapper.QueryFirstOrDefaultAsync<Clientes>(sql,_facturaDto);
            if (cliente == null){
                cliente= new Clientes();
                sql = $@"SELECT ""idCiudad"" 
                        FROM ""ciudades""
                        WHERE upper(""nombre"") LIKE '%{ciudadDefecto.ToUpper()}%'
                        AND ""activo""=TRUE  
                        LIMIT 1";
                cliente.IdCiudad = await _dapper.ExecuteScalarAsync<Guid>(sql);
                _facturaDto.idCiudad = Tools.toGuid(cliente.IdCiudad);
                cliente.Activo = true;
                cliente.IdTipoIdentificacion=_facturaDto.idTipoIdenticacion;
                cliente.RazonSocial = _facturaDto.razonSocial;
                cliente.FechaRegistro = DateTime.Now;
                cliente.Direccion = _facturaDto.direccion;
                cliente.Identificacion= _facturaDto.identificacion;
                cliente.Telefono = _facturaDto.telefono;
                cliente.Email = _facturaDto.email;
                cliente.IdEmpresa = _facturaDto.idEmpresa;
                _context.Clientes.Add(cliente);
            }
            else
            {
                cliente=await _context.Clientes.Where(x=>x.IdEmpresa==_facturaDto.idEmpresa && x.Identificacion==_facturaDto.identificacion).FirstOrDefaultAsync();
                cliente.RazonSocial = _facturaDto.razonSocial;
                cliente.Direccion = _facturaDto.direccion;
                cliente.Telefono = _facturaDto.telefono;
                cliente.Email = _facturaDto.email;
                _facturaDto.idCiudad = Tools.toGuid(cliente.IdCiudad);
                _context.Clientes.Update(cliente);
            }

            await _context.SaveChangesAsync();
            _facturaDto.idCliente = cliente.IdCliente;
            _facturaDto.idDocumentoEmitir=(await _context.DocumentosEmitir.AsNoTracking().Where(x=>x.IdTipoDocumento==_facturaDto.idTipoDocumento).FirstOrDefaultAsync()).IdDocumentoEmitir;
            sql = @"SELECT ""nombre"" 
                        FROM ""establecimientos"" 
                        WHERE ""idEstablecimiento""=uuid(@idEstablecimiento)
                        ";
            _facturaDto.establecimiento = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
            sql = @"SELECT ""nombre"" 
                    FROM ""puntoEmisiones""  
                    WHERE ""idPuntoEmision""=uuid(@idPuntoEmision)
                    ";
            _facturaDto.puntoEmision = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D3");
            sql = @"SELECT ""nombre"" 
                    FROM ""secuenciales"" 
                    WHERE ""idEmpresa""=uuid(@idEmpresa)
                    ";
            _facturaDto.secuencial = Convert.ToInt32(await _dapper.ExecuteScalarAsync<string>(sql, _facturaDto)).ToString("D9");
            _facturaDto.idUsuario = new Guid(Tools.getIdUsuario(HttpContext));
            _facturaDto.idFormaPago = _facturaDto.formaPago.FirstOrDefault().idFormaPago;
            return _facturaDto;
        } 



        [AllowAnonymous]
        [HttpGet]
        public async Task<string> generarRide(factura_V1_0_0 _factura, string email)
        {
            try
            {
                var consulta = await _IFacturas.generaRide(ControllerContext, _factura, email);
                return "ok";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        [HttpGet]
        public async Task<IActionResult> tiposDocumentos()
        {
            try
            {
                string sql = @"
                            SELECT * FROM ""tipoDocumentos""
                            WHERE codigo in(1,0)
                            AND activo=true
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> puntosEmisiones()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""puntoEmisiones""
                                WHERE ""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY NOT predeterminado;
                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> establecimientos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""establecimientos""
                                WHERE ""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY NOT predeterminado;
                                
                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> formaPagos()
        {
            try
            {
                string sql = @"SELECT * FROM ""formaPagos"" 
                               WHERE ""activo""=true
                               ORDER BY codigo;
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> tiempoFormaPagos()
        {
            try
            {
                string sql = @"SELECT * FROM ""tiempoFormaPagos""
                               WHERE ""activo""=true
                            ";
                return Ok(await _dapper.QueryAsync(sql));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        

        [HttpGet]
        public async Task<IActionResult> listaProductos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT p.*,v.valor AS ""iva"",v.nombre AS ""nombreIva"" 
                                FROM ""productos"" p
                                INNER JOIN ""ivas"" v ON v.""idIva""= p.""idIva""	
                                WHERE p.""activo""=TRUE
                                AND ""idEmpresa""=uuid(@idEmpresa)
                                ORDER BY ""nombre""; 
                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> listaPreciosProductos()
        {
            try
            {
                var idEmpresa = Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT p.""idProducto"" as""idDetallePrecioProducto"",""idProducto"",p.""nombre"" AS ""producto"",p.""codigo"" AS ""codigoProducto"",""precio"",i.codigo AS ""codigoIva"",i.""idIva"",i.""nombre"" AS ""nombreIva"",i.""valor"" as iva
                                FROM productos p
                                INNER JOIN ivas i ON i.""idIva"" = p.""idIva"" 
                                WHERE p.""activo"" = true 
                                AND p.""idEmpresa""=uuid(@idEmpresa)
                                UNION ALL 
                                SELECT ""idDetallePrecioProducto"",dp.""idProducto"",p.""nombre"" AS ""producto"",p.""codigo"" AS ""codigoProducto"",dp.""totalIva"" AS ""precio"",i.""codigo"" AS ""codigoIva"",i.""idIva"",i.""nombre"" AS ""nombreIva"",i.""valor"" as iva
                                FROM ""detallePrecioProductos"" dp
                                INNER JOIN ""productos"" p ON dp.""idProducto"" = p.""idProducto"" 
                                INNER JOIN ""ivas"" i ON i.""idIva"" = dp.""idIva""  
                                WHERE p.""activo"" =TRUE AND dp.""activo""=true
                                AND p.""idEmpresa""=uuid(@idEmpresa)

                            ";
                return Ok(await _dapper.QueryAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        [Route("{identificacion}")]
        public async Task<IActionResult> buscarCliente(string identificacion)
        {
            try
            {
                var idEmpresa= Tools.getIdEmpresa(HttpContext);
                string sql = @"SELECT * FROM ""clientes""
                            WHERE ""identificacion""=@identificacion
                            AND ""idEmpresa""=uuid(@idEmpresa);
                            ";
                return Ok(await _dapper.QueryFirstOrDefaultAsync(sql, new { idEmpresa,identificacion }));
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        


    }
}