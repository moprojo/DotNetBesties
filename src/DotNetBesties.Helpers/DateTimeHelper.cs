using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTime"/> values.
/// </summary>
public static class DateTimeHelper
{
    public static string Format(DateTime value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static DateTime ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

    public static bool TryParseExactInvariant(string input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
        => DateTime.SpecifyKind(value, kind);

    public static DateTime ToUniversalTime(DateTime value)
        => value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();

    public static DateTime ToLocalTime(DateTime value)
        => value.Kind == DateTimeKind.Local ? value : value.ToLocalTime();

    public static DateTimeOffset ToDateTimeOffset(DateTime value)
        => new(value);

    public static DateOnly ToDateOnly(DateTime value)
        => DateOnly.FromDateTime(value);

    public static int IsoWeek(DateTime value)
        => ISOWeek.GetWeekOfYear(value);
}
