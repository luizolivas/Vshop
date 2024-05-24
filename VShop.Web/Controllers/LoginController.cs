using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services;

namespace VShop.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<IActionResult> Login()
        {
            bool isLoggedOut = await _loginService.LoginLogout();

            if (isLoggedOut)
            {
                // Usuário foi deslogado, redirecione para a Home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Usuário foi redirecionado para o login, não faça nada
                return new EmptyResult();
            }
        }
    }
}
