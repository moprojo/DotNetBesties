using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="DateTimeOffset"/> values.
/// </summary>
public static class DateTimeOffsetHelper
{
    #region DateTimeOffset
    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> value to the specified <see cref="TimeZoneInfo"/>.
    /// </summary>
    public static DateTimeOffset ConvertTime(DateTimeOffset value, TimeZoneInfo destination)
        => TimeZoneInfo.ConvertTime(value, destination);

    /// <summary>
    /// Combines <see cref="DateOnly"/>, <see cref="TimeOnly"/>, and a fixed offset into a <see cref="DateTimeOffset"/>.
    /// </summary>
    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeSpan offset)
        => new(date.ToDateTime(time, DateTimeKind.Unspecified), offset);

    /// <summary>
    /// Combines <see cref="DateOnly"/> and <see cref="TimeOnly"/> using timezone rules to produce a <see cref="DateTimeOffset"/>.
    /// </summary>
    public static DateTimeOffset FromDateOnly(DateOnly date, TimeOnly time, TimeZoneInfo zone)
    {
        var dateTime = date.ToDateTime(time, DateTimeKind.Unspecified);
        var offset = zone.GetUtcOffset(dateTime);
        var dto = new DateTimeOffset(dateTime, offset);
        return TimeZoneInfo.ConvertTime(dto, zone);
    }

    /// <summary>
    /// Creates a <see cref="DateTimeOffset"/> from a <see cref="DateTime"/> value.
    /// </summary>
    public static DateTimeOffset FromDateTime(DateTime value)
        => new(value);

    /// <summary>
    /// Converts Unix milliseconds to a <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);

    /// <summary>
    /// Converts nullable Unix milliseconds to a nullable <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    public static DateTimeOffset? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(milliseconds.Value) : null;

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    public static DateTimeOffset? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(seconds.Value) : null;

    /// <summary>
    /// Parses a string exactly using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static DateTimeOffset ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

    /// <summary>
    /// Attempts to parse a string using invariant culture and the specified format. Returns <c>null</c> on failure.
    /// </summary>
    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse a string using any of the provided formats and invariant culture. Returns <c>null</c> if none match.
    /// </summary>
    public static DateTimeOffset? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : null;

    /// <summary>
    /// Adjusts the <see cref="DateTimeOffset"/> to the specified offset while keeping the same UTC time.
    /// </summary>
    public static DateTimeOffset ToOffset(DateTimeOffset value, TimeSpan offset)
        => value.ToOffset(offset);
    #endregion
}
