using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateTime"/> values.
/// </summary>
public static class DateTimeHelper
{
    #region DateTime

    /// <summary>
    /// Creates a <see cref="DateTime"/> from the provided <see cref="DateOnly"/> and <see cref="TimeOnly"/> values.
    /// </summary>
    /// <param name="date">The date component.</param>
    /// <param name="time">The time component.</param>
    /// <param name="kind">The <see cref="DateTimeKind"/> for the resulting instance.</param>
    /// <returns>A new <see cref="DateTime"/> representing the given date and time.</returns>
    public static DateTime FromDateOnly(DateOnly date, TimeOnly time, DateTimeKind kind = DateTimeKind.Unspecified)
        => date.ToDateTime(time, kind);

    /// <summary>
    /// Extracts the <see cref="DateTime"/> portion of a <see cref="DateTimeOffset"/>.
    /// </summary>
    public static DateTime FromDateTimeOffset(DateTimeOffset value)
        => value.DateTime;

    /// <summary>
    /// Converts Unix milliseconds to a <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime FromUnixTimeMilliseconds(long milliseconds)
        => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;

    /// <summary>
    /// Converts nullable Unix milliseconds to a nullable <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime? FromUnixTimeMilliseconds(long? milliseconds)
        => milliseconds.HasValue ? FromUnixTimeMilliseconds(milliseconds.Value) : (DateTime?)null;

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : (DateTime?)null;

    /// <summary>
    /// Gets the ISO 8601 week number for the specified date.
    /// </summary>
    public static int IsoWeek(DateTime value)
        => ISOWeek.GetWeekOfYear(value);

    /// <summary>
    /// Parses a string using the exact format and <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static DateTime ParseExactInvariant(string input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, styles);

    /// <summary>
    /// Attempts to parse a string with the specified format and invariant culture.
    /// Returns <c>null</c> if parsing fails.
    /// </summary>
    public static DateTime? ParseExactInvariantOrNull(string? input, string format, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTime?)null;

    /// <summary>
    /// Attempts to parse a string using any of the provided formats and invariant culture.
    /// Returns <c>null</c> if none match.
    /// </summary>
    public static DateTime? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : (DateTime?)null;

    /// <summary>
    /// Returns a new instance of <see cref="DateTime"/> with the specified <see cref="DateTimeKind"/>.
    /// </summary>
    public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
        => DateTime.SpecifyKind(value, kind);

    /// <summary>
    /// Ensures the value is in local time, converting if necessary.
    /// </summary>
    public static DateTime ToLocalTime(DateTime value)
        => value.Kind == DateTimeKind.Local ? value : value.ToLocalTime();

    /// <summary>
    /// Ensures the value is in UTC, converting if necessary.
    /// </summary>
    public static DateTime ToUniversalTime(DateTime value)
        => value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();

    #endregion
}
