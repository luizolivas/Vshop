using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        //private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)//, ITokenService tokenService)
        {
            _logger = logger;
            _productService = productService;
            //_tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProducts();

            if (result == null)
            {
                return View("Error");
            }
            return View(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
