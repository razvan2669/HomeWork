namespace _18.Services;

/// <summary>
/// Валидация email по популярным доменам в РФ
/// </summary>
public static class EmailValidator
{
    /// <summary>
    /// 20+ самых популярных почтовых доменов в России и СНГ
    /// </summary>
    private static readonly HashSet<string> AllowedDomains =
    [
        "gmail.com",
        "yandex.ru",
        "ya.ru",
        "mail.ru",
        "inbox.ru",
        "list.ru",
        "bk.ru",
        "rambler.ru",
        "outlook.com",
        "hotmail.com",
        "live.ru",
        "icloud.com",
        "ukr.net",
        "tut.by",
        "mail.ua",
        "yahoo.com",
        "protonmail.com",
        "gmx.com",
        "zoho.com",
        "ya.by",
        "meta.ua",
    ];

    /// <summary>
    /// Проверяет, что email имеет допустимый домен из списка популярных
    /// </summary>
    public static bool IsValidDomain(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var parts = email.Trim().Split('@');
        if (parts.Length != 2) return false;

        var domain = parts[1].ToLowerInvariant();
        return AllowedDomains.Contains(domain);
    }

    /// <summary>
    /// Базовая проверка формата email (есть @, часть до и после)
    /// </summary>
    public static bool IsValidFormat(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        email = email.Trim();
        if (email.Length < 5) return false;

        var idx = email.IndexOf('@');
        if (idx <= 0 || idx >= email.Length - 1) return false;
        if (email.IndexOf('@', idx + 1) >= 0) return false; // несколько @

        var domain = email[(idx + 1)..];
        return domain.Contains('.') && domain.Length >= 4;
    }

    /// <summary>
    /// Краткая проверка (формат + домен) — для использования в коде
    /// </summary>
    public static bool IsValid(string? email)
    {
        var (isValid, _) = Validate(email);
        return isValid;
    }

    /// <summary>
    /// Полная проверка: формат + допустимый домен
    /// </summary>
    public static (bool IsValid, string? ErrorMessage) Validate(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return (false, "Введите email");

        if (!IsValidFormat(email))
            return (false, "Неверный формат email (пример: user@mail.ru)");

        if (!IsValidDomain(email))
            return (false, "Используйте популярный почтовый домен: gmail.com, yandex.ru, mail.ru, outlook.com и др.");

        return (true, null);
    }
}
