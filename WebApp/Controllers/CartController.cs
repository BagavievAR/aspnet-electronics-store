using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class CartController : Controller
{
    public IActionResult Index() => View();
}
