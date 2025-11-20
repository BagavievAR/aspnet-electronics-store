using WebApp.Models;

namespace WebApp;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        db.Database.EnsureCreated(); // на всякий случай

        if (db.Categories.Any())
        {
            // Уже инициализировано – ничего не делаем
            return;
        }

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
                Category = smartphones,
                Brand = apple,
                ImageUrl = null,
                IsPublished = true
            },
            new Product
            {
                Title = "Samsung Galaxy S24",
                Description = "Топовый Android-смартфон.",
                Price = 79990,
                Stock = 10,
                Category = smartphones,
                Brand = samsung,
                ImageUrl = null,
                IsPublished = true
            },
            new Product
            {
                Title = "Xiaomi Redmi Buds 5",
                Description = "Беспроводные наушники.",
                Price = 3990,
                Stock = 25,
                Category = headphones,
                Brand = xiaomi,
                ImageUrl = null,
                IsPublished = true
            }
        );

        db.SaveChanges();
    }
}
