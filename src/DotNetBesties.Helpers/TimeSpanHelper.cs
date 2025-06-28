using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

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
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out var result) ? result : (TimeSpan?)null;

    /// <summary>
    /// Attempts to parse the string using any of the provided formats and invariant culture. Returns <c>null</c> on failure.
    /// </summary>
    public static TimeSpan? ParseExactInvariantOrNull(string? input, string[] formats)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out var result) ? result : (TimeSpan?)null;

    /// <summary>
    /// Subtracts one <see cref="TimeSpan"/> from another.
    /// </summary>
    public static TimeSpan Subtract(TimeSpan left, TimeSpan right)
        => left - right;

    #endregion
}
