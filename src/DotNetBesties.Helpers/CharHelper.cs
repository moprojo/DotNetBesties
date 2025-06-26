using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="char"/> results.
/// </summary>
public static class CharHelper
{
    #region Char
    public static char ToLower(char value, CultureInfo? culture = null)
        => char.ToLower(value, culture ?? CultureInfo.InvariantCulture);

    public static char ToUpper(char value, CultureInfo? culture = null)
        => char.ToUpper(value, culture ?? CultureInfo.InvariantCulture);
    #endregion

    #region String
    public static char GetFirstOrDefault(string? input, char defaultChar = '\0')
        => string.IsNullOrEmpty(input) ? defaultChar : input![0];
    #endregion
}
