using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="char"/> results.
/// </summary>
public static class CharHelper
{
    #region Char
    /// <summary>
    /// Converts the character to lower case using the provided culture or <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static char ToLower(char value, CultureInfo? culture = null)
        => char.ToLower(value, culture ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts the character to upper case using the provided culture or <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static char ToUpper(char value, CultureInfo? culture = null)
        => char.ToUpper(value, culture ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Determines whether the character is a vowel (a, e, i, o, u - case insensitive).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <returns><c>true</c> if the character is a vowel; otherwise, <c>false</c>.</returns>
    public static bool IsVowel(char value)
    {
        var lower = char.ToLowerInvariant(value);
        return lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u';
    }

    /// <summary>
    /// Determines whether the character is a consonant (a letter that is not a vowel).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <returns><c>true</c> if the character is a consonant; otherwise, <c>false</c>.</returns>
    public static bool IsConsonant(char value)
        => char.IsLetter(value) && !IsVowel(value);

    /// <summary>
    /// Repeats the character the specified number of times.
    /// </summary>
    /// <param name="value">The character to repeat.</param>
    /// <param name="count">The number of times to repeat the character.</param>
    /// <returns>A string containing the repeated character.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when count is negative.</exception>
    public static string Repeat(char value, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        
        return new string(value, count);
    }

    /// <summary>
    /// Determines whether the character is in the specified range (inclusive).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <param name="start">The start of the range (inclusive).</param>
    /// <param name="end">The end of the range (inclusive).</param>
    /// <returns><c>true</c> if the character is within the range; otherwise, <c>false</c>.</returns>
    public static bool IsInRange(char value, char start, char end)
        => value >= start && value <= end;

    /// <summary>
    /// Determines whether the character is any of the specified characters.
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <param name="chars">The characters to compare against.</param>
    /// <returns><c>true</c> if the character matches any of the specified characters; otherwise, <c>false</c>.</returns>
    public static bool IsAnyOf(char value, params char[] chars)
    {
        ArgumentNullException.ThrowIfNull(chars);
        return chars.Contains(value);
    }
    #endregion

    #region String
    /// <summary>
    /// Returns the first character of the string or the specified default if the string is null or empty.
    /// </summary>
    public static char GetFirstOrDefault(string? input, char defaultChar = '\0')
        => string.IsNullOrEmpty(input) ? defaultChar : input![0];
    /// <summary>
    /// Returns the last character of the string or the specified default if the string is null or empty.
    /// </summary>
    public static char GetLastOrDefault(string? input, char defaultChar = '\0')
        => string.IsNullOrEmpty(input) ? defaultChar : input![input.Length - 1];
    /// <summary>
    /// Returns the character at the specified index or the specified default if the index is out of bounds.
    /// </summary>
    public static char GetAtOrDefault(string? input, int index, char defaultChar = '\0')
        => input != null && index >= 0 && index < input.Length ? input[index] : defaultChar;
    #endregion
}
