using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVm vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (_db.Users.Any(u => u.UserName == vm.UserName))
        {
            ModelState.AddModelError(nameof(vm.UserName), "Пользователь с таким логином уже существует");
            return View(vm);
        }

        var user = new AppUser
        {
            UserName = vm.UserName,
            PasswordHash = PasswordHelper.Hash(vm.Password),
            Role = "User"          // регистрируемых считаем обычными пользователями
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        SignIn(user);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginVm { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVm vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = _db.Users.FirstOrDefault(u => u.UserName == vm.UserName);
        if (user == null || user.PasswordHash != PasswordHelper.Hash(vm.Password))
        {
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
            return View(vm);
        }

        SignIn(user);

        if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
            return Redirect(vm.ReturnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Denied()
    {
        return View(); // простая страница "нет доступа"
    }

    private void SignIn(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var identity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal).GetAwaiter().GetResult();
    }
}
