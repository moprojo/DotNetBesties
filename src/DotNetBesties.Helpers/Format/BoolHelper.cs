using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="bool"/> results.
/// </summary>
public static class BoolHelper
{
    #region Conversion

    /// <summary>
    /// Converts a boolean value to "Yes" or "No" string.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>"Yes" if true, "No" if false.</returns>
    public static string ToYesNo(bool value)
        => value ? "Yes" : "No";

    /// <summary>
    /// Converts a boolean value to an integer (1 for true, 0 for false).
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>1 if true, 0 if false.</returns>
    public static int ToInt(bool value)
        => value ? 1 : 0;

    #endregion

    #region Parsing

    /// <summary>
    /// Attempts to parse a string to a boolean value.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The parsed boolean value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? value, out bool result)
    {
        result = false;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var trimmed = value.Trim();

        // Standard boolean parsing
        if (bool.TryParse(trimmed, out result))
            return true;

        // Check for numeric values
        if (trimmed == "1")
        {
            result = true;
            return true;
        }

        if (trimmed == "0")
        {
            result = false;
            return true;
        }
        
        // Check for yes/no (case-insensitive)
        if (trimmed.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            result = true;
            return true;
        }

        if (trimmed.Equals("no", StringComparison.OrdinalIgnoreCase))
        {
            result = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Parses a string to a boolean value.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed boolean value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    /// <exception cref="FormatException">Thrown when the value cannot be parsed.</exception>
    public static bool Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (TryParse(value, out bool result))
            return result;

        throw new FormatException($"String '{value}' was not recognized as a valid Boolean.");
    }

    /// <summary>
    /// Parses a string to a boolean value with a default value if parsing fails.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="defaultValue">The default value to return if parsing fails.</param>
    /// <returns>The parsed boolean value or the default value.</returns>
    public static bool ParseOrDefault(string? value, bool defaultValue = false)
        => TryParse(value, out bool result) ? result : defaultValue;

    #endregion

    #region DateOnly
    /// <summary>
    /// Attempts to parse a <see cref="DateOnly"/> using the specified format and invariant culture.
    /// </summary>
    public static bool TryParseExactDateOnlyInvariant(string? input, string format, out DateOnly result)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateOnly"/> using any of the provided formats and invariant culture.
    /// </summary>
    public static bool TryParseExactDateOnlyInvariant(string? input, string[] formats, out DateOnly result)
        => DateOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    #endregion

    #region DateTime
    /// <summary>
    /// Attempts to parse a <see cref="DateTime"/> using the specified format, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeInvariant(string? input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateTime"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeInvariant(string? input, string[] formats, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Attempts to parse a <see cref="DateTimeOffset"/> using the specified format, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateTimeOffset"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region Long
    /// <summary>
    /// Attempts to parse an <see cref="long"/> using invariant culture.
    /// </summary>
    public static bool TryParseLongInvariant(string? input, out long result)
        => long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Attempts to parse a <see cref="TimeSpan"/> using the specified format and invariant culture.
    /// </summary>
    public static bool TryParseExactTimeSpanInvariant(string? input, string format, out TimeSpan result)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out result);

    /// <summary>
    /// Attempts to parse a <see cref="TimeSpan"/> using any of the provided formats and invariant culture.
    /// </summary>
    public static bool TryParseExactTimeSpanInvariant(string? input, string[] formats, out TimeSpan result)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out result);
    #endregion
}
