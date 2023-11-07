using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Administrativa_Api.Controllers.Interfaz
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult validateToken()
        {
            try
            {
                if (!User.IsAuthenticated()) throw new Exception("La sesión ha caducado");
                return Ok();

            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
    }
}
