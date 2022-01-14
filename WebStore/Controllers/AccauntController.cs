using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers;

public class AccauntController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccauntController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel Model)
    {
        if (!ModelState.IsValid) // проверка серверной валидации
            return View(Model);

        var user = new User 
        { 
            UserName = Model.UserName,
        };

        var registration_result = await _userManager.CreateAsync(user, Model.Password);
        if (registration_result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in registration_result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(Model);
    }
    public IActionResult Login() => View();
    public IActionResult Logout() => RedirectToAction("Index", "HomeController");
    public IActionResult AccessDenied() => View();
}
