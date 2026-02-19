namespace _14.Models;

/// <summary>
/// Класс для хранения информации о песне.
/// </summary>
public class Song
{
    /// <summary>Название песни</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Исполнитель</summary>
    public string Artist { get; set; } = string.Empty;

    /// <summary>Год выпуска</summary>
    public int ReleaseYear { get; set; }

    /// <summary>Текст песни</summary>
    public string Lyrics { get; set; } = string.Empty;

    public Song() { }

    public Song(string title, string artist, int releaseYear, string lyrics)
    {
        Title = title;
        Artist = artist;
        ReleaseYear = releaseYear;
        Lyrics = lyrics;
    }

    /// <summary>
    /// Возвращает информацию о песне для отображения на экране.
    /// </summary>
    public string GetInfo()
    {
        return $"Название: {Title}\nИсполнитель: {Artist}\nГод выпуска: {ReleaseYear}\n\nТекст:\n{Lyrics}";
    }

    /// <summary>
    /// Краткая строка для списка.
    /// </summary>
    public string ToDisplayString()
    {
        return $"{Title} — {Artist} ({ReleaseYear})";
    }

    public override string ToString() => ToDisplayString();

    /// <summary>Текст для отображения в списке (привязка в UI).</summary>
    public string DisplayText => ToDisplayString();
}
