using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Utility methods for working with <see cref="DateTime"/> values.
/// </summary>
public static class DateTimeHelper
{
    #region DateTime

    /// <summary>
    /// Creates a <see cref="DateTime"/> from the provided <see cref="DateOnly"/> and <see cref="TimeOnly"/> values.
    /// </summary>
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
        => milliseconds.HasValue ? FromUnixTimeMilliseconds(milliseconds.Value) : null;

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateTime"/> in UTC.
    /// </summary>
    public static DateTime? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : null;

    /// <summary>
    /// Gets the ISO 8601 week number for the specified date.
    /// </summary>
    public static int IsoWeek(DateTime value)
        => ISOWeek.GetWeekOfYear(value);

    /// <summary>
    /// Gets the ISO 8601 week year for the specified date.
    /// </summary>
    public static int GetIsoWeekYear(DateTime value)
        => ISOWeek.GetYear(value);

    /// <summary>
    /// Extracts the date portion as <see cref="DateOnly"/>.
    /// </summary>
    public static DateOnly ToDateOnly(DateTime value)
        => DateOnly.FromDateTime(value);

    /// <summary>
    /// Extracts the time portion as <see cref="TimeOnly"/>.
    /// </summary>
    public static TimeOnly ToTimeOnly(DateTime value)
        => TimeOnly.FromDateTime(value);

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
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out var result) ? result : null;

    /// <summary>
    /// Attempts to parse a string using any of the provided formats and invariant culture.
    /// Returns <c>null</c> if none match.
    /// </summary>
    public static DateTime? ParseExactInvariantOrNull(string? input, string[] formats, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out var result) ? result : null;

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

    /// <summary>
    /// Ensures the nullable value is in UTC, converting if necessary.
    /// </summary>
    public static DateTime? ToUniversalTime(DateTime? value)
        => value.HasValue ? ToUniversalTime(value.Value) : null;

    #endregion

    #region Date Manipulation

    /// <summary>
    /// Returns the start of the day (midnight).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the day.</returns>
    public static DateTime StartOfDay(DateTime value)
        => value.Date;

    /// <summary>
    /// Returns the end of the day (23:59:59.999).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the day.</returns>
    public static DateTime EndOfDay(DateTime value)
        => value.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Returns the start of the week.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <param name="firstDayOfWeek">The first day of the week. Default is Monday.</param>
    /// <returns>The DateTime at the start of the week.</returns>
    public static DateTime StartOfWeek(DateTime value, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        var diff = (7 + (value.DayOfWeek - firstDayOfWeek)) % 7;
        return value.AddDays(-diff).Date;
    }

    /// <summary>
    /// Returns the start of the month.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the month.</returns>
    public static DateTime StartOfMonth(DateTime value)
        => new DateTime(value.Year, value.Month, 1, 0, 0, 0, value.Kind);

    /// <summary>
    /// Returns the end of the month.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the month.</returns>
    public static DateTime EndOfMonth(DateTime value)
    {
        var daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
        return new DateTime(value.Year, value.Month, daysInMonth, 23, 59, 59, 999, value.Kind).AddTicks(9999);
    }

    /// <summary>
    /// Returns the start of the year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the year.</returns>
    public static DateTime StartOfYear(DateTime value)
        => new DateTime(value.Year, 1, 1, 0, 0, 0, value.Kind);

    /// <summary>
    /// Returns the end of the year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the year.</returns>
    public static DateTime EndOfYear(DateTime value)
        => new DateTime(value.Year, 12, 31, 23, 59, 59, 999, value.Kind).AddTicks(9999);

    #endregion

    #region Date Queries

    /// <summary>
    /// Determines whether the DateTime is in the past.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is in the past; otherwise, false.</returns>
    public static bool IsInPast(DateTime value)
        => value < DateTime.Now;

    /// <summary>
    /// Determines whether the DateTime is in the future.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is in the future; otherwise, false.</returns>
    public static bool IsInFuture(DateTime value)
        => value > DateTime.Now;

    /// <summary>
    /// Determines whether the DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is today; otherwise, false.</returns>
    public static bool IsToday(DateTime value)
        => value.Date == DateTime.Today;

    /// <summary>
    /// Determines whether the DateTime is a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is a weekend; otherwise, false.</returns>
    public static bool IsWeekend(DateTime value)
        => value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// Determines whether the DateTime is a weekday (Monday to Friday).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is a weekday; otherwise, false.</returns>
    public static bool IsWeekday(DateTime value)
        => !IsWeekend(value);

    /// <summary>
    /// Determines whether the DateTime is a leap year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the year is a leap year; otherwise, false.</returns>
    public static bool IsLeapYear(DateTime value)
        => DateTime.IsLeapYear(value.Year);

    #endregion

    #region Age Calculation

    /// <summary>
    /// Calculates the age in years from the birth date to now.
    /// </summary>
    /// <param name="birthDate">The birth date.</param>
    /// <returns>The age in years.</returns>
    public static int GetAge(DateTime birthDate)
        => GetAge(birthDate, DateTime.Today);

    /// <summary>
    /// Calculates the age in years from the birth date to a specific date.
    /// </summary>
    /// <param name="birthDate">The birth date.</param>
    /// <param name="asOfDate">The date to calculate age as of.</param>
    /// <returns>The age in years.</returns>
    public static int GetAge(DateTime birthDate, DateTime asOfDate)
    {
        var age = asOfDate.Year - birthDate.Year;
        if (asOfDate.Month < birthDate.Month || (asOfDate.Month == birthDate.Month && asOfDate.Day < birthDate.Day))
            age--;
        return age;
    }

    #endregion
}
