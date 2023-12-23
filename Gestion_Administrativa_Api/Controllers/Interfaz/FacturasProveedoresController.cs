using Gestion_Administrativa_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionAdministrativa_Api.Models.Factura;
using System.Text;
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

                XmlSerializer serializer = new XmlSerializer(typeof(FacturaProveedor));
                var factura = serializer.Deserialize(fileXml.OpenReadStream());
                return Ok(factura);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
