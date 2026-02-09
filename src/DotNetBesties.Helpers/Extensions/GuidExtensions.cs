namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="Guid"/> operations.
/// </summary>
public static class GuidExtensions
{
    #region Queries

    /// <summary>
    /// Determines whether the Guid is empty (all zeros).
    /// </summary>
    /// <param name="value">The Guid to check.</param>
    /// <returns>True if the Guid is empty; otherwise, false.</returns>
    public static bool IsEmpty(this Guid value)
        => Format.GuidHelper.IsEmpty(value);

    /// <summary>
    /// Determines whether the nullable Guid is null or empty.
    /// </summary>
    /// <param name="value">The nullable Guid to check.</param>
    /// <returns>True if the Guid is null or empty; otherwise, false.</returns>
    public static bool IsNullOrEmpty(this Guid? value)
        => Format.GuidHelper.IsNullOrEmpty(value);

    #endregion

    #region Formatting

    /// <summary>
    /// Converts the Guid to a string using the specified format.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <param name="format">The format specifier: "N", "D", "B", "P", or "X".</param>
    /// <returns>The formatted Guid string.</returns>
    public static string ToString(this Guid value, char format)
        => Format.GuidHelper.ToString(value, format);

    /// <summary>
    /// Converts the Guid to a string without hyphens (format "N").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a 32-character hex string without hyphens.</returns>
    public static string ToStringN(this Guid value)
        => Format.GuidHelper.ToStringN(value);

    /// <summary>
    /// Converts the Guid to a string with hyphens (format "D"). This is the default format.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a 36-character string with hyphens.</returns>
    public static string ToStringD(this Guid value)
        => Format.GuidHelper.ToStringD(value);

    /// <summary>
    /// Converts the Guid to a string enclosed in braces (format "B").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a string enclosed in braces.</returns>
    public static string ToStringB(this Guid value)
        => Format.GuidHelper.ToStringB(value);

    /// <summary>
    /// Converts the Guid to a string enclosed in parentheses (format "P").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a string enclosed in parentheses.</returns>
    public static string ToStringP(this Guid value)
        => Format.GuidHelper.ToStringP(value);

    /// <summary>
    /// Converts the Guid to a hexadecimal array format (format "X").
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>The Guid as a hexadecimal array string.</returns>
    public static string ToStringX(this Guid value)
        => Format.GuidHelper.ToStringX(value);

    #endregion

    #region Conversion

    /// <summary>
    /// Converts the Guid to a Base64 string (shorter than hex representation).
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A Base64 string representation of the Guid.</returns>
    public static string ToBase64String(this Guid value)
        => Format.GuidHelper.ToBase64String(value);

    /// <summary>
    /// Converts the Guid to a URL-safe Base64 string (replaces + with - and / with _).
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A URL-safe Base64 string representation.</returns>
    public static string ToBase64UrlString(this Guid value)
        => Format.GuidHelper.ToBase64UrlString(value);

    #endregion

    #region Short Guid

    /// <summary>
    /// Converts the Guid to a short 22-character string representation.
    /// Uses Base64 encoding without padding and with URL-safe characters.
    /// </summary>
    /// <param name="value">The Guid to convert.</param>
    /// <returns>A 22-character string representation.</returns>
    public static string ToShortString(this Guid value)
        => Format.GuidHelper.ToShortString(value);

    #endregion

    #region Comparison

    /// <summary>
    /// Determines whether the Guid matches any of the specified Guids.
    /// </summary>
    /// <param name="value">The Guid to check.</param>
    /// <param name="guids">The Guids to compare against.</param>
    /// <returns>True if the Guid matches any of the specified Guids; otherwise, false.</returns>
    public static bool IsAnyOf(this Guid value, params Guid[] guids)
        => Format.GuidHelper.IsAnyOf(value, guids);

    #endregion
}
