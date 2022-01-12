using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Hello from controller!");
            //ControllerContext.HttpContext.Request.RouteValues
            return View();
        }

        public string ConfiguredAction(string id, string Value)
        {
            return $"Hello {id} - {Value}";
        }

        public void Throw(string Message) => throw new ApplicationException(Message);
    }
}
