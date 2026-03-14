using _19.Models;

namespace _19.Services;

/// <summary>Сервис магазина (Задание 2)</summary>
public class StoreService
{
    private static readonly List<Product> Products =
    [
        new Product { Id = 1, Name = "Смартфон Galaxy Pro", Description = "Современный смартфон с отличной камерой", Price = 29990, ImageUrl = "📱", Category = "Электроника", StockQuantity = 15 },
        new Product { Id = 2, Name = "Беспроводные наушники", Description = "Звук премиум класса, 30ч работы", Price = 4990, ImageUrl = "🎧", Category = "Аксессуары", StockQuantity = 42 },
        new Product { Id = 3, Name = "Умные часы", Description = "Фитнес-трекер и уведомления", Price = 8990, ImageUrl = "⌚", Category = "Электроника", StockQuantity = 28 },
        new Product { Id = 4, Name = "Портативная колонка", Description = "Мощный бас, Bluetooth 5.0", Price = 3490, ImageUrl = "🔊", Category = "Аксессуары", StockQuantity = 55 },
        new Product { Id = 5, Name = "Механическая клавиатура", Description = "RGB подсветка, переключатели MX", Price = 7990, ImageUrl = "⌨️", Category = "Периферия", StockQuantity = 20 },
        new Product { Id = 6, Name = "Веб-камера HD", Description = "1080p, автофокус", Price = 2990, ImageUrl = "📷", Category = "Периферия", StockQuantity = 35 },
    ];

    public List<ProductDisplayModel> GetAllProducts() =>
        Products.Select(ProductDisplayModel.FromProduct).ToList();

    public ProductDisplayModel? GetProductById(int id)
    {
        var p = Products.FirstOrDefault(x => x.Id == id);
        return p != null ? ProductDisplayModel.FromProduct(p) : null;
    }
}
