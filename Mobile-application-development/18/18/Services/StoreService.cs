using System.Collections.ObjectModel;
using _18.Models;

namespace _18.Services;

public class StoreService
{
    private static readonly List<Product> Products =
    [
        new Product { Id = 1, Name = "Смартфон Galaxy Pro", Description = "Современный смартфон с отличной камерой", Price = 29990, ImageUrl = "📱", Category = "Электроника", StockQuantity = 15 },
        new Product { Id = 2, Name = "Беспроводные наушники", Description = "Звук премиум класса, 30ч работы", Price = 4990, ImageUrl = "🎧", Category = "Аксессуары", StockQuantity = 42 },
        new Product { Id = 3, Name = "Умные часы", Description = "Фитнес-трекер и уведомления", Price = 8990, ImageUrl = "⌚", Category = "Электроника", StockQuantity = 28 },
        new Product { Id = 4, Name = "Портативная колонка", Description = "Мощный бас, Bluetooth 5.0", Price = 3490, ImageUrl = "🔊", Category = "Аксессуары", StockQuantity = 55 },
        new Product { Id = 5, Name = "Рюкзак для ноутбука", Description = "Водоотталкивающая ткань, 15.6\"", Price = 2490, ImageUrl = "🎒", Category = "Аксессуары", StockQuantity = 30 },
        new Product { Id = 6, Name = "Клавиатура механическая", Description = "RGB подсветка, переключатели MX", Price = 7990, ImageUrl = "⌨️", Category = "Периферия", StockQuantity = 20 },
        new Product { Id = 7, Name = "Веб-камера HD", Description = "1080p, автофокус", Price = 2990, ImageUrl = "📷", Category = "Периферия", StockQuantity = 35 },
        new Product { Id = 8, Name = "USB‑хаб 4-портовый", Description = "USB 3.0, быстрая зарядка", Price = 1290, ImageUrl = "🔌", Category = "Аксессуары", StockQuantity = 100 },
    ];

    private static readonly List<CartItem> Cart = [];
    private static readonly List<Customer> Customers = [];
    private static readonly List<Order> Orders = [];
    private static int _customerIdCounter = 1;
    private static int _orderIdCounter = 1;

    public IReadOnlyList<Product> GetAllProducts() => Products;

    public Product? GetProductById(int id) => Products.FirstOrDefault(p => p.Id == id);

    public Task<List<ProductDisplayModel>> GetAllProductsAsync()
    {
        var list = Products.Select(ProductDisplayModel.FromProduct).ToList();
        return Task.FromResult(list);
    }

    public Task<ProductDisplayModel?> GetProductDisplayAsync(int id)
    {
        var p = GetProductById(id);
        return Task.FromResult(p != null ? ProductDisplayModel.FromProduct(p) : null);
    }

    public Task AddToCartAsync(int productId, int quantity)
    {
        var existing = Cart.FirstOrDefault(c => c.ProductId == productId);
        if (existing != null)
            existing.Quantity += quantity;
        else
            Cart.Add(new CartItem { ProductId = productId, Quantity = quantity });
        return Task.CompletedTask;
    }

    public Task<List<CartItemDisplay>> GetCartItemsAsync()
    {
        var list = Cart.Select(c =>
        {
            var p = GetProductById(c.ProductId)!;
            return new CartItemDisplay
            {
                ProductId = c.ProductId,
                Name = p.Name,
                Price = p.Price,
                Quantity = c.Quantity,
                ImageUrl = p.ImageUrl
            };
        }).ToList();
        return Task.FromResult(list);
    }

    public Task RemoveFromCartAsync(int productId)
    {
        var item = Cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null) Cart.Remove(item);
        return Task.CompletedTask;
    }

    public Task<Customer> GetOrCreateCustomerAsync(string name, string email)
    {
        var parts = name.Trim().Split(' ', 2);
        var customer = new Customer
        {
            Id = _customerIdCounter++,
            FirstName = parts.Length > 0 ? parts[0] : name,
            LastName = parts.Length > 1 ? parts[1] : "",
            Email = email.Trim()
        };
        Customers.Add(customer);
        return Task.FromResult(customer);
    }

    public Task<Order?> CreateOrderFromCartAsync(int customerId)
    {
        if (Cart.Count == 0) return Task.FromResult<Order?>(null);

        var total = Cart.Sum(c =>
        {
            var p = GetProductById(c.ProductId);
            return (p?.Price ?? 0) * c.Quantity;
        });

        var order = new Order
        {
            Id = _orderIdCounter++,
            CustomerId = customerId,
            OrderDate = DateTime.Now,
            TotalAmount = total,
            Status = "Новый"
        };
        Orders.Add(order);
        Cart.Clear();
        return Task.FromResult<Order?>(order);
    }

    private class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
