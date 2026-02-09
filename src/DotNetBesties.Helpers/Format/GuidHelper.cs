using System;
using System.Linq;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="Guid"/> values.
/// </summary>
public static class GuidHelper
{
    #region Guid
    /// <summary>
    /// Returns an empty GUID.
    /// </summary>
    public static Guid Empty()
        => Guid.Empty;

    /// <summary>
    /// Creates a new GUID.
    /// </summary>
    public static Guid NewGuid()
        => Guid.NewGuid();

    /// <summary>
    /// Parses the string to a GUID.
    /// </summary>
    public static Guid Parse(string input)
        => Guid.Parse(input);

    /// <summary>
    /// Attempts to parse the string to a GUID. Returns <c>null</c> if parsing fails.
    /// </summary>
    public static Guid? ParseOrNull(string? input)
        => Guid.TryParse(input, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse the string to a GUID using the exact format. Returns <c>null</c> if parsing fails.
    /// </summary>
    public static Guid? ParseExactOrNull(string? input, string format)
        => Guid.TryParseExact(input, format, out var result) ? result : null;

    /// <summary>
    /// Determines whether the Guid is empty (all zeros).
    /// </summary>
    /// <param name="value">The Guid to check.</param>
    /// <returns>True if the Guid is empty; otherwise, false.</returns>
    public static bool IsEmpty(Guid value)
        => value == Guid.Empty;

    /// <summary>
    /// Determines whether the nullable Guid is null or empty.
    /// </summary>
    /// <param name="value">The nullable Guid to check.</param>
    /// <returns>True if the Guid is null or empty; otherwise, false.</returns>
    public static bool IsNullOrEmpty(Guid? value)
        => !value.HasValue || value.Value == Guid.Empty;

    /// <summary>
    /// Converts the Guid to a string using the specified format.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <param name="format">The format specifier: "N", "D", "B", "P", or "X".</param>
    /// <returns>The formatted Guid string.</returns>
    public static string ToString(Guid value, char format)
        => value.ToString(format.ToString());

    /// <summary>
    /// Converts the Guid to a string without hyphens (format "N").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a 32-character hex string without hyphens.</returns>
    public static string ToStringN(Guid value)
        => value.ToString("N");

    /// <summary>
    /// Converts the Guid to a string with hyphens (format "D"). This is the default format.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a 36-character string with hyphens.</returns>
    public static string ToStringD(Guid value)
        => value.ToString("D");

    /// <summary>
    /// Converts the Guid to a string enclosed in braces (format "B").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a string enclosed in braces.</returns>
    public static string ToStringB(Guid value)
        => value.ToString("B");

    /// <summary>
    /// Converts the Guid to a string enclosed in parentheses (format "P").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a string enclosed in parentheses.</returns>
    public static string ToStringP(Guid value)
        => value.ToString("P");

    /// <summary>
    /// Converts the Guid to a hexadecimal array format (format "X").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a hexadecimal array string.</returns>
    public static string ToStringX(Guid value)
        => value.ToString("X");

    /// <summary>
    /// Converts the Guid to a Base64 string (shorter than hex representation).
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A Base64 string representation of the Guid.</returns>
    public static string ToBase64String(Guid value)
        => Convert.ToBase64String(value.ToByteArray());

    /// <summary>
    /// Converts the Guid to a URL-safe Base64 string (replaces + with - and / with _).
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A URL-safe Base64 string representation.</returns>
    public static string ToBase64UrlString(Guid value)
    {
        var base64 = Convert.ToBase64String(value.ToByteArray());
        return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }

    /// <summary>
    /// Converts the Guid to a short 22-character string representation.
    /// Uses Base64 encoding without padding and with URL-safe characters.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A 22-character string representation.</returns>
    public static string ToShortString(Guid value)
        => ToBase64UrlString(value);

    /// <summary>
    /// Determines whether the Guid matches any of the specified Guids.
    /// </summary>
    /// <param name="value">The Guid to check.</param>
    /// <param name="guids">The Guids to compare against.</param>
    /// <returns>True if the Guid matches any of the specified Guids; otherwise, false.</returns>
    public static bool IsAnyOf(Guid value, params Guid[] guids)
    {
        ArgumentNullException.ThrowIfNull(guids);
        return guids.Contains(value);
    }
    #endregion
}
