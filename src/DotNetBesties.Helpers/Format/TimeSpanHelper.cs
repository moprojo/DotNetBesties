using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="TimeSpan"/> values.
/// </summary>
public static class TimeSpanHelper
{
    #region TimeSpan

    /// <summary>
    /// Adds two <see cref="TimeSpan"/> values.
    /// </summary>
    public static TimeSpan Add(TimeSpan left, TimeSpan right)
        => left + right;

    /// <summary>
    /// Divides the duration by the specified divisor.
    /// </summary>
    public static TimeSpan Divide(TimeSpan value, double divisor)
        => TimeSpan.FromTicks((long)(value.Ticks / divisor));

    /// <summary>
    /// Returns the absolute value of the <see cref="TimeSpan"/>.
    /// </summary>
    public static TimeSpan Duration(TimeSpan value)
        => value.Duration();

    /// <summary>
    /// Creates a <see cref="TimeSpan"/> representing the specified number of days.
    /// </summary>
    public static TimeSpan FromDays(double days)
        => TimeSpan.FromDays(days);

    /// <summary>
    /// Creates a <see cref="TimeSpan"/> representing the specified number of hours.
    /// </summary>
    public static TimeSpan FromHours(double hours)
        => TimeSpan.FromHours(hours);

    /// <summary>
    /// Creates a <see cref="TimeSpan"/> representing the specified number of milliseconds.
    /// </summary>
    public static TimeSpan FromMilliseconds(double milliseconds)
        => TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Creates a <see cref="TimeSpan"/> representing the specified number of minutes.
    /// </summary>
    public static TimeSpan FromMinutes(double minutes)
        => TimeSpan.FromMinutes(minutes);

    /// <summary>
    /// Creates a <see cref="TimeSpan"/> representing the specified number of seconds.
    /// </summary>
    public static TimeSpan FromSeconds(double seconds)
        => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Multiplies the duration by the specified factor.
    /// </summary>
    public static TimeSpan Multiply(TimeSpan value, double factor)
        => TimeSpan.FromTicks((long)(value.Ticks * factor));

    /// <summary>
    /// Negates the specified <see cref="TimeSpan"/>.
    /// </summary>
    public static TimeSpan Negate(TimeSpan value)
        => value.Negate();

    /// <summary>
    /// Parses the string exactly using the provided format and invariant culture.
    /// </summary>
    public static TimeSpan ParseExactInvariant(string input, string format)
        => TimeSpan.ParseExact(input, format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parses the string using any of the provided formats and invariant culture.
    /// </summary>
    public static TimeSpan ParseExactInvariant(string input, string[] formats)
        => TimeSpan.ParseExact(input, formats, CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse the string exactly using invariant culture. Returns <c>null</c> if parsing fails.
    /// </summary>
    public static TimeSpan? ParseExactInvariantOrNull(string? input, string format)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse the string using any of the provided formats and invariant culture. Returns <c>null</c> on failure.
    /// </summary>
    public static TimeSpan? ParseExactInvariantOrNull(string? input, string[] formats)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>
    /// Subtracts one <see cref="TimeSpan"/> from another.
    /// </summary>
    public static TimeSpan Subtract(TimeSpan left, TimeSpan right)
        => left - right;

    #endregion

    #region Queries

    /// <summary>
    /// Determines whether the TimeSpan is zero.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is zero; otherwise, false.</returns>
    public static bool IsZero(TimeSpan value)
        => value == TimeSpan.Zero;

    /// <summary>
    /// Determines whether the TimeSpan is positive.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is positive; otherwise, false.</returns>
    public static bool IsPositive(TimeSpan value)
        => value > TimeSpan.Zero;

    /// <summary>
    /// Determines whether the TimeSpan is negative.
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>True if the TimeSpan is negative; otherwise, false.</returns>
    public static bool IsNegative(TimeSpan value)
        => value < TimeSpan.Zero;

    #endregion

    #region Formatting

    /// <summary>
    /// Converts the TimeSpan to a human-readable string (e.g., "2 days, 3 hours").
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <param name="precision">The number of components to include (default is 2).</param>
    /// <returns>A human-readable string representation.</returns>
    public static string ToHumanReadable(TimeSpan value, int precision = 2)
    {
        if (value == TimeSpan.Zero)
            return "0 seconds";

        var isNegative = value < TimeSpan.Zero;
        value = Duration(value);

        var parts = new List<string>();

        if (value.Days > 0)
            parts.Add($"{value.Days} day{(value.Days == 1 ? "" : "s")}");
        
        if (value.Hours > 0)
            parts.Add($"{value.Hours} hour{(value.Hours == 1 ? "" : "s")}");
        
        if (value.Minutes > 0)
            parts.Add($"{value.Minutes} minute{(value.Minutes == 1 ? "" : "s")}");
        
        if (value.Seconds > 0)
            parts.Add($"{value.Seconds} second{(value.Seconds == 1 ? "" : "s")}");
        
        if (parts.Count == 0 && value.Milliseconds > 0)
            parts.Add($"{value.Milliseconds} millisecond{(value.Milliseconds == 1 ? "" : "s")}");

        var result = string.Join(", ", parts.Take(precision));
        return isNegative ? $"-{result}" : result;
    }

    /// <summary>
    /// Converts the TimeSpan to a compact string (e.g., "2d 3h 15m").
    /// </summary>
    /// <param name="value">The TimeSpan value.</param>
    /// <returns>A compact string representation.</returns>
    public static string ToCompactString(TimeSpan value)
    {
        if (value == TimeSpan.Zero)
            return "0s";

        var isNegative = value < TimeSpan.Zero;
        value = Duration(value);

        var parts = new List<string>();

        if (value.Days > 0)
            parts.Add($"{value.Days}d");
        
        if (value.Hours > 0)
            parts.Add($"{value.Hours}h");
        
        if (value.Minutes > 0)
            parts.Add($"{value.Minutes}m");
        
        if (value.Seconds > 0)
            parts.Add($"{value.Seconds}s");
        
        if (parts.Count == 0 && value.Milliseconds > 0)
            parts.Add($"{value.Milliseconds}ms");

        var result = string.Join(" ", parts);
        return isNegative ? $"-{result}" : result;
    }

    #endregion

    #region Rounding

    /// <summary>
    /// Rounds the TimeSpan to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded TimeSpan.</returns>
    public static TimeSpan Round(TimeSpan value, TimeSpan interval)
    {
        if (interval == TimeSpan.Zero)
            throw new ArgumentException("Interval cannot be zero.", nameof(interval));

        var ticks = (long)Math.Round((double)value.Ticks / interval.Ticks) * interval.Ticks;
        return TimeSpan.FromTicks(ticks);
    }

    /// <summary>
    /// Rounds the TimeSpan up to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round up.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded up TimeSpan.</returns>
    public static TimeSpan Ceiling(TimeSpan value, TimeSpan interval)
    {
        if (interval == TimeSpan.Zero)
            throw new ArgumentException("Interval cannot be zero.", nameof(interval));

        var ticks = (long)Math.Ceiling((double)value.Ticks / interval.Ticks) * interval.Ticks;
        return TimeSpan.FromTicks(ticks);
    }

    /// <summary>
    /// Rounds the TimeSpan down to the nearest specified interval.
    /// </summary>
    /// <param name="value">The TimeSpan to round down.</param>
    /// <param name="interval">The rounding interval.</param>
    /// <returns>The rounded down TimeSpan.</returns>
    public static TimeSpan Floor(TimeSpan value, TimeSpan interval)
    {
        if (interval == TimeSpan.Zero)
            throw new ArgumentException("Interval cannot be zero.", nameof(interval));

        var ticks = (long)Math.Floor((double)value.Ticks / interval.Ticks) * interval.Ticks;
        return TimeSpan.FromTicks(ticks);
    }

    #endregion
}
