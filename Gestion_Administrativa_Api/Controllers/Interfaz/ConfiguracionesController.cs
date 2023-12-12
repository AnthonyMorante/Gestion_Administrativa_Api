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
    public class ConfiguracionesController : ControllerBase
    {
        private readonly _context _context;
        private readonly IDbConnection _dapper;

        public ConfiguracionesController(_context context, IDbConnection dapper)
        {
            _dapper = dapper;
            _context = context;
        }

        public class datosSri
        {
            public Guid? idEmpresa { get; set; }
            public string identificacion { get; set; }
            public string razonSocial { get; set; }
            public IFormFile? archivoFirma { get; set; }
            public IFormFile? archivoLogo { get; set; }
            public string telefono { get; set; }
            public string? ruta { get; set; }
            public string? codigo { get; set; }
            public string direccionMatriz { get; set; }
            public Guid? idInformacionFirma { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> datosEmpresa()
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT  e.""idEmpresa"",e.""identificacion"",
                            e.""razonSocial"",e.""telefono"",""ruta"",e.""idInformacionFirma"",e.""direccionMatriz""
                            FROM empresas e
                            LEFT JOIN ""informacionFirmas"" i ON i.""idInformacionFirma"" = e.""idInformacionFirma""
                            WHERE ""idEmpresa"" = @idEmpresa::UUID";
                return Ok(await _dapper.QueryFirstOrDefaultAsync(sql, new { idEmpresa }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}