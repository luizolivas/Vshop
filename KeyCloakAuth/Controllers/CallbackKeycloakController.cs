using KeyCloakAuth.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyCloakAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackKeycloakController : ControllerBase
    {
        private readonly ICallbackKeycloakService _callbackKeycloakService;
        public CallbackKeycloakController(ICallbackKeycloakService callbackKeycloakService)
        {
            _callbackKeycloakService = callbackKeycloakService;
        }

        [HttpGet]
        public async Task<IActionResult> KeycloakCallback(string session_state, string code)
        {

            // Troque o código de autorização por tokens de acesso
            await _callbackKeycloakService.ExchangeCodeForTokens(session_state, code);

            // Salve os tokens e autentique o usuário conforme necessário
            // ...

            // Redirecione para a página de destino após o login
            return Redirect("https://localhost:7097");
        }

        [HttpGet("/api/CallbackKeycloak/test")]
        public void KeycloakCallbackReturn(string code)
        {
            _callbackKeycloakService.RetryCode(code);
        }
    }
}
