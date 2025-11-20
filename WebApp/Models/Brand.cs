using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Brand
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    [Required, StringLength(100)]
    public string Slug { get; set; } = null!;

    public bool IsVisible { get; set; } = true;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
