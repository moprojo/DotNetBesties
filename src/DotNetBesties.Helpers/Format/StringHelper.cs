using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Conversion helpers that produce <see cref="string"/> results.
/// </summary>
public static class StringHelper
{
    #region Null/Empty Checks

    /// <summary>
    /// Indicates whether the specified string is null or empty.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrEmpty(string? value)
        => string.IsNullOrEmpty(value);

    /// <summary>
    /// Indicates whether the specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrWhiteSpace(string? value)
        => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Indicates whether the specified string is not null and not empty.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is not null and not empty; otherwise, <c>false</c>.</returns>
    public static bool HasValue(string? value)
        => !string.IsNullOrEmpty(value);

    /// <summary>
    /// Returns the string if it has a value, otherwise returns the default value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="defaultValue">The default value to return.</param>
    /// <returns>The original string or the default value.</returns>
    public static string DefaultIfEmpty(string? value, string defaultValue)
        => string.IsNullOrEmpty(value) ? defaultValue : value;

    #endregion

    #region Color
    /// <summary>
    /// Converts RGB values to a Hex string
    /// </summary>
    /// <param name="r">Red component (0-255)</param>
    /// <param name="g">Green component (0-255)</param>
    /// <param name="b">Blue component (0-255)</param>
    /// <returns>Hex color string in format #RRGGBB</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any color component is outside the valid range (0-255).</exception>
    public static string RGBToHex(int r, int g, int b)
    {
        ValidateColorComponent(r, nameof(r));
        ValidateColorComponent(g, nameof(g));
        ValidateColorComponent(b, nameof(b));
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    /// <summary>
    /// Converts Color values to a Hex string
    /// </summary>
    /// <param name="color">The color to convert</param>
    /// <returns>Hex color string in format #RRGGBB</returns>
    public static string ColorToHexString(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
    
    /// <summary>
    /// Converts Color to an RGB color string.
    /// </summary>
    /// <param name="color">Current color</param>
    /// <returns>RGB color string in format rgb(R, G, B)</returns>
    public static string ColorToRgbString(Color color)
    {
        return $"rgb({color.R}, {color.G}, {color.B})";
    }

    /// <summary>
    /// Converts Color to an ARGB color string.
    /// </summary>
    /// <param name="color">Current color</param>
    /// <returns>ARGB color string in format rgba(R, G, B, A)</returns>
    public static string ColorToARgbString(Color color)
    {
        return $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
    }

    /// <summary>
    /// Validates that a color component is within the valid range (0-255).
    /// </summary>
    private static void ValidateColorComponent(int value, string paramName)
    {
        if (value < 0 || value > 255)
            throw new ArgumentOutOfRangeException(paramName, value, "Color component must be between 0 and 255.");
    }
    #endregion

    #region DateOnly
    /// <summary>
    /// Formats a nullable <see cref="DateOnly"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateOnly(DateOnly? value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateOnly"/> using the specified format and provider.
    /// </summary>
    public static string FromDateOnly(DateOnly value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateTime
    /// <summary>
    /// Formats a nullable <see cref="DateTime"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateTime(DateTime? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateTime"/> using the specified format and provider.
    /// </summary>
    public static string FromDateTime(DateTime value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Formats a nullable <see cref="DateTimeOffset"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateTimeOffset(DateTimeOffset? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateTimeOffset"/> using the specified format and provider.
    /// </summary>
    public static string FromDateTimeOffset(DateTimeOffset value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region TimeOnly
    /// <summary>
    /// Formats a nullable <see cref="TimeOnly"/> using the specified format and provider.
    /// </summary>
    public static string? FromTimeOnly(TimeOnly? value, string format = "HH:mm:ss", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="TimeOnly"/> using the specified format and provider.
    /// </summary>
    public static string FromTimeOnly(TimeOnly value, string format = "HH:mm:ss", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Formats a nullable <see cref="TimeSpan"/> using the specified format and provider.
    /// </summary>
    public static string? FromTimeSpan(TimeSpan? value, string format = "c", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="TimeSpan"/> using the specified format and provider.
    /// </summary>
    public static string FromTimeSpan(TimeSpan value, string format = "c", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region String Validation

    /// <summary>
    /// Determines whether the string contains only numeric digits (0-9).
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the string contains only digits; otherwise, <c>false</c>.</returns>
    public static bool IsNumeric(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return value.All(char.IsDigit);
    }

    /// <summary>
    /// Determines whether the string contains only alphanumeric characters (letters and digits).
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the string contains only letters and digits; otherwise, <c>false</c>.</returns>
    public static bool IsAlphanumeric(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return value.All(char.IsLetterOrDigit);
    }

    #endregion

    #region String Manipulation

    /// <summary>
    /// Truncates the string to the specified maximum length.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix to append when truncated (default: "...").</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(string? value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        if (maxLength < 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength), "Maximum length must be non-negative.");

        if (value.Length <= maxLength)
            return value;

        var truncateLength = maxLength - suffix.Length;
        if (truncateLength < 0)
            truncateLength = 0;

        return value.Substring(0, truncateLength) + suffix;
    }

    /// <summary>
    /// Removes all whitespace characters from the string.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string with all whitespace removed.</returns>
    public static string RemoveWhitespace(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        return new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }

    /// <summary>
    /// Collapses multiple consecutive whitespace characters into a single space.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string with collapsed whitespace.</returns>
    public static string CollapseWhitespace(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        return Regex.Replace(value, @"\s+", " ").Trim();
    }

    /// <summary>
    /// Reverses the characters in the string.
    /// </summary>
    /// <param name="value">The string to reverse.</param>
    /// <returns>The reversed string.</returns>
    public static string Reverse(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var charArray = value.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Repeats the string the specified number of times.
    /// </summary>
    /// <param name="value">The string to repeat.</param>
    /// <param name="count">The number of times to repeat.</param>
    /// <returns>The repeated string.</returns>
    public static string Repeat(string? value, int count)
    {
        if (string.IsNullOrEmpty(value) || count <= 0)
            return string.Empty;

        if (count == 1)
            return value;

        var sb = new StringBuilder(value.Length * count);
        for (int i = 0; i < count; i++)
        {
            sb.Append(value);
        }

        return sb.ToString();
    }

    #endregion

    #region String Comparison

    /// <summary>
    /// Determines whether two strings are equal using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to compare.</param>
    /// <returns><c>true</c> if the strings are equal ignoring case; otherwise, <c>false</c>.</returns>
    public static bool EqualsIgnoreCase(string? source, string? value)
        => string.Equals(source, value, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Determines whether the string contains the specified value using the specified comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to seek.</param>
    /// <param name="comparisonType">The comparison type.</param>
    /// <returns><c>true</c> if the value is found; otherwise, <c>false</c>.</returns>
    public static bool Contains(string? source, string value, StringComparison comparisonType)
    {
        if (source == null)
            return false;

        return source.IndexOf(value, comparisonType) >= 0;
    }

    /// <summary>
    /// Determines whether the string starts with the specified value using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the string starts with the value ignoring case; otherwise, <c>false</c>.</returns>
    public static bool StartsWithIgnoreCase(string? source, string value)
    {
        if (source == null)
            return false;

        return source.StartsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the string ends with the specified value using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the string ends with the value ignoring case; otherwise, <c>false</c>.</returns>
    public static bool EndsWithIgnoreCase(string? source, string value)
    {
        if (source == null)
            return false;

        return source.EndsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Counts the number of occurrences of a substring in the string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The substring to count.</param>
    /// <param name="comparisonType">The comparison type.</param>
    /// <returns>The number of occurrences.</returns>
    public static int CountOccurrences(string? source, string value, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
            return 0;

        int count = 0;
        int index = 0;

        while ((index = source.IndexOf(value, index, comparisonType)) != -1)
        {
            count++;
            index += value.Length;
        }

        return count;
    }

    #endregion

    #region String Replace

    /// <summary>
    /// Replaces the first occurrence of a string with another string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace with.</param>
    /// <returns>The modified string.</returns>
    public static string ReplaceFirst(string? source, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(oldValue))
            return source ?? string.Empty;

        var index = source.IndexOf(oldValue, StringComparison.Ordinal);
        if (index < 0)
            return source;

        return source.Substring(0, index) + newValue + source.Substring(index + oldValue.Length);
    }

    /// <summary>
    /// Replaces the last occurrence of a string with another string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace with.</param>
    /// <returns>The modified string.</returns>
    public static string ReplaceLast(string? source, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(oldValue))
            return source ?? string.Empty;

        var index = source.LastIndexOf(oldValue, StringComparison.Ordinal);
        if (index < 0)
            return source;

        return source.Substring(0, index) + newValue + source.Substring(index + oldValue.Length);
    }

    #endregion

    #region Substring Helpers

    /// <summary>
    /// Returns a specified number of characters from the left side of the string.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The left substring.</returns>
    public static string Left(string? value, int length)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");

        return value.Length <= length ? value : value.Substring(0, length);
    }

    /// <summary>
    /// Returns a specified number of characters from the right side of the string.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The right substring.</returns>
    public static string Right(string? value, int length)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");

        return value.Length <= length ? value : value.Substring(value.Length - length);
    }

    /// <summary>
    /// Returns a substring starting at the specified index with the specified length.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="startIndex">The starting index.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The middle substring.</returns>
    public static string Mid(string? value, int startIndex, int length)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        if (startIndex < 0 || startIndex >= value.Length)
            return string.Empty;

        if (startIndex + length > value.Length)
            length = value.Length - startIndex;

        return value.Substring(startIndex, length);
    }

    #endregion

    #region Case Conversion

    /// <summary>
    /// Converts the string to title case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <param name="culture">The culture to use (default: CurrentCulture).</param>
    /// <returns>The string in title case.</returns>
    public static string ToTitleCase(string? value, CultureInfo? culture = null)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var textInfo = (culture ?? CultureInfo.CurrentCulture).TextInfo;
        return textInfo.ToTitleCase(value.ToLower());
    }

    /// <summary>
    /// Converts the string to PascalCase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in PascalCase.</returns>
    public static string ToPascalCase(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var words = Regex.Split(value, @"[\s_-]+");
        var sb = new StringBuilder();

        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                sb.Append(char.ToUpperInvariant(word[0]));
                if (word.Length > 1)
                    sb.Append(word.Substring(1).ToLowerInvariant());
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Converts the string to camelCase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in camelCase.</returns>
    public static string ToCamelCase(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var pascal = ToPascalCase(value);
        if (pascal.Length == 0)
            return pascal;

        return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
    }

    /// <summary>
    /// Converts the string to kebab-case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in kebab-case.</returns>
    public static string ToKebabCase(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var result = Regex.Replace(value, "([a-z])([A-Z])", "$1-$2");
        result = Regex.Replace(result, @"[\s_]+", "-");
        return result.ToLowerInvariant();
    }

    /// <summary>
    /// Converts the string to snake_case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in snake_case.</returns>
    public static string ToSnakeCase(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value ?? string.Empty;

        var result = Regex.Replace(value, "([a-z])([A-Z])", "$1_$2");
        result = Regex.Replace(result, @"[\s-]+", "_");
        return result.ToLowerInvariant();
    }

    #endregion

    #region Encoding

    /// <summary>
    /// Encodes the string to Base64 format using UTF-8 encoding.
    /// </summary>
    /// <param name="value">The string to encode.</param>
    /// <returns>The Base64 encoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    public static string ToBase64(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a Base64 encoded string using UTF-8 encoding.
    /// </summary>
    /// <param name="base64">The Base64 encoded string.</param>
    /// <returns>The decoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when base64 is null.</exception>
    /// <exception cref="FormatException">Thrown when base64 is not a valid Base64 string.</exception>
    public static string FromBase64(string base64)
    {
        ArgumentNullException.ThrowIfNull(base64);

        var bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }

    #endregion

    #region String Prefix/Suffix

    /// <summary>
    /// Ensures the string ends with the specified suffix.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="suffix">The suffix to ensure.</param>
    /// <returns>The string with the suffix.</returns>
    public static string EnsureEndsWith(string? value, string suffix)
    {
        if (string.IsNullOrEmpty(value))
            return suffix;

        return value.EndsWith(suffix) ? value : value + suffix;
    }

    /// <summary>
    /// Ensures the string starts with the specified prefix.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="prefix">The prefix to ensure.</param>
    /// <returns>The string with the prefix.</returns>
    public static string EnsureStartsWith(string? value, string prefix)
    {
        if (string.IsNullOrEmpty(value))
            return prefix;

        return value.StartsWith(prefix) ? value : prefix + value;
    }

    #endregion
}
