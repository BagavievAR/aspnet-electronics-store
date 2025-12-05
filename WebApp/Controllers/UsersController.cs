using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    // список пользователей
    public IActionResult Index()
    {
        var users = _db.Users
            .OrderBy(u => u.UserName)
            .ToList();

        return View(users);
    }

    // смена роли
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeRole(int id, string role)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        if (role != "User" && role != "Admin")
        {
            return BadRequest();
        }

        user.Role = role;
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
}
