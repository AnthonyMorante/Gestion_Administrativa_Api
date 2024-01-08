using Dapper;
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
    public class CajasController : ControllerBase
    {
        private readonly _context _context;
        private readonly IDbConnection _dapper;

        public CajasController(_context context)
        {
            _context = context;
            _dapper = context.Database.GetDbConnection();
        }

        [HttpPost]
        public async Task<IActionResult> listar([FromBody] Tools.DataTableModel _params)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = (from caja in _context.Cajas
                              select caja).ToQueryString();
                return Ok(await Tools.DataTableSql(new Tools.DataTableParams
                {
                    parameters = new { idEmpresa },
                    query = $"{sql}  WHERE idEmpresa=@idEmpresa ",
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
        public async Task<IActionResult> aperturaDia()
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT COUNT(*) FROM Cajas WHERE idEmpresa=@idEmpresa
                            AND CAST(fechaRegistro AS DATE)=CAST(getdate() AS DATE)";
                return Ok(await _dapper.ExecuteScalarAsync<int>(sql, new { idEmpresa }) > 0);
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
                throw;
            }
        }

        [HttpGet("{idCaja}")]
        public async Task<IActionResult> listarDenominaciones(int? idCaja)
        {
            try
            {
                string sql = @"SELECT d.idDenominacion,nombre,valor,tipo,
                                CASE WHEN dc.idDetalleCaja IS NULL THEN 0 ELSE dc.idDetalleCaja END AS idDetalleCaja,
                                CASE WHEN dc.total IS NULL THEN 0 ELSE dc.total END  AS total,
                                CASE WHEN dc.cantidad IS NULL THEN 0 ELSE dc.cantidad END AS cantidad
                                FROM DenominacionesDinero d
                                INNER JOIN TiposDenominacionesDinero t ON t.idTipoDenominacion = d.idTipoDenominacion
                                LEFT JOIN DetallesCajas dc ON dc.idDenominacion = d.idDenominacion AND dc.idCaja = @idCaja
                                WHERE d.activo = 1
                                ORDER BY d.valor";
                return Ok(await _dapper.QueryAsync(sql, new { idCaja }));
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpGet("{idCaja}")]
        public async Task<IActionResult> unDato(int? idCaja)
        {
            try
            {
                string sql = @"SELECT idCaja,d.idDenominacion,nombre,valor,tipo,
                                CASE WHEN dc.idDetalleCaja IS NULL THEN 0 ELSE dc.idDetalleCaja END AS idDetalleCaja,
                                CASE WHEN dc.total IS NULL THEN 0 ELSE dc.total END  AS total,
                                CASE WHEN dc.cantidad IS NULL THEN 0 ELSE dc.cantidad END AS cantidad
                                FROM DenominacionesDinero d
                                INNER JOIN TiposDenominacionesDinero t ON t.idTipoDenominacion = d.idTipoDenominacion
                                LEFT JOIN DetallesCajas dc ON dc.idDenominacion = d.idDenominacion AND dc.idCaja = @idCaja
                                WHERE d.activo = 1
                                ORDER BY d.valor";
                var detalleCaja = await _dapper.QueryAsync(sql, new { idCaja });
                var caja = await _context.Cajas.AsNoTracking().Where(x => x.IdCaja == idCaja).FirstOrDefaultAsync();
                return Ok(new { detalleCaja, caja });
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardar([FromBody] Cajas _data)
        {
            try
            {
                _data.IdEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                _data.FechaRegistro = DateTime.Now;
                if (_data.IdCaja == 0) _context.Cajas.Add(_data);
                else _context.Cajas.Update(_data);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardarCierre([FromBody] Cajas _data)
        {
            try
            {
                var caja = await _context.Cajas.FindAsync(_data.IdCaja);
                caja.FechaCierre = DateTime.Now;
                caja.TotalCierre = _data.TotalCierre;
                _context.Cajas.Update(caja);
                _context.DetallesCajasCierres.AddRange(_data.DetallesCajasCierres);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }

        [HttpDelete("{idCaja}")]
        public async Task<IActionResult> eliminar(int? idCaja)
        {
            try
            {
                string sql = @"DELETE FROM DetallesCajas
                                WHERE idCaja=@idCaja;
                                DELETE FROM Cajas
                                WHERE idCaja=@idCaja;
                                ";
                await _dapper.ExecuteAsync(sql, new { idCaja });
                return Ok();
            }
            catch (Exception ex)
            {
                return Tools.handleError(ex);
            }
        }
    }
}