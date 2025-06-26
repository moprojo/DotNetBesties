using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="TimeSpan"/> values.
/// </summary>
public static class TimeSpanHelper
{
    public static string? Format(TimeSpan? value, string format = "c", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string Format(TimeSpan value, string format = "c", IFormatProvider? provider = null)
        => Format((TimeSpan?)value, format, provider)!;

    public static TimeSpan ParseExactInvariant(string input, string format)
        => TimeSpan.ParseExact(input, format, CultureInfo.InvariantCulture);

    public static TimeSpan ParseExactInvariant(string input, string[] formats)
        => TimeSpan.ParseExact(input, formats, CultureInfo.InvariantCulture);

    public static bool TryParseExactInvariant(string? input, string[] formats, out TimeSpan result)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out result);

    public static bool TryParseExactInvariant(string input, string format, out TimeSpan result)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out result);

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
}
