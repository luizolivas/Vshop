using KeyCloakAuth.Models;
using KeyCloakAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyCloakAuth.Controllers
{

    [Route("login")]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Login()
        {
            return View("ViewLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.Authenticate(model.username, model.password);
                if (result.Success)
                {
                    // Autenticação bem-sucedida, redirecionar para página inicial ou outra página protegida
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Autenticação falhou, exibir mensagem de erro
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
            }
            // Se chegou aqui, a validação falhou ou a autenticação falhou, retornar a página de login
            return View("ViewLogin", model);
        }
    }
}
