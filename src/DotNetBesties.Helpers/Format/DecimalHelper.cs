using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="decimal"/> values.
/// </summary>
public static class DecimalHelper
{
    #region Decimal

    /// <summary>
    /// Computes the absolute value of a specified <see cref="decimal"/> number.
    /// </summary>
    public static decimal Abs(decimal value)
        => Math.Abs(value);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified decimal number.
    /// </summary>
    public static decimal Ceiling(decimal value)
        => Math.Ceiling(value);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified decimal number.
    /// </summary>
    public static decimal Floor(decimal value)
        => Math.Floor(value);

    /// <summary>
    /// Parses the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static decimal ParseInvariant(string input)
        => decimal.Parse(input, CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>. 
    /// Returns <c>null</c> if parsing fails.
    /// </summary>
    public static decimal? ParseInvariantOrNull(string? input)
        => decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>
    /// Rounds a decimal value to the designated number of fractional digits, using the specified rounding mode.
    /// Default mode is <see cref="MidpointRounding.ToEven"/>.
    /// </summary>
    public static decimal Round(decimal value, int decimals = 0, MidpointRounding mode = MidpointRounding.ToEven)
        => Math.Round(value, decimals, mode);

    /// <summary>
    /// Calculates the integral part of a specified decimal number.
    /// </summary>
    public static decimal Truncate(decimal value)
        => Math.Truncate(value);

    #endregion
}
