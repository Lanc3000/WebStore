using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")] //нужно указывать область, чтоб приложение понимало какой контролер вызывать
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
