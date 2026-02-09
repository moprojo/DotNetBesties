using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="DateOnly"/> values.
/// </summary>
public static class DateOnlyHelper
{
    #region DateOnly
    /// <summary>
    /// Adds the specified number of days to a <see cref="DateOnly"/> value.
    /// </summary>
    public static DateOnly AddDays(DateOnly value, int days)
        => value.AddDays(days);

    /// <summary>
    /// Adds the specified number of months to a <see cref="DateOnly"/> value.
    /// </summary>
    public static DateOnly AddMonths(DateOnly value, int months)
        => value.AddMonths(months);

    /// <summary>
    /// Adds the specified number of years to a <see cref="DateOnly"/> value.
    /// </summary>
    public static DateOnly AddYears(DateOnly value, int years)
        => value.AddYears(years);

    /// <summary>
    /// Converts a <see cref="DateOnly"/> to a <see cref="DateTime"/> using the specified <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="time">The time component.</param>
    /// <returns>A <see cref="DateTime"/> combining the date and time.</returns>
    public static DateTime ToDateTime(DateOnly value, TimeOnly time)
        => value.ToDateTime(time, DateTimeKind.Unspecified);

    /// <summary>
    /// Converts a <see cref="DateOnly"/> to a <see cref="DateTime"/> using the specified <see cref="TimeOnly"/> and <see cref="DateTimeKind"/>.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="time">The time component.</param>
    /// <param name="kind">The kind of date and time.</param>
    /// <returns>A <see cref="DateTime"/> combining the date and time.</returns>
    public static DateTime ToDateTime(DateOnly value, TimeOnly time, DateTimeKind kind)
        => value.ToDateTime(time, kind);

    /// <summary>
    /// Creates a <see cref="DateOnly"/> from a <see cref="DateTime"/> instance.
    /// </summary>
    public static DateOnly FromDateTime(DateTime dateTime)
        => DateOnly.FromDateTime(dateTime);

    /// <summary>
    /// Creates a <see cref="DateOnly"/> from a <see cref="DateTimeOffset"/> instance.
    /// </summary>
    public static DateOnly FromDateTimeOffset(DateTimeOffset value)
        => DateOnly.FromDateTime(value.DateTime);

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateOnly"/> in UTC.
    /// </summary>
    public static DateOnly FromUnixTimeSeconds(long seconds)
        => DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime);

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateOnly"/> in UTC.
    /// </summary>
    public static DateOnly? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : null;

    /// <summary>
    /// Parses a string exactly using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static DateOnly ParseExactInvariant(string input, string format)
        => DateOnly.ParseExact(input, format, CultureInfo.InvariantCulture);


    /// <summary>
    /// Attempts to parse a string exactly using invariant culture. Returns <c>null</c> if parsing fails.
    /// </summary>
    public static DateOnly? ParseExactInvariantOrNull(string? input, string format)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse a string using any of the provided formats and invariant culture. Returns <c>null</c> on failure.
    /// </summary>
    public static DateOnly? ParseExactInvariantOrNull(string? input, string[] formats)
        => DateOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;

    /// <summary>
    /// Converts a <see cref="DateOnly"/> to Unix time in seconds (UTC).
    /// </summary>
    public static long ToUnixTimeSeconds(DateOnly value)
        => new DateTimeOffset(value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)).ToUnixTimeSeconds();

    /// <summary>
    /// Converts a nullable <see cref="DateOnly"/> to Unix time in seconds (UTC).
    /// </summary>
    public static long? ToUnixTimeSeconds(DateOnly? value)
        => value.HasValue ? ToUnixTimeSeconds(value.Value) : null;

    #endregion
}
