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

    [HttpPost, ValidateAntiForgeryToken] //защита от человека по середине
    public async Task<IActionResult> Register(RegisterUserViewModel Model)
    {
        if (!ModelState.IsValid) // проверка серверной валидации
            return View(Model);

        var user = new User 
        { 
            UserName = Model.UserName,
        };

        var registration_result = await _userManager.CreateAsync(user, Model.Password).ConfigureAwait(true);

        if (registration_result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Role.Users).ConfigureAwait(true);

            await _signInManager.SignInAsync(user, false).ConfigureAwait(true);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in registration_result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(Model);
    }
    public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl});
    
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        var login_result = await _signInManager.PasswordSignInAsync(
            Model.UserName,
            Model.Password,
            Model.RememberMe,
            true).ConfigureAwait(true);

        if (login_result.Succeeded)
        {
            // не безопасно - могут перехватить куки с данными 
            //return RedirectToAction(Model.ReturnUrl); 

            //if (Url.IsLocalUrl(Model.ReturnUrl))
            //    return Redirect(Model.ReturnUrl);
            //return RedirectToAction("Index", "Home"); 
            // заменяется сторокой ниже
            return LocalRedirect(Model.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

        return View(Model);
    }

    public async Task<IActionResult> Logout() 
    {
        await _signInManager.SignOutAsync().ConfigureAwait(true);
        return RedirectToAction("Index", "HomeController");
    }  
    public IActionResult AccessDenied() => View();
}
