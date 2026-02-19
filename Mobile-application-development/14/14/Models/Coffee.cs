namespace _14.Models;

/// <summary>
/// Кофе — вид напитка.
/// </summary>
public class Coffee : Drink
{
    public string CoffeeType { get; set; } = "Эспрессо"; // Эспрессо, Американо, Капучино, Латте и т.д.

    public Coffee(string name, double volumeMl, string temperature, string coffeeType)
        : base(name, volumeMl, temperature)
    {
        CoffeeType = coffeeType;
    }

    public override string GetDrinkType() => "Кофе";

    public override string GetInfo()
    {
        return base.GetInfo() + $"\nВид: {CoffeeType}";
    }

    public override string ToDisplayString()
    {
        return $"Кофе ({CoffeeType}) — {Name}, {VolumeMl} мл";
    }
}
