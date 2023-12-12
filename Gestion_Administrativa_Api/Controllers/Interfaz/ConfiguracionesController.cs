using Dapper;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography.X509Certificates;

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
            public string validezFirma { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> datosEmpresa()
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT  e.""idEmpresa"",e.""identificacion"",
                            e.""razonSocial"",e.""telefono"",""ruta"",codigo,e.""idInformacionFirma"",e.""direccionMatriz""
                            FROM empresas e
                            LEFT JOIN ""informacionFirmas"" i ON i.""idInformacionFirma"" = e.""idInformacionFirma""
                            WHERE ""idEmpresa"" = @idEmpresa::UUID";
                var datos = await _dapper.QueryFirstOrDefaultAsync<datosSri>(sql, new { idEmpresa });
                datos.validezFirma = await validezFirma(datos);
                datos.codigo = null;
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<string> validezFirma([FromForm] datosSri _data)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                if (_data.archivoFirma != null)
                {
                    var fullPath = $"{Tools.rootPath}/Temp/";
                    var fileName = $"{idEmpresa}.p12";
                    try
                    {
                        if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
                        using (FileStream fs = System.IO.File.Create($"{fullPath}{fileName}")) _data.archivoFirma.CopyTo(fs);
                        var cert = new X509Certificate2($"{fullPath}{fileName}", _data.codigo);
                        return $"Firma valida hasta {cert.GetExpirationDateString()}";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    finally
                    {
                        if (System.IO.File.Exists($"{fullPath}{fileName}")) System.IO.File.Delete($"{fullPath}{fileName}");
                    }
                }
                else
                {
                    var rutaFirma = $"{Tools.rootPath}{_data.ruta}";
                    if (!System.IO.File.Exists(rutaFirma)) return "No se ha registrado una firma";
                    var cert = new X509Certificate2(rutaFirma, _data.codigo);
                    return $"Firma valida hasta {cert.GetExpirationDateString()}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}