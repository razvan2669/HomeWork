namespace _18.Models;

public class CartItemDisplay
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity;

    public string DisplayText => $"{Subtotal:N0} ₽ · {Quantity} шт.";
    public string ImageUrl { get; set; } = string.Empty;
}
