using System;
using System.Globalization;
using System.Linq;

namespace DotNetBesties.Helpers.Common;

/// <summary>
/// Helper methods for working with <see cref="CultureInfo"/>.
/// </summary>
public static class CultureInfoHelper
{
    /// <summary>
    /// Gets the invariant culture.
    /// </summary>
    public static CultureInfo InvariantCulture => CultureInfo.InvariantCulture;

    /// <summary>
    /// Gets the current culture.
    /// </summary>
    public static CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

    /// <summary>
    /// Gets the current UI culture.
    /// </summary>
    public static CultureInfo CurrentUICulture => CultureInfo.CurrentUICulture;

    /// <summary>
    /// Tries to get a culture by name.
    /// </summary>
    /// <param name="name">The culture name (e.g., "en-US", "de-DE").</param>
    /// <param name="culture">The culture if found.</param>
    /// <returns><c>true</c> if the culture was found; otherwise, <c>false</c>.</returns>
    public static bool TryGetCulture(string name, out CultureInfo? culture)
    {
        culture = null;

        if (string.IsNullOrWhiteSpace(name))
            return false;

        try
        {
            culture = CultureInfo.GetCultureInfo(name);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a culture by name, or returns the invariant culture if not found.
    /// </summary>
    /// <param name="name">The culture name.</param>
    /// <returns>The culture, or invariant culture if not found.</returns>
    public static CultureInfo GetCultureOrInvariant(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CultureInfo.InvariantCulture;

        try
        {
            return CultureInfo.GetCultureInfo(name);
        }
        catch
        {
            return CultureInfo.InvariantCulture;
        }
    }

    /// <summary>
    /// Gets all available cultures.
    /// </summary>
    /// <returns>An array of all available cultures.</returns>
    public static CultureInfo[] GetAllCultures()
        => CultureInfo.GetCultures(CultureTypes.AllCultures);

    /// <summary>
    /// Gets all specific cultures (not neutral).
    /// </summary>
    /// <returns>An array of all specific cultures.</returns>
    public static CultureInfo[] GetSpecificCultures()
        => CultureInfo.GetCultures(CultureTypes.SpecificCultures);

    /// <summary>
    /// Gets all neutral cultures.
    /// </summary>
    /// <returns>An array of all neutral cultures.</returns>
    public static CultureInfo[] GetNeutralCultures()
        => CultureInfo.GetCultures(CultureTypes.NeutralCultures);

    /// <summary>
    /// Checks if a culture name is valid.
    /// </summary>
    /// <param name="name">The culture name to check.</param>
    /// <returns><c>true</c> if the culture name is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidCultureName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        try
        {
            _ = CultureInfo.GetCultureInfo(name);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the currency symbol for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The currency symbol.</returns>
    public static string GetCurrencySymbol(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.NumberFormat.CurrencySymbol;
    }

    /// <summary>
    /// Gets the decimal separator for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The decimal separator.</returns>
    public static string GetDecimalSeparator(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.NumberFormat.NumberDecimalSeparator;
    }

    /// <summary>
    /// Gets the thousands separator for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The thousands separator.</returns>
    public static string GetThousandsSeparator(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.NumberFormat.NumberGroupSeparator;
    }

    /// <summary>
    /// Gets the date separator for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The date separator.</returns>
    public static string GetDateSeparator(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.DateSeparator;
    }

    /// <summary>
    /// Gets the time separator for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The time separator.</returns>
    public static string GetTimeSeparator(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.TimeSeparator;
    }

    /// <summary>
    /// Gets the short date pattern for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The short date pattern.</returns>
    public static string GetShortDatePattern(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.ShortDatePattern;
    }

    /// <summary>
    /// Gets the long date pattern for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The long date pattern.</returns>
    public static string GetLongDatePattern(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.LongDatePattern;
    }

    /// <summary>
    /// Gets the short time pattern for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The short time pattern.</returns>
    public static string GetShortTimePattern(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.ShortTimePattern;
    }

    /// <summary>
    /// Gets the long time pattern for a culture.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The long time pattern.</returns>
    public static string GetLongTimePattern(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return culture.DateTimeFormat.LongTimePattern;
    }

    /// <summary>
    /// Determines if a culture uses 24-hour time format.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns><c>true</c> if the culture uses 24-hour format; otherwise, <c>false</c>.</returns>
    public static bool Uses24HourFormat(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        var shortTimePattern = culture.DateTimeFormat.ShortTimePattern;
        return !shortTimePattern.Contains("t", StringComparison.OrdinalIgnoreCase);
    }
}
