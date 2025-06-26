using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTimeOffset"/> values.
/// </summary>
public static class DateTimeOffsetHelper
{
    #region DateTimeOffset

    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeSpan offset)
        => new(date.ToDateTime(time, DateTimeKind.Unspecified), offset);
    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeZoneInfo zone)
    {
        var dateTime = date.ToDateTime(time, DateTimeKind.Unspecified);
        var offset = zone.GetUtcOffset(dateTime);
        var dto = new DateTimeOffset(dateTime, offset);
        return TimeZoneInfo.ConvertTime(dto, zone);
    }

    public static DateTimeOffset FromDateTime(DateTime value)
        => new(value);

    public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);

    public static DateTimeOffset? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(milliseconds.Value) : (DateTimeOffset?)null;

    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds);

    public static DateTimeOffset? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(seconds.Value) : (DateTimeOffset?)null;

    public static DateTimeOffset ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.ParseExact(input, format, CultureInfo.InvariantCulture, styles);
    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTimeOffset?)null;

    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTimeOffset?)null;

    public static DateTimeOffset ToOffset(DateTimeOffset value, TimeSpan offset)
        => value.ToOffset(offset);
    #endregion
}
