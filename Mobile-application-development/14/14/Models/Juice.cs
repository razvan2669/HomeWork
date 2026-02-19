namespace _14.Models;

/// <summary>
/// Сок — вид напитка.
/// </summary>
public class Juice : Drink
{
    public string FruitType { get; set; } = "Яблочный"; // Яблочный, Апельсиновый, Виноградный и т.д.

    public Juice(string name, double volumeMl, string fruitType)
        : base(name, volumeMl, "Холодный")
    {
        FruitType = fruitType;
    }

    public override string GetDrinkType() => "Сок";

    public override string GetInfo()
    {
        return base.GetInfo() + $"\nВид: {FruitType}";
    }

    public override string ToDisplayString()
    {
        return $"Сок ({FruitType}) — {Name}, {VolumeMl} мл";
    }
}
