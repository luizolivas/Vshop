using Microsoft.AspNetCore.Mvc;

namespace VShop.ProductAPI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
