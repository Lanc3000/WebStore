using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class AccauntController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register() => View();
    public IActionResult Login() => View();
    public IActionResult Logout() => View();
    public IActionResult AccessDenied() => View();
}
