using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class BlogsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details()
    {
        return View();
    }
}

