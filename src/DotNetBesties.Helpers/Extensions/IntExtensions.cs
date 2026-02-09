using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="int"/> operations.
/// </summary>
public static class IntExtensions
{
    /// <summary>
    /// Determines whether the integer is even.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is even; otherwise, <c>false</c>.</returns>
    public static bool IsEven(this int value)
        => IntHelper.IsEven(value);

    /// <summary>
    /// Determines whether the integer is odd.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is odd; otherwise, <c>false</c>.</returns>
    public static bool IsOdd(this int value)
        => IntHelper.IsOdd(value);

    /// <summary>
    /// Determines whether the integer is a prime number.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is prime; otherwise, <c>false</c>.</returns>
    public static bool IsPrime(this int value)
        => IntHelper.IsPrime(value);

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static int Clamp(this int value, int min, int max)
        => IntHelper.Clamp(value, min, max);

    /// <summary>
    /// Gets the absolute value of the integer.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>The absolute value.</returns>
    public static int Abs(this int value)
        => IntHelper.Abs(value);

    /// <summary>
    /// Returns the sign of the integer (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(this int value)
        => IntHelper.Sign(value);
}
