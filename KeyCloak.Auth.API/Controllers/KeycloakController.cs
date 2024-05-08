using KeyCloak.Auth.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyCloak.Auth.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly KeycloakService _keycloakService;

        public AuthController(KeycloakService keycloakService)
        {
            _keycloakService = keycloakService;
        }

        // Endpoint para trocar o código de autorização por um token de acesso
        [HttpPost("token")]
        public async Task<IActionResult> ExchangeCodeForToken([FromBody] string authorizationCode)
        {
            var tokenResponse = await _keycloakService.ExchangeCodeForToken(authorizationCode);
            return Ok(tokenResponse);
        }
    }

}
