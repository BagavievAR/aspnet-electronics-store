using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

public class CatalogController : Controller
{
    private readonly AppDbContext _db;

    public CatalogController(AppDbContext db)
    {
        _db = db;
    }

    // Страница каталога доступна всем (включая гостей)
    [AllowAnonymous]
    public IActionResult Index(int? categoryId, int? brandId,
                               decimal? minPrice, decimal? maxPrice)
    {
        var query = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => p.IsPublished);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (brandId.HasValue)
            query = query.Where(p => p.BrandId == brandId.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        var vm = new CatalogPageVm
        {
            Products = query.ToList(),
            Categories = _db.Categories.OrderBy(c => c.Name).ToList(),
            Brands = _db.Brands.OrderBy(b => b.Name).ToList(),
            SelectedCategoryId = categoryId,
            SelectedBrandId = brandId,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        return View(vm);
    }
}
