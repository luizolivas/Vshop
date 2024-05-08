using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductAPI.Services;

namespace VShop.ProductAPI.Controllers
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
        public async Task<IActionResult> KeycloakCallback(string code)
        {

            // Troque o código de autorização por tokens de acesso
            await _callbackKeycloakService.ExchangeCodeForTokens(code);

            // Salve os tokens e autentique o usuário conforme necessário
            // ...

            // Redirecione para a página de destino após o login
            return RedirectToAction("Index", "Home");
        }
    }
}
