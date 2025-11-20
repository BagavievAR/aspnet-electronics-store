using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _db;

    public ProductController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Details(int id)
    {
        var product = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == id && p.IsPublished);

        if (product == null) return NotFound();

        return View(product);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new ProductEditVm
        {
            Categories = _db.Categories.OrderBy(c => c.Name).ToList(),
            Brands = _db.Brands.OrderBy(b => b.Name).ToList()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductEditVm vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Categories = _db.Categories.OrderBy(c => c.Name).ToList();
            vm.Brands = _db.Brands.OrderBy(b => b.Name).ToList();
            return View(vm);
        }

        var product = new Product
        {
            Title = vm.Title,
            Description = vm.Description,
            Price = vm.Price,
            Stock = vm.Stock,
            CategoryId = vm.CategoryId,
            BrandId = vm.BrandId,
            ImageUrl = vm.ImageUrl,
            IsPublished = true
        };

        _db.Products.Add(product);
        _db.SaveChanges();

        return RedirectToAction("Details", new { id = product.Id });
    }

    // ---------- РЕДАКТИРОВАНИЕ ----------

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        var vm = new ProductEditVm
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            BrandId = product.BrandId,
            ImageUrl = product.ImageUrl,
            Categories = _db.Categories.OrderBy(c => c.Name).ToList(),
            Brands = _db.Brands.OrderBy(b => b.Name).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ProductEditVm vm)
    {
        if (id != vm.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            vm.Categories = _db.Categories.OrderBy(c => c.Name).ToList();
            vm.Brands = _db.Brands.OrderBy(b => b.Name).ToList();
            return View(vm);
        }

        var product = _db.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Title = vm.Title;
        product.Description = vm.Description;
        product.Price = vm.Price;
        product.Stock = vm.Stock;
        product.CategoryId = vm.CategoryId;
        product.BrandId = vm.BrandId;
        product.ImageUrl = vm.ImageUrl;

        _db.SaveChanges();

        return RedirectToAction("Details", new { id = product.Id });
    }

    // ---------- УДАЛЕНИЕ ----------

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        _db.Products.Remove(product);
        _db.SaveChanges();

        return RedirectToAction("Index", "Catalog");
    }
}
