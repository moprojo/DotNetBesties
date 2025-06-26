using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="TimeSpan"/> values.
/// </summary>
public static class TimeSpanHelper
{
    #region TimeSpan

    public static TimeSpan ParseExactInvariant(string input, string format)
        => TimeSpan.ParseExact(input, format, CultureInfo.InvariantCulture);

    public static TimeSpan ParseExactInvariant(string input, string[] formats)
        => TimeSpan.ParseExact(input, formats, CultureInfo.InvariantCulture);


    public static TimeSpan? ParseExactInvariantOrNull(string? input, string format)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out var result) ? result : (TimeSpan?)null;

    public static TimeSpan? ParseExactInvariantOrNull(string? input, string[] formats)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out var result) ? result : (TimeSpan?)null;

    public static TimeSpan Add(TimeSpan left, TimeSpan right)
        => left + right;

    public static TimeSpan Subtract(TimeSpan left, TimeSpan right)
        => left - right;

    public static TimeSpan Multiply(TimeSpan value, double factor)
        => TimeSpan.FromTicks((long)(value.Ticks * factor));

    public static TimeSpan Divide(TimeSpan value, double divisor)
        => TimeSpan.FromTicks((long)(value.Ticks / divisor));

    public static TimeSpan Negate(TimeSpan value)
        => value.Negate();

    public static TimeSpan Duration(TimeSpan value)
        => value.Duration();

    #endregion
}
