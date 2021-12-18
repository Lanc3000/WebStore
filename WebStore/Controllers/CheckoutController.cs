using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
