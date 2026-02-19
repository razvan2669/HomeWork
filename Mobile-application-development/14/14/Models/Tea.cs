namespace _14.Models;

/// <summary>
/// Чай — вид напитка.
/// </summary>
public class Tea : Drink
{
    public string TeaType { get; set; } = "Чёрный"; // Чёрный, Зелёный, Белый, Травяной и т.д.

    public Tea(string name, double volumeMl, string temperature, string teaType)
        : base(name, volumeMl, temperature)
    {
        TeaType = teaType;
    }

    public override string GetDrinkType() => "Чай";

    public override string GetInfo()
    {
        return base.GetInfo() + $"\nСорт: {TeaType}";
    }

    public override string ToDisplayString()
    {
        return $"Чай ({TeaType}) — {Name}, {VolumeMl} мл";
    }
}
