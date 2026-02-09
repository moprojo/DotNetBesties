using System.Globalization;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="char"/> operations.
/// </summary>
public static class CharExtensions
{
    #region Case Conversion

    /// <summary>
    /// Converts the character to lower case using the provided culture or <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    /// <param name="value">The character to convert.</param>
    /// <param name="culture">The culture to use for conversion. If null, <see cref="CultureInfo.InvariantCulture"/> is used.</param>
    /// <returns>The lowercase version of the character.</returns>
    public static char ToLowerInvariant(this char value, CultureInfo? culture = null)
        => Format.CharHelper.ToLower(value, culture);

    /// <summary>
    /// Converts the character to upper case using the provided culture or <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    /// <param name="value">The character to convert.</param>
    /// <param name="culture">The culture to use for conversion. If null, <see cref="CultureInfo.InvariantCulture"/> is used.</param>
    /// <returns>The uppercase version of the character.</returns>
    public static char ToUpperInvariant(this char value, CultureInfo? culture = null)
        => Format.CharHelper.ToUpper(value, culture);

    #endregion

    #region Character Classification

    /// <summary>
    /// Determines whether the character is a vowel (a, e, i, o, u - case insensitive).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <returns><c>true</c> if the character is a vowel; otherwise, <c>false</c>.</returns>
    public static bool IsVowel(this char value)
        => Format.CharHelper.IsVowel(value);

    /// <summary>
    /// Determines whether the character is a consonant (a letter that is not a vowel).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <returns><c>true</c> if the character is a consonant; otherwise, <c>false</c>.</returns>
    public static bool IsConsonant(this char value)
        => Format.CharHelper.IsConsonant(value);

    #endregion

    #region Repetition

    /// <summary>
    /// Repeats the character the specified number of times.
    /// </summary>
    /// <param name="value">The character to repeat.</param>
    /// <param name="count">The number of times to repeat the character.</param>
    /// <returns>A string containing the repeated character.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when count is negative.</exception>
    public static string Repeat(this char value, int count)
        => Format.CharHelper.Repeat(value, count);

    #endregion

    #region Comparison

    /// <summary>
    /// Determines whether the character is in the specified range (inclusive).
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <param name="start">The start of the range (inclusive).</param>
    /// <param name="end">The end of the range (inclusive).</param>
    /// <returns><c>true</c> if the character is within the range; otherwise, <c>false</c>.</returns>
    public static bool IsInRange(this char value, char start, char end)
        => Format.CharHelper.IsInRange(value, start, end);

    /// <summary>
    /// Determines whether the character is any of the specified characters.
    /// </summary>
    /// <param name="value">The character to check.</param>
    /// <param name="chars">The characters to compare against.</param>
    /// <returns><c>true</c> if the character matches any of the specified characters; otherwise, <c>false</c>.</returns>
    public static bool IsAnyOf(this char value, params char[] chars)
        => Format.CharHelper.IsAnyOf(value, chars);

    #endregion
}
