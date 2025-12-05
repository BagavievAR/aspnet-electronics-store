using WebApp.Models;

namespace WebApp;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        db.Database.EnsureCreated();

        // --- Админ ---
        if (!db.Users.Any())
        {
            var admin = new AppUser
            {
                UserName = "admin",
                PasswordHash = PasswordHelper.Hash("Admin123!"), // пароль для входа
                Role = "Admin"
            };

            db.Users.Add(admin);
        }

        // --- Категории/бренды/товары ---
        if (!db.Categories.Any())
        {
            var smartphones = new Category { Name = "Смартфоны", Slug = "smartphones" };
            var laptops     = new Category { Name = "Ноутбуки", Slug = "laptops" };
            var headphones  = new Category { Name = "Наушники", Slug = "headphones" };

            var apple   = new Brand { Name = "Apple", Slug = "apple" };
            var samsung = new Brand { Name = "Samsung", Slug = "samsung" };
            var xiaomi  = new Brand { Name = "Xiaomi", Slug = "xiaomi" };

            db.Categories.AddRange(smartphones, laptops, headphones);
            db.Brands.AddRange(apple, samsung, xiaomi);

            db.Products.AddRange(
                new Product
                {
                    Title = "iPhone 15",
                    Description = "Флагманский смартфон Apple.",
                    Price = 99990,
                    Stock = 5,
                    ImageUrl = "https://i.postimg.cc/d1PMrjvd/apple-iphone-15-128gb-pink-mtp13pxa-c3f46-reference.webp",
                    Category = smartphones,
                    Brand = apple,
                    IsPublished = true
                },
                new Product
                {
                    Title = "Samsung Galaxy S24",
                    Description = "Топовый Android-смартфон.",
                    Price = 79990,
                    Stock = 10,
                    ImageUrl = "https://i.postimg.cc/Nfy4vqRm/preview0000.jpg",
                    Category = smartphones,
                    Brand = samsung,
                    IsPublished = true
                },
                new Product
                {
                    Title = "Samsung Galaxy S25 Ultra",
                    Description = "Смартфон Samsung Galaxy S25 Ultra сочетает в себе премиальный дизайн и передовые технологии для создания профессиональных фотографий и упрощения повседневных задач благодаря искусственному интеллекту Galaxy AI.",
                    Price = 108999,
                    Stock = 5,
                    ImageUrl = "https://i.ibb.co/VYyT71N7/viedtalruni-samsung-galaxy-s25-ultra-67-octa-7e68042c834e7ac06c7f1df0baea931b-reference.jpg",
                    Category = smartphones,
                    Brand = samsung,
                    IsPublished = true
                },
                new Product
                {
                    Title = "Xiaomi Redmi Buds 5",
                    Description = "Беспроводные наушники.",
                    Price = 3990,
                    Stock = 25,
                    ImageUrl = "https://i.postimg.cc/3Nz2xN2C/2c2a04a90d6e61547004dafc86b333ea.jpg",
                    Category = headphones,
                    Brand = xiaomi,
                    IsPublished = true
                }
            );
        }

        db.SaveChanges();
    }
}
