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

        // Endpoint para iniciar o fluxo de autorização
        public void Login()
        {
            _loginService.StartAuthorizationFlow();

        }
    }
}
