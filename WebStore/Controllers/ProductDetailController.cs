using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ProductDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
