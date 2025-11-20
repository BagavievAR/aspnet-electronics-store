using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ProductEditVm
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
    [Display(Name = "Категория")]
    public int CategoryId { get; set; }

    [Required]
    [Display(Name = "Бренд")]
    public int BrandId { get; set; }

    [Display(Name = "URL изображения")]
    public string? ImageUrl { get; set; }

    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    public IEnumerable<Brand> Brands { get; set; } = Enumerable.Empty<Brand>();
}
