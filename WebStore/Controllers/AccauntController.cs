using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class AccauntController : Controller
{
    public IActionResult Register() => View();
    public IActionResult Login() => View();
    public IActionResult Logout() => RedirectToAction("Index", "HomeController");
    public IActionResult AccessDenied() => View();
}
