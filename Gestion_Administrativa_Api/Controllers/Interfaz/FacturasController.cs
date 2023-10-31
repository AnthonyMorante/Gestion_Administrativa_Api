using Dapper;
using Gestion_Administrativa_Api.Dtos.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static Gestion_Administrativa_Api.Documents_Models.Factura.factura_V100;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturas _IFacturas;
        private readonly IDbConnection _dapper;

        public FacturasController(IFacturas IFacturas, IDbConnection db)
        {
            _IFacturas = IFacturas;
            _dapper = db;
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
                string sql = @" SELECT * FROM secuenciales
                                WHERE activo = TRUE AND ""idEmpresa""=uidd(@idEmpresa)
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
                return BadRequest(new { error = "error", exc = ex });
            }
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