using DotNetBesties.Helpers.Format;
using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/> types.
/// </summary>
public static class StringExtensions
{
    #region Null/Empty Checks

    /// <summary>
    /// Indicates whether the specified string is null or empty.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrEmpty(this string? value)
        => StringHelper.IsNullOrEmpty(value);

    /// <summary>
    /// Indicates whether the specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrWhiteSpace(this string? value)
        => StringHelper.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Indicates whether the specified string is not null and not empty.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the value is not null and not empty; otherwise, <c>false</c>.</returns>
    public static bool HasValue(this string? value)
        => StringHelper.HasValue(value);

    /// <summary>
    /// Returns the string if it has a value, otherwise returns the default value.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="defaultValue">The default value to return.</param>
    /// <returns>The original string or the default value.</returns>
    public static string DefaultIfEmpty(this string? value, string defaultValue)
        => StringHelper.DefaultIfEmpty(value, defaultValue);

    #endregion

    #region Validation

    /// <summary>
    /// Determines whether the string contains only numeric digits (0-9).
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the string contains only digits; otherwise, <c>false</c>.</returns>
    public static bool IsNumeric(this string? value)
        => StringHelper.IsNumeric(value);

    /// <summary>
    /// Determines whether the string contains only alphanumeric characters (letters and digits).
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><c>true</c> if the string contains only letters and digits; otherwise, <c>false</c>.</returns>
    public static bool IsAlphanumeric(this string? value)
        => StringHelper.IsAlphanumeric(value);

    #endregion

    #region Truncate

    /// <summary>
    /// Truncates the string to the specified maximum length.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix to append when truncated (default: "...").</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string? value, int maxLength, string suffix = "...")
        => StringHelper.Truncate(value, maxLength, suffix);

    #endregion

    #region Contains/Comparison

    /// <summary>
    /// Determines whether the string contains the specified value using the specified comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to seek.</param>
    /// <param name="comparisonType">The comparison type.</param>
    /// <returns><c>true</c> if the value is found; otherwise, <c>false</c>.</returns>
    public static bool Contains(this string? source, string value, StringComparison comparisonType)
        => StringHelper.Contains(source, value, comparisonType);

    /// <summary>
    /// Determines whether two strings are equal using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to compare.</param>
    /// <returns><c>true</c> if the strings are equal ignoring case; otherwise, <c>false</c>.</returns>
    public static bool EqualsIgnoreCase(this string? source, string? value)
        => StringHelper.EqualsIgnoreCase(source, value);

    /// <summary>
    /// Determines whether the string starts with the specified value using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the string starts with the value ignoring case; otherwise, <c>false</c>.</returns>
    public static bool StartsWithIgnoreCase(this string? source, string value)
        => StringHelper.StartsWithIgnoreCase(source, value);

    /// <summary>
    /// Determines whether the string ends with the specified value using case-insensitive comparison.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if the string ends with the value ignoring case; otherwise, <c>false</c>.</returns>
    public static bool EndsWithIgnoreCase(this string? source, string value)
        => StringHelper.EndsWithIgnoreCase(source, value);

    #endregion

    #region Whitespace

    /// <summary>
    /// Removes all whitespace characters from the string.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string with all whitespace removed.</returns>
    public static string RemoveWhitespace(this string? value)
        => StringHelper.RemoveWhitespace(value);

    /// <summary>
    /// Collapses multiple consecutive whitespace characters into a single space.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string with collapsed whitespace.</returns>
    public static string CollapseWhitespace(this string? value)
        => StringHelper.CollapseWhitespace(value);

    #endregion

    #region Case Conversion

    /// <summary>
    /// Converts the string to title case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <param name="culture">The culture to use (default: CurrentCulture).</param>
    /// <returns>The string in title case.</returns>
    public static string ToTitleCase(this string? value, CultureInfo? culture = null)
        => StringHelper.ToTitleCase(value, culture);

    /// <summary>
    /// Converts the string to PascalCase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in PascalCase.</returns>
    public static string ToPascalCase(this string? value)
        => StringHelper.ToPascalCase(value);

    /// <summary>
    /// Converts the string to camelCase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in camelCase.</returns>
    public static string ToCamelCase(this string? value)
        => StringHelper.ToCamelCase(value);

    /// <summary>
    /// Converts the string to kebab-case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in kebab-case.</returns>
    public static string ToKebabCase(this string? value)
        => StringHelper.ToKebabCase(value);

    /// <summary>
    /// Converts the string to snake_case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in snake_case.</returns>
    public static string ToSnakeCase(this string? value)
        => StringHelper.ToSnakeCase(value);

    #endregion

    #region Encoding

    /// <summary>
    /// Encodes the string to Base64 format using UTF-8 encoding.
    /// </summary>
    /// <param name="value">The string to encode.</param>
    /// <returns>The Base64 encoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    public static string ToBase64(this string value)
        => StringHelper.ToBase64(value);

    /// <summary>
    /// Decodes a Base64 encoded string using UTF-8 encoding.
    /// </summary>
    /// <param name="base64">The Base64 encoded string.</param>
    /// <returns>The decoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when base64 is null.</exception>
    /// <exception cref="FormatException">Thrown when base64 is not a valid Base64 string.</exception>
    public static string FromBase64(this string base64)
        => StringHelper.FromBase64(base64);

    #endregion

    #region Replace

    /// <summary>
    /// Replaces the first occurrence of a string with another string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace with.</param>
    /// <returns>The modified string.</returns>
    public static string ReplaceFirst(this string? source, string oldValue, string newValue)
        => StringHelper.ReplaceFirst(source, oldValue, newValue);

    /// <summary>
    /// Replaces the last occurrence of a string with another string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace with.</param>
    /// <returns>The modified string.</returns>
    public static string ReplaceLast(this string? source, string oldValue, string newValue)
        => StringHelper.ReplaceLast(source, oldValue, newValue);

    #endregion

    #region Substring Helpers

    /// <summary>
    /// Returns a specified number of characters from the left side of the string.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The left substring.</returns>
    public static string Left(this string? value, int length)
        => StringHelper.Left(value, length);

    /// <summary>
    /// Returns a specified number of characters from the right side of the string.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The right substring.</returns>
    public static string Right(this string? value, int length)
        => StringHelper.Right(value, length);

    /// <summary>
    /// Returns a substring starting at the specified index with the specified length.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="startIndex">The starting index.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns>The middle substring.</returns>
    public static string Mid(this string? value, int startIndex, int length)
        => StringHelper.Mid(value, startIndex, length);

    #endregion

    #region DateOnly Parsing

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateOnly"/> using the specified format and invariant culture.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="format">The required format.</param>
    /// <param name="result">The parsed DateOnly value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateOnlyInvariant(this string? input, string format, out DateOnly result)
        => BoolHelper.TryParseExactDateOnlyInvariant(input, format, out result);

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateOnly"/> using any of the provided formats and invariant culture.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="formats">The acceptable formats.</param>
    /// <param name="result">The parsed DateOnly value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateOnlyInvariant(this string? input, string[] formats, out DateOnly result)
        => BoolHelper.TryParseExactDateOnlyInvariant(input, formats, out result);

    #endregion

    #region DateTime Parsing

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateTime"/> using the specified format, invariant culture and styles.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="format">The required format.</param>
    /// <param name="result">The parsed DateTime value if successful.</param>
    /// <param name="styles">The DateTimeStyles to use. Default is None.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateTimeInvariant(this string? input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => BoolHelper.TryParseExactDateTimeInvariant(input, format, out result, styles);

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateTime"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="formats">The acceptable formats.</param>
    /// <param name="result">The parsed DateTime value if successful.</param>
    /// <param name="styles">The DateTimeStyles to use. Default is None.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateTimeInvariant(this string? input, string[] formats, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => BoolHelper.TryParseExactDateTimeInvariant(input, formats, out result, styles);

    #endregion

    #region DateTimeOffset Parsing

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateTimeOffset"/> using the specified format, invariant culture and styles.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="format">The required format.</param>
    /// <param name="result">The parsed DateTimeOffset value if successful.</param>
    /// <param name="styles">The DateTimeStyles to use. Default is None.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateTimeOffsetInvariant(this string? input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => BoolHelper.TryParseExactDateTimeOffsetInvariant(input, format, out result, styles);

    /// <summary>
    /// Attempts to parse the string to a <see cref="DateTimeOffset"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="formats">The acceptable formats.</param>
    /// <param name="result">The parsed DateTimeOffset value if successful.</param>
    /// <param name="styles">The DateTimeStyles to use. Default is None.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseDateTimeOffsetInvariant(this string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => BoolHelper.TryParseExactDateTimeOffsetInvariant(input, formats, out result, styles);

    #endregion

    #region TimeSpan Parsing

    /// <summary>
    /// Attempts to parse the string to a <see cref="TimeSpan"/> using the specified format and invariant culture.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="format">The required format.</param>
    /// <param name="result">The parsed TimeSpan value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseTimeSpanInvariant(this string? input, string format, out TimeSpan result)
        => BoolHelper.TryParseExactTimeSpanInvariant(input, format, out result);

    /// <summary>
    /// Attempts to parse the string to a <see cref="TimeSpan"/> using any of the provided formats and invariant culture.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="formats">The acceptable formats.</param>
    /// <param name="result">The parsed TimeSpan value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseTimeSpanInvariant(this string? input, string[] formats, out TimeSpan result)
        => BoolHelper.TryParseExactTimeSpanInvariant(input, formats, out result);

    #endregion

    #region Long Parsing

    /// <summary>
    /// Attempts to parse the string to a <see cref="long"/> using invariant culture.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="result">The parsed long value if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParseLongInvariant(this string? input, out long result)
        => BoolHelper.TryParseLongInvariant(input, out result);

    #endregion

    #region Other

    /// <summary>
    /// Reverses the characters in the string.
    /// </summary>
    /// <param name="value">The string to reverse.</param>
    /// <returns>The reversed string.</returns>
    public static string Reverse(this string? value)
        => StringHelper.Reverse(value);

    /// <summary>
    /// Counts the number of occurrences of a substring in the string.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <param name="value">The substring to count.</param>
    /// <param name="comparisonType">The comparison type.</param>
    /// <returns>The number of occurrences.</returns>
    public static int CountOccurrences(this string? source, string value, StringComparison comparisonType = StringComparison.Ordinal)
        => StringHelper.CountOccurrences(source, value, comparisonType);

    /// <summary>
    /// Repeats the string the specified number of times.
    /// </summary>
    /// <param name="value">The string to repeat.</param>
    /// <param name="count">The number of times to repeat.</param>
    /// <returns>The repeated string.</returns>
    public static string Repeat(this string? value, int count)
        => StringHelper.Repeat(value, count);

    /// <summary>
    /// Ensures the string ends with the specified suffix.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="suffix">The suffix to ensure.</param>
    /// <returns>The string with the suffix.</returns>
    public static string EnsureEndsWith(this string? value, string suffix)
        => StringHelper.EnsureEndsWith(value, suffix);

    /// <summary>
    /// Ensures the string starts with the specified prefix.
    /// </summary>
    /// <param name="value">The source string.</param>
    /// <param name="prefix">The prefix to ensure.</param>
    /// <returns>The string with the prefix.</returns>
    public static string EnsureStartsWith(this string? value, string prefix)
        => StringHelper.EnsureStartsWith(value, prefix);

    #endregion
}
