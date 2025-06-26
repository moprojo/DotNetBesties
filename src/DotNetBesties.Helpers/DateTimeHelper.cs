using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTime"/> values.
/// </summary>
public static class DateTimeHelper
{
    #region DateTime
    public static DateTime ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

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

    public static DateTime FromDateTimeOffset(DateTimeOffset value)
        => value.DateTime;

    public static DateTime FromDateOnly(DateOnly date, TimeOnly time, DateTimeKind kind = DateTimeKind.Unspecified)
        => date.ToDateTime(time, kind);

    public static DateTime FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

    public static DateTime FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;

    public static DateTime? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : (DateTime?)null;

    public static DateTime? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? FromUnixTimeMilliseconds(milliseconds.Value) : (DateTime?)null;
    #endregion
}
