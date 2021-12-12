using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Hello from controller!");
            return View();
        }

        public string ConfiguredAction(string id)
        {
            return $"akdgja - {id}";
        }
    }
}
