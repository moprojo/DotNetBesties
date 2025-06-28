using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

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
