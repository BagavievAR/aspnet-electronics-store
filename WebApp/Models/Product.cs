using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = null!;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Range(0.01, 1_000_000)]
    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [Required]
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    [StringLength(300)]
    public string? ImageUrl { get; set; }

    public bool IsPublished { get; set; } = true;
}
