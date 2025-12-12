using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppStats _stats;

    public HomeController(ILogger<HomeController> logger, AppStats stats)
    {
        _logger = logger;
        _stats = stats;
    }

    public IActionResult Index()
    {
        // фиксируем просмотр главной страницы (Application state)
        _stats.IncrementHome();
        return View();
    }

    public IActionResult Privacy() => View();

    public IActionResult Contacts() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    // Переименовано, чтобы не конфликтовать с ControllerBase.StatusCode
    public IActionResult HttpStatus(int code)
    {
        if (code == 404) return View("NotFound");
        return View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult HidePromo()
    {
        Response.Cookies.Append(
            "HidePromoBanner",
            "1",
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = false,      // можно увидеть в браузере
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            });

        return RedirectToAction("Index");
    }
}
