using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateOnly"/> values.
/// </summary>
public static class DateOnlyHelper
{
    public static string Format(DateOnly value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static DateOnly ParseExactInvariant(string input, string format)
        => DateOnly.ParseExact(input, format, CultureInfo.InvariantCulture);

    public static bool TryParseExactInvariant(string input, string format, out DateOnly result)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    public static DateOnly AddDays(DateOnly value, int days)
        => value.AddDays(days);

    public static DateOnly AddMonths(DateOnly value, int months)
        => value.AddMonths(months);

    public static DateTime ToDateTime(DateOnly date, TimeOnly time, DateTimeKind kind = DateTimeKind.Unspecified)
        => date.ToDateTime(time, kind);

    public static DateOnly FromDateTime(DateTime dateTime)
        => DateOnly.FromDateTime(dateTime);
}
