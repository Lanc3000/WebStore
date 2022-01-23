using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.Identity;

public class LoginViewModel 
{
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить")]
    public bool RememberMe { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string ReturnUrl { get; set; }
}
