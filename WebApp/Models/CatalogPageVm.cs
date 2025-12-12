using System;
using System.Collections.Generic;

namespace WebApp.Models;

public class CatalogPageVm
{
    public List<Product> Products { get; set; } = new();

    public List<Category> Categories { get; set; } = new();
    public List<Brand> Brands { get; set; } = new();

    // Выбранные фильтры
    public int? SelectedCategoryId { get; set; }
    public int? SelectedBrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    // Время генерации данных в кэше
    public DateTime CategoriesGenerated { get; set; }
    public DateTime BrandsGenerated { get; set; }
}
