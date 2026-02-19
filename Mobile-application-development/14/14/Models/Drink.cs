namespace _14.Models;

/// <summary>
/// Базовый класс для всех напитков.
/// </summary>
public abstract class Drink
{
    public string Name { get; set; } = string.Empty;
    public double VolumeMl { get; set; }
    public string Temperature { get; set; } = "Горячий"; // Горячий / Холодный

    protected Drink(string name, double volumeMl, string temperature)
    {
        Name = name;
        VolumeMl = volumeMl;
        Temperature = temperature;
    }

    /// <summary>
    /// Возвращает краткое описание типа напитка.
    /// </summary>
    public abstract string GetDrinkType();

    /// <summary>
    /// Возвращает полную информацию о напитке.
    /// </summary>
    public virtual string GetInfo()
    {
        return $"Напиток: {Name}\nОбъём: {VolumeMl} мл\nТемпература: {Temperature}\nТип: {GetDrinkType()}";
    }

    /// <summary>
    /// Строка для отображения в списке на экране.
    /// </summary>
    public virtual string ToDisplayString()
    {
        return $"{GetDrinkType()} — {Name}, {VolumeMl} мл ({Temperature})";
    }

    public override string ToString() => ToDisplayString();

    /// <summary>Текст для отображения в списке (привязка в UI).</summary>
    public string DisplayText => ToDisplayString();
}
