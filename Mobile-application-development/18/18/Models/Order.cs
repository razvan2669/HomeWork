namespace _18.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Новый"; // Новый, В обработке, Доставлен

    public int OrderId => Id;
}
