using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class RegisterVm
{
    [Required]
    [Display(Name = "Логин")]
    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Повторите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = null!;
}
