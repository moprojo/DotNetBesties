using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTime"/> values.
/// </summary>
public static class DateTimeHelper
{
    public static string? Format(DateTime? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string Format(DateTime value, string format = "O", IFormatProvider? provider = null)
        => Format((DateTime?)value, format, provider)!;

    public static DateTime ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

    public static bool TryParseExactInvariant(string input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static bool TryParseExactInvariant(string? input, string[] formats, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);

    public static DateTime? ParseExactInvariantOrNull(string? input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTime?)null;

    public static DateTime? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTime?)null;

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

    public static long ToUnixTimeSeconds(DateTime value)
        => new DateTimeOffset(value).ToUnixTimeSeconds();

    public static long ToUnixTimeMilliseconds(DateTime value)
        => new DateTimeOffset(value).ToUnixTimeMilliseconds();

    public static DateTime FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

    public static DateTime FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;

    public static DateTime? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : (DateTime?)null;

    public static DateTime? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? FromUnixTimeMilliseconds(milliseconds.Value) : (DateTime?)null;
}
