using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosEmitirController : ControllerBase
    {
        private readonly IDocumentosEmitir _IDocumentosEmitir;

        public DocumentosEmitirController(IDocumentosEmitir IDocumentosEmitir)
        {
            _IDocumentosEmitir = IDocumentosEmitir;
        }

        [HttpGet]
        [Route("[action]/{codigo}")]
        public async Task<IActionResult> listar(int codigo)
        {
            try
            {
                var consulta = await _IDocumentosEmitir.listar(codigo);
                return StatusCode(200, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "error", exc = ex });
            }
        }
    }
}