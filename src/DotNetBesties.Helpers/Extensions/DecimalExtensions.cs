using System;
using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="decimal"/> operations.
/// </summary>
public static class DecimalExtensions
{
    #region Mathematical Operations

    /// <summary>
    /// Computes the absolute value of this <see cref="decimal"/> number.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>The absolute value.</returns>
    public static decimal Abs(this decimal value)
        => DecimalHelper.Abs(value);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to this decimal number.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>The ceiling value.</returns>
    public static decimal Ceiling(this decimal value)
        => DecimalHelper.Ceiling(value);

    /// <summary>
    /// Returns the largest integral value less than or equal to this decimal number.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>The floor value.</returns>
    public static decimal Floor(this decimal value)
        => DecimalHelper.Floor(value);

    /// <summary>
    /// Rounds this decimal value to the designated number of fractional digits, using the specified rounding mode.
    /// Default mode is <see cref="MidpointRounding.ToEven"/>.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <param name="decimals">The number of fractional digits in the return value.</param>
    /// <param name="mode">The rounding strategy.</param>
    /// <returns>The rounded value.</returns>
    public static decimal Round(this decimal value, int decimals = 0, MidpointRounding mode = MidpointRounding.ToEven)
        => DecimalHelper.Round(value, decimals, mode);

    /// <summary>
    /// Calculates the integral part of this decimal number.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>The truncated value.</returns>
    public static decimal Truncate(this decimal value)
        => DecimalHelper.Truncate(value);

    #endregion
}
