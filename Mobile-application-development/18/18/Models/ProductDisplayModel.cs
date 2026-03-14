namespace _18.Models;

/// <summary>
/// Класс для отображения товара в UI (Задание № 3)
/// </summary>
public class ProductDisplayModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int StockQuantity { get; set; }

    /// <summary>
    /// Форматированная цена для отображения
    /// </summary>
    public string FormattedPrice => $"{Price:N0} ₽";

    /// <summary>
    /// Признак наличия на складе
    /// </summary>
    public bool InStock => StockQuantity > 0;

    /// <summary>
    /// Создать из Product
    /// </summary>
    public static ProductDisplayModel FromProduct(Product product)
    {
        return new ProductDisplayModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Category = product.Category,
            StockQuantity = product.StockQuantity
        };
    }
}
