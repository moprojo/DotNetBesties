using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="bool"/> operations.
/// </summary>
public static class BoolExtensions
{
    /// <summary>
    /// Converts a boolean value to "Yes" or "No" string.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>"Yes" if true, "No" if false.</returns>
    public static string ToYesNo(this bool value)
        => BoolHelper.ToYesNo(value);

    /// <summary>
    /// Converts a boolean value to an integer (1 for true, 0 for false).
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>1 if true, 0 if false.</returns>
    public static int ToInt(this bool value)
        => BoolHelper.ToInt(value);

    /// <summary>
    /// Attempts to parse a string to a boolean value.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The parsed boolean value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(this string? value, out bool result)
        => BoolHelper.TryParse(value, out result);

    /// <summary>
    /// Parses a string to a boolean value.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed boolean value.</returns>
    /// <exception cref="FormatException">Thrown when the value cannot be parsed.</exception>
    public static bool ParseBool(this string value)
        => BoolHelper.Parse(value);

    /// <summary>
    /// Parses a string to a boolean value with a default value if parsing fails.
    /// Accepts "true"/"false", "1"/"0", "yes"/"no" (case-insensitive).
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="defaultValue">The default value to return if parsing fails.</param>
    /// <returns>The parsed boolean value or the default value.</returns>
    public static bool ParseBoolOrDefault(this string? value, bool defaultValue = false)
        => BoolHelper.ParseOrDefault(value, defaultValue);
}
