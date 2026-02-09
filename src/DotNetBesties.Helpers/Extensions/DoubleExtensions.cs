using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="double"/> operations.
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    /// Rounds the double to the specified number of decimal places.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimals">The number of decimal places.</param>
    /// <returns>The rounded value.</returns>
    public static double Round(this double value, int decimals = 0)
        => DoubleHelper.Round(value, decimals);

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static double Clamp(this double value, double min, double max)
        => DoubleHelper.Clamp(value, min, max);

    /// <summary>
    /// Determines whether the value is approximately equal to another value within a tolerance.
    /// </summary>
    /// <param name="value">The first value.</param>
    /// <param name="other">The second value.</param>
    /// <param name="tolerance">The tolerance for comparison. Default is 1e-9.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    public static bool IsApproximately(this double value, double other, double tolerance = 1e-9)
        => DoubleHelper.IsApproximately(value, other, tolerance);

    /// <summary>
    /// Gets the absolute value of the double.
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>The absolute value.</returns>
    public static double Abs(this double value)
        => DoubleHelper.Abs(value);

    /// <summary>
    /// Returns the sign of the double (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(this double value)
        => DoubleHelper.Sign(value);
}
