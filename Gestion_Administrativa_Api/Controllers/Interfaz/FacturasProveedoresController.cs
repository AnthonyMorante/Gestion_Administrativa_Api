using Gestion_Administrativa_Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasProveedoresController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> leerXml([FromForm] IFormFile fileXml)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Tools.Factura));
                var factura = (Tools.Factura)serializer.Deserialize(fileXml.OpenReadStream());
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}