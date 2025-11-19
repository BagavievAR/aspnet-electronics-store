using Microsoft.AspNetCore.Mvc;


public class ProductController : Controller
{
    [Route("product/{slug}")]
    public IActionResult Details(string slug)
    {
        ViewBag.Slug = slug;
        return View();
    }
}
