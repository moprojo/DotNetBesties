using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="float"/> operations.
/// </summary>
public static class FloatExtensions
{
    /// <summary>
    /// Rounds the float to the specified number of decimal places.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimals">The number of decimal places.</param>
    /// <returns>The rounded value.</returns>
    public static float Round(this float value, int decimals = 0)
        => FloatHelper.Round(value, decimals);

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static float Clamp(this float value, float min, float max)
        => FloatHelper.Clamp(value, min, max);

    /// <summary>
    /// Determines whether the value is approximately equal to another value within a tolerance.
    /// </summary>
    /// <param name="value">The first value.</param>
    /// <param name="other">The second value.</param>
    /// <param name="tolerance">The tolerance for comparison. Default is 0.0001f.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    public static bool IsApproximately(this float value, float other, float tolerance = 0.0001f)
        => FloatHelper.IsApproximately(value, other, tolerance);

    /// <summary>
    /// Gets the absolute value of the float.
    /// </summary>
    /// <param name="value">The float value.</param>
    /// <returns>The absolute value.</returns>
    public static float Abs(this float value)
        => FloatHelper.Abs(value);

    /// <summary>
    /// Returns the sign of the float (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The float value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(this float value)
        => FloatHelper.Sign(value);
}
