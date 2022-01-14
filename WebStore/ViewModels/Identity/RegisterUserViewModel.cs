using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.Identity;

public class RegisterUserViewModel
{
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Подтверждение пароля")]
    public string PasswordConfirm { get; set; }
}
