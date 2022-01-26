using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrators)] //нужно указывать область, чтоб приложение понимало какой контролер вызывать
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
