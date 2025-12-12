using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize] // ← корзина только для авторизованных
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existing == null)
                cart.Add(new CartItem { ProductId = productId });
            else
                existing.Quantity++;

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            cart.RemoveAll(c => c.ProductId == productId);

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("Index");
        }
    }
}
