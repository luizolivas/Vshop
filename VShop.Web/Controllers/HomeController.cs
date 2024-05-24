using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop.Web.Models;

namespace VShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            var product = await _productService.FindProductById(id);

            if (product is null)
            {
                return View("Error");
            }

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
