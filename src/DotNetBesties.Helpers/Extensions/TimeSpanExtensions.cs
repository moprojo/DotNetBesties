using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="TimeSpan"/> operations.
/// </summary>
public static class TimeSpanExtensions
{
    #region Arithmetic

    /// <summary>
    /// Multiplies the TimeSpan by the specified factor.
    /// </summary>
    /// <param name="value">The TimeSpan to multiply.</param>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>The multiplied TimeSpan.</returns>
    public static TimeSpan Multiply(this TimeSpan value, double factor)
        => Format.TimeSpanHelper.Multiply(value, factor);

    /// <summary>
    /// Divides the TimeSpan by the specified divisor.
    /// </summary>
    /// <param name="value">The TimeSpan to divide.</param>
    /// <param name="divisor">The division divisor.</param>
    /// <returns>The divided TimeSpan.</returns>
    public static TimeSpan Divide(this TimeSpan value, double divisor)
        => Format.TimeSpanHelper.Divide(value, divisor);

    #endregion

    #region Conversion

    /// <summary>
    /// Gets the absolute value (duration) of the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The absolute TimeSpan.</returns>
    public static TimeSpan Abs(this TimeSpan value)
        => Format.TimeSpanHelper.Duration(value);

    /// <summary>
    /// Gets the total number of days represented by the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The total days as a double.</returns>
    public static double ToTotalDays(this TimeSpan value)
        => Format.DoubleHelper.TotalDays(value);

    /// <summary>
    /// Gets the total number of days represented by the nullable TimeSpan.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <returns>The total days as a double or null.</returns>
    public static double? ToTotalDays(this TimeSpan? value)
        => Format.DoubleHelper.TotalDays(value);

    /// <summary>
    /// Gets the total number of hours represented by the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The total hours as a double.</returns>
    public static double ToTotalHours(this TimeSpan value)
        => Format.DoubleHelper.TotalHours(value);

    /// <summary>
    /// Gets the total number of hours represented by the nullable TimeSpan.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <returns>The total hours as a double or null.</returns>
    public static double? ToTotalHours(this TimeSpan? value)
        => Format.DoubleHelper.TotalHours(value);

    /// <summary>
    /// Gets the total number of minutes represented by the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The total minutes as a double.</returns>
    public static double ToTotalMinutes(this TimeSpan value)
        => Format.DoubleHelper.TotalMinutes(value);

    /// <summary>
    /// Gets the total number of minutes represented by the nullable TimeSpan.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <returns>The total minutes as a double or null.</returns>
    public static double? ToTotalMinutes(this TimeSpan? value)
        => Format.DoubleHelper.TotalMinutes(value);

    /// <summary>
    /// Gets the total number of seconds represented by the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The total seconds as a double.</returns>
    public static double ToTotalSeconds(this TimeSpan value)
        => Format.DoubleHelper.TotalSeconds(value);

    /// <summary>
    /// Gets the total number of seconds represented by the nullable TimeSpan.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <returns>The total seconds as a double or null.</returns>
    public static double? ToTotalSeconds(this TimeSpan? value)
        => Format.DoubleHelper.TotalSeconds(value);

    /// <summary>
    /// Gets the total number of milliseconds represented by the TimeSpan.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>The total milliseconds as a double.</returns>
    public static double ToTotalMilliseconds(this TimeSpan value)
        => Format.DoubleHelper.TotalMilliseconds(value);

    /// <summary>
    /// Gets the total number of milliseconds represented by the nullable TimeSpan.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <returns>The total milliseconds as a double or null.</returns>
    public static double? ToTotalMilliseconds(this TimeSpan? value)
        => Format.DoubleHelper.TotalMilliseconds(value);

    #endregion

    #region String Formatting

    /// <summary>
    /// Formats the TimeSpan using the specified format and provider.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <param name="format">The format string. Default is "c" (constant format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation.</returns>
    public static string ToInvariantString(this TimeSpan value, string format = "c", IFormatProvider? provider = null)
        => Format.StringHelper.FromTimeSpan(value, format, provider);

    /// <summary>
    /// Formats the nullable TimeSpan using the specified format and provider.
    /// </summary>
    /// <param name="value">The nullable TimeSpan value.</param>
    /// <param name="format">The format string. Default is "c" (constant format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation or null.</returns>
    public static string? ToInvariantString(this TimeSpan? value, string format = "c", IFormatProvider? provider = null)
        => Format.StringHelper.FromTimeSpan(value, format, provider);

    #endregion

    #region Queries

    /// <summary>
    /// Determines whether the TimeSpan is zero.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is zero; otherwise, false.</returns>
    public static bool IsZero(this TimeSpan value)
        => Format.TimeSpanHelper.IsZero(value);

    /// <summary>
    /// Determines whether the TimeSpan is positive.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is positive; otherwise, false.</returns>
    public static bool IsPositive(this TimeSpan value)
        => Format.TimeSpanHelper.IsPositive(value);

    /// <summary>
    /// Determines whether the TimeSpan is negative.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is negative; otherwise, false.</returns>
    public static bool IsNegative(this TimeSpan value)
        => Format.TimeSpanHelper.IsNegative(value);

    #endregion

    #region Formatting

    /// <summary>
    /// Converts the TimeSpan to a human-readable string (e.g., "2 days, 3 hours").
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <param name="precision">The number of components to include (default is 2).</param>
    /// <returns>A human-readable string representation.</returns>
    public static string ToHumanReadable(this TimeSpan value, int precision = 2)
        => Format.TimeSpanHelper.ToHumanReadable(value, precision);

    /// <summary>
    /// Converts the TimeSpan to a compact string (e.g., "2d 3h 15m").
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>A compact string representation.</returns>
    public static string ToCompactString(this TimeSpan value)
        => Format.TimeSpanHelper.ToCompactString(value);

    #endregion

    #region Rounding

    /// <summary>
    /// Rounds the TimeSpan to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded TimeSpan.</returns>
    public static TimeSpan Round(this TimeSpan value, TimeSpan interval)
        => Format.TimeSpanHelper.Round(value, interval);

    /// <summary>
    /// Rounds the TimeSpan up to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round up.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded up TimeSpan.</returns>
    public static TimeSpan Ceiling(this TimeSpan value, TimeSpan interval)
        => Format.TimeSpanHelper.Ceiling(value, interval);

    /// <summary>
    /// Rounds the TimeSpan down to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round down.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded down TimeSpan.</returns>
    public static TimeSpan Floor(this TimeSpan value, TimeSpan interval)
        => Format.TimeSpanHelper.Floor(value, interval);

    #endregion
}
