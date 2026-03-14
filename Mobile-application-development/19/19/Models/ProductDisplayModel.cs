namespace _19.Models;

/// <summary>Класс для отображения товара (Задание 3)</summary>
public class ProductDisplayModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool InStock => StockQuantity > 0;
    public int StockQuantity { get; set; }

    public string FormattedPrice => $"{Price:N0} ₽";

    public static ProductDisplayModel FromProduct(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        ImageUrl = p.ImageUrl,
        Category = p.Category,
        StockQuantity = p.StockQuantity
    };
}
