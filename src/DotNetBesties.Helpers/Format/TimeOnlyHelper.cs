using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="TimeOnly"/> values.
/// </summary>
public static class TimeOnlyHelper
{
    #region TimeOnly

    /// <summary>
    /// Adds the specified number of hours to a <see cref="TimeOnly"/> value.
    /// </summary>
    public static TimeOnly AddHours(TimeOnly value, int hours)
        => value.AddHours(hours);

    /// <summary>
    /// Adds the specified number of minutes to a <see cref="TimeOnly"/> value.
    /// </summary>
    public static TimeOnly AddMinutes(TimeOnly value, int minutes)
        => value.AddMinutes(minutes);

    /// <summary>
    /// Converts a <see cref="TimeOnly"/> to a <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="value">The time value.</param>
    /// <returns>A <see cref="TimeSpan"/> representing the time duration since midnight.</returns>
    public static TimeSpan ToTimeSpan(TimeOnly value)
        => value.ToTimeSpan();

    /// <summary>
    /// Converts a nullable <see cref="TimeOnly"/> to a nullable <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="value">The nullable time value.</param>
    /// <returns>A nullable <see cref="TimeSpan"/>, or <c>null</c> if the value is <c>null</c>.</returns>
    public static TimeSpan? ToTimeSpan(TimeOnly? value)
        => value?.ToTimeSpan();

    /// <summary>
    /// Creates a <see cref="TimeOnly"/> from a <see cref="DateTime"/> instance.
    /// </summary>
    public static TimeOnly FromDateTime(DateTime dateTime)
        => TimeOnly.FromDateTime(dateTime);

    /// <summary>
    /// Creates a <see cref="TimeOnly"/> from a <see cref="DateTimeOffset"/> instance.
    /// </summary>
    public static TimeOnly FromDateTimeOffset(DateTimeOffset value)
        => TimeOnly.FromDateTime(value.DateTime);

    /// <summary>
    /// Creates a <see cref="TimeOnly"/> from a <see cref="TimeSpan"/> instance.
    /// </summary>
    public static TimeOnly FromTimeSpan(TimeSpan timeSpan)
        => TimeOnly.FromTimeSpan(timeSpan);

    /// <summary>
    /// Parses a string exactly using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static TimeOnly ParseExactInvariant(string input, string format)
        => TimeOnly.ParseExact(input, format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse a string exactly using invariant culture. Returns <c>null</c> if parsing fails.
    /// </summary>
    public static TimeOnly? ParseExactInvariantOrNull(string? input, string format)
        => TimeOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse a string using any of the provided formats and invariant culture. Returns <c>null</c> on failure.
    /// </summary>
    public static TimeOnly? ParseExactInvariantOrNull(string? input, string[] formats)
        => TimeOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;

    #endregion
}
