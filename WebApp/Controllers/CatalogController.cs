using Microsoft.AspNetCore.Mvc;


public class CatalogController : Controller
{
    public IActionResult Index(string? category, string? brand, decimal? min, decimal? max, string sort = "popular", int page = 1)
    {
        ViewBag.Category = category; ViewBag.Brand = brand;
        ViewBag.Min = min; ViewBag.Max = max; ViewBag.Sort = sort; ViewBag.Page = page;
        return View();
    }
}
