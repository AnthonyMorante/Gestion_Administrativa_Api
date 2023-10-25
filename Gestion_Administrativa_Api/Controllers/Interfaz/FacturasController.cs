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
    }
}