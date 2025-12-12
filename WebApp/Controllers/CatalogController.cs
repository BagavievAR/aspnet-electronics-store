using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Models;

namespace WebApp.Controllers;

public class CatalogController : Controller
{
    private readonly AppDbContext _db;
    private readonly AppStats _stats;
    private readonly IMemoryCache _cache;

    public CatalogController(AppDbContext db, AppStats stats, IMemoryCache cache)
    {
        _db = db;
        _stats = stats;
        _cache = cache;
    }

    // Страница каталога доступна всем (гости тоже видят товары)
    [AllowAnonymous]
    public IActionResult Index(int? categoryId, int? brandId,
                               decimal? minPrice, decimal? maxPrice)
    {
        // Application-состояние: считаем просмотры каталога
        _stats.IncrementCatalog();

        // Базовый запрос по товарам
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

        // ---------- КЭШ КАТЕГОРИЙ ----------
        // В кэше храним: время создания + список категорий
        var categoriesEntry = _cache.GetOrCreate<(DateTime Created, List<Category> Items)>("categories", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

            var items = _db.Categories
                .OrderBy(c => c.Name)
                .ToList();

            return (DateTime.Now, items);
        });

        // ---------- КЭШ БРЕНДОВ ----------
        var brandsEntry = _cache.GetOrCreate<(DateTime Created, List<Brand> Items)>("brands", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

            var items = _db.Brands
                .OrderBy(b => b.Name)
                .ToList();

            return (DateTime.Now, items);
        });

        var vm = new CatalogPageVm
        {
            Products = query.ToList(),

            Categories = categoriesEntry.Items,
            Brands = brandsEntry.Items,

            CategoriesGenerated = categoriesEntry.Created,
            BrandsGenerated = brandsEntry.Created,

            SelectedCategoryId = categoryId,
            SelectedBrandId = brandId,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        return View(vm);
    }
}
