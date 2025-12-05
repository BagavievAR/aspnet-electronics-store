using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class LoginVm
{
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    public string? ReturnUrl { get; set; }
}
