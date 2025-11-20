namespace WebApp.Models;

public class CatalogPageVm
{
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    public IEnumerable<Brand> Brands { get; set; } = Enumerable.Empty<Brand>();

    public int? SelectedCategoryId { get; set; }
    public int? SelectedBrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
