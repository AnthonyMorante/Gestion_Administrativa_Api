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
            public string? identificacion { get; set; }
            public string? razonSocial { get; set; }
            public IFormFile? archivoFirma { get; set; }
            public IFormFile? archivoLogo { get; set; }
            public string? telefono { get; set; }
            public string? ruta { get; set; }
            public string? codigo { get; set; }
            public string? direccionMatriz { get; set; }
            public Guid? idInformacionFirma { get; set; }
            public string? validarFirma { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> datosEmpresa()
        {
            try
            {
                var datos = await _datosSri();
                datos.validarFirma = await validarFirma(datos);
                datos.codigo = null;
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private async Task<datosSri?> _datosSri()
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                string sql = @"SELECT  e.""idEmpresa"",e.""identificacion"",
                            e.""razonSocial"",e.""telefono"",""ruta"",codigo,e.""idInformacionFirma"",e.""direccionMatriz""
                            FROM empresas e
                            LEFT JOIN ""informacionFirmas"" i ON i.""idInformacionFirma"" = e.""idInformacionFirma""
                            WHERE ""idEmpresa"" = CAST(@idEmpresa AS UNIQUEIDENTIFIER)";
                return await _dapper.QueryFirstOrDefaultAsync<datosSri>(sql, new { idEmpresa });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<string> validarFirma([FromForm] datosSri _data)
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
                    var data = await _datosSri();
                    if (data == null) throw new Exception("No se han podido recuperar los datos");
                    var rutaFirma = $"{Tools.rootPath}{data.ruta}";
                    if (!System.IO.File.Exists(rutaFirma)) return "No se ha registrado una firma";
                    var cert = new X509Certificate2(rutaFirma, _data.codigo);
                    if (cert.NotAfter < DateTime.Now) throw new Exception($"La firma caduco {cert.GetExpirationDateString()}");
                    return $"Firma valida hasta {cert.GetExpirationDateString()}";
                }
            }
            catch (Exception ex)
            {
                return $"error:{ex.Message}";
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardar([FromForm] datosSri _data)
        {
            try
            {
                var idEmpresa = Guid.Parse(Tools.getIdEmpresa(HttpContext));
                var empresa = await _context.Empresas.FindAsync(idEmpresa);
                empresa.RazonSocial = _data.razonSocial;
                empresa.Telefono = _data.telefono;
                empresa.DireccionMatriz = _data.direccionMatriz;
                string sql = @"SELECT ""idInformacionFirma"" FROM ""informacionFirmas""
                               WHERE identificacion =@identificacion";
                var idInformacionFirma = await _dapper.ExecuteScalarAsync<Guid>(sql, empresa);
                if (idInformacionFirma == null)
                {
                    var informacionFirma = new InformacionFirmas();
                    informacionFirma.Ruta = $"/Firmas/{empresa.Identificacion}/{empresa.IdEmpresa}";
                    informacionFirma.Identificacion = empresa.Identificacion;
                    informacionFirma.RazonSocial = _data.razonSocial;
                    informacionFirma.Activo = true;
                    informacionFirma.Codigo = _data.codigo;
                    informacionFirma.FechaRegistro = DateTime.Now;
                    if (_data.archivoFirma != null)
                    {
                        var fullPath = $"{Tools.rootPath}/Facturacion/Firmas/{empresa.Identificacion}/";
                        var fileName = $"{idEmpresa}.p12";
                        if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
                        using (FileStream fs = System.IO.File.Create($"{fullPath}{fileName}")) _data.archivoFirma.CopyTo(fs);
                        informacionFirma.Ruta = $"/Facturacion/Firmas/{empresa.Identificacion}/{fileName}";
                    }
                    _context.InformacionFirmas.Add(informacionFirma);
                    empresa.IdInformacionFirma = informacionFirma.IdInformacionFirma;
                }
                else
                {
                    var informacionFirma = await _context.InformacionFirmas.FindAsync(idInformacionFirma);
                    informacionFirma.Ruta = $"/Facturacion/Firmas/{empresa.Identificacion}/{empresa.IdEmpresa}.p12";
                    informacionFirma.Identificacion = empresa.Identificacion;
                    informacionFirma.RazonSocial = _data.razonSocial;
                    informacionFirma.Activo = true;
                    if (_data.archivoFirma != null)
                    {
                        var fullPath = $"{Tools.rootPath}/Facturacion/Firmas/{empresa.Identificacion}/";
                        var fileName = $"{idEmpresa}.p12";
                        if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
                        using (FileStream fs = System.IO.File.Create($"{fullPath}{fileName}")) _data.archivoFirma.CopyTo(fs);
                        informacionFirma.Ruta = $"/Facturacion/Firmas/{empresa.Identificacion}/{fileName}";
                        informacionFirma.Codigo = _data.codigo;
                    }
                    _context.InformacionFirmas.Update(informacionFirma);
                    empresa.IdInformacionFirma = informacionFirma.IdInformacionFirma;
                }
                if (_data.archivoLogo != null)
                {
                    var fullPath = $"{Tools.rootPath}/Imagenes/Logos/{empresa.Identificacion}/";
                    var fileName = $"logo.png";
                    if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
                    using (FileStream fs = System.IO.File.Create($"{fullPath}{fileName}")) _data.archivoLogo.CopyTo(fs);
                }
                _context.Empresas.Update(empresa);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}