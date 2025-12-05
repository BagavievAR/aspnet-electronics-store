using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AppUser
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string Role { get; set; } = "User";  // User или Admin
}
