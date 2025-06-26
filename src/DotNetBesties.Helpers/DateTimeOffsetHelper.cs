using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTimeOffset"/> values.
/// </summary>
public static class DateTimeOffsetHelper
{
    public static string? Format(DateTimeOffset? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string Format(DateTimeOffset value, string format = "O", IFormatProvider? provider = null)
        => Format((DateTimeOffset?)value, format, provider)!;

    public static DateTimeOffset ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

    public static bool TryParseExactInvariant(string input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static bool TryParseExactInvariant(string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);

    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTimeOffset?)null;

    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTimeOffset?)null;

    public static DateTimeOffset ToOffset(DateTimeOffset value, TimeSpan offset)
        => value.ToOffset(offset);

    public static DateTimeOffset ConvertTime(DateTimeOffset value, TimeZoneInfo destination)
        => TimeZoneInfo.ConvertTime(value, destination);

    public static long ToUnixTimeSeconds(DateTimeOffset value)
        => value.ToUnixTimeSeconds();

    public static long? ToUnixTimeSeconds(DateTimeOffset? value)
        => value?.ToUnixTimeSeconds();

    public static long ToUnixTimeMilliseconds(DateTimeOffset value)
        => value.ToUnixTimeMilliseconds();

    public static long? ToUnixTimeMilliseconds(DateTimeOffset? value)
        => value?.ToUnixTimeMilliseconds();

    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds);

    public static DateTimeOffset? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(seconds.Value) : (DateTimeOffset?)null;

    public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);

    public static DateTimeOffset? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(milliseconds.Value) : (DateTimeOffset?)null;

    public static DateTimeOffset FromDateTime(DateTime value)
        => new(value);

    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeSpan offset)
        => new(date.ToDateTime(time, DateTimeKind.Unspecified), offset);

    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeZoneInfo zone)
    {
        var dateTime = date.ToDateTime(time, DateTimeKind.Unspecified);
        var offset = zone.GetUtcOffset(dateTime);
        var dto = new DateTimeOffset(dateTime, offset);
        return TimeZoneInfo.ConvertTime(dto, zone);
    }
}
