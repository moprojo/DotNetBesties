using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="DateTime"/> operations.
/// </summary>
public static class DateTimeExtensions
{
    #region Conversion

    /// <summary>
    /// Converts the DateTime to Unix time in milliseconds.
    /// </summary>
    /// <param name="value">The DateTime to convert.</param>
    /// <returns>The Unix time in milliseconds.</returns>
    public static long ToUnixTimeMilliseconds(this DateTime value)
        => Format.LongHelper.ToUnixTimeMilliseconds(value);

    /// <summary>
    /// Converts the DateTime to Unix time in seconds.
    /// </summary>
    /// <param name="value">The DateTime to convert.</param>
    /// <returns>The Unix time in seconds.</returns>
    public static long ToUnixTimeSeconds(this DateTime value)
        => Format.LongHelper.ToUnixTimeSeconds(value);

    /// <summary>
    /// Converts the DateTime to UTC if needed.
    /// </summary>
    /// <param name="value">The DateTime to convert.</param>
    /// <returns>The DateTime in UTC.</returns>
    public static DateTime ToUtc(this DateTime value)
        => Format.DateTimeHelper.ToUniversalTime(value);

    /// <summary>
    /// Converts the nullable DateTime to UTC if needed.
    /// </summary>
    /// <param name="value">The nullable DateTime to convert.</param>
    /// <returns>The DateTime in UTC or null.</returns>
    public static DateTime? ToUtc(this DateTime? value)
        => Format.DateTimeHelper.ToUniversalTime(value);

    /// <summary>
    /// Converts the DateTime to local time if needed.
    /// </summary>
    /// <param name="value">The DateTime to convert.</param>
    /// <returns>The DateTime in local time.</returns>
    public static DateTime ToLocal(this DateTime value)
        => Format.DateTimeHelper.ToLocalTime(value);

    /// <summary>
    /// Converts the DateTime to its OLE Automation date representation.
    /// </summary>
    /// <param name="value">The DateTime to convert.</param>
    /// <returns>The OLE Automation date equivalent.</returns>
    public static double ToOADate(this DateTime value)
        => Format.DoubleHelper.ToOADate(value);

    /// <summary>
    /// Converts the nullable DateTime to its OLE Automation date representation.
    /// </summary>
    /// <param name="value">The nullable DateTime to convert.</param>
    /// <returns>The OLE Automation date equivalent or null.</returns>
    public static double? ToOADate(this DateTime? value)
        => Format.DoubleHelper.ToOADate(value);

    /// <summary>
    /// Extracts the date portion as DateOnly.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The date portion as DateOnly.</returns>
    public static DateOnly ToDateOnly(this DateTime value)
        => Format.DateTimeHelper.ToDateOnly(value);

    /// <summary>
    /// Extracts the time portion as TimeOnly.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The time portion as TimeOnly.</returns>
    public static TimeOnly ToTimeOnly(this DateTime value)
        => Format.DateTimeHelper.ToTimeOnly(value);

    #endregion

    #region String Formatting

    /// <summary>
    /// Formats the DateTime using the specified format and provider.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <param name="format">The format string. Default is "O" (round-trip format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation.</returns>
    public static string ToInvariantString(this DateTime value, string format = "O", IFormatProvider? provider = null)
        => Format.StringHelper.FromDateTime(value, format, provider);

    /// <summary>
    /// Formats the nullable DateTime using the specified format and provider.
    /// </summary>
    /// <param name="value">The nullable DateTime value.</param>
    /// <param name="format">The format string. Default is "O" (round-trip format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation or null.</returns>
    public static string? ToInvariantString(this DateTime? value, string format = "O", IFormatProvider? provider = null)
        => Format.StringHelper.FromDateTime(value, format, provider);

    #endregion

    #region ISO Week

    /// <summary>
    /// Gets the ISO 8601 week number for the DateTime.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The ISO week number.</returns>
    public static int GetIsoWeek(this DateTime value)
        => Format.DateTimeHelper.IsoWeek(value);

    /// <summary>
    /// Gets the ISO 8601 week year for the DateTime.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The ISO week year.</returns>
    public static int GetIsoWeekYear(this DateTime value)
        => Format.DateTimeHelper.GetIsoWeekYear(value);

    #endregion

    #region Kind Specification

    /// <summary>
    /// Returns a new DateTime with the specified DateTimeKind.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <param name="kind">The DateTimeKind to apply.</param>
    /// <returns>A new DateTime with the specified kind.</returns>
    public static DateTime WithKind(this DateTime value, DateTimeKind kind)
        => Format.DateTimeHelper.SpecifyKind(value, kind);

    /// <summary>
    /// Returns a new DateTime with DateTimeKind.Utc.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>A new DateTime with UTC kind.</returns>
    public static DateTime AsUtc(this DateTime value)
        => Format.DateTimeHelper.SpecifyKind(value, DateTimeKind.Utc);

    /// <summary>
    /// Returns a new DateTime with DateTimeKind.Local.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>A new DateTime with Local kind.</returns>
    public static DateTime AsLocal(this DateTime value)
        => Format.DateTimeHelper.SpecifyKind(value, DateTimeKind.Local);

    /// <summary>
    /// Returns a new DateTime with DateTimeKind.Unspecified.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>A new DateTime with Unspecified kind.</returns>
    public static DateTime AsUnspecified(this DateTime value)
        => Format.DateTimeHelper.SpecifyKind(value, DateTimeKind.Unspecified);

    #endregion

    #region Manipulation

    /// <summary>
    /// Returns the start of the day (midnight).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the day.</returns>
    public static DateTime StartOfDay(this DateTime value)
        => Format.DateTimeHelper.StartOfDay(value);

    /// <summary>
    /// Returns the end of the day (23:59:59.999).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the day.</returns>
    public static DateTime EndOfDay(this DateTime value)
        => Format.DateTimeHelper.EndOfDay(value);

    /// <summary>
    /// Returns the start of the week (Monday).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <param name="firstDayOfWeek">The first day of the week. Default is Monday.</param>
    /// <returns>The DateTime at the start of the week.</returns>
    public static DateTime StartOfWeek(this DateTime value, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        => Format.DateTimeHelper.StartOfWeek(value, firstDayOfWeek);

    /// <summary>
    /// Returns the start of the month.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the month.</returns>
    public static DateTime StartOfMonth(this DateTime value)
        => Format.DateTimeHelper.StartOfMonth(value);

    /// <summary>
    /// Returns the end of the month.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the month.</returns>
    public static DateTime EndOfMonth(this DateTime value)
        => Format.DateTimeHelper.EndOfMonth(value);

    /// <summary>
    /// Returns the start of the year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the start of the year.</returns>
    public static DateTime StartOfYear(this DateTime value)
        => Format.DateTimeHelper.StartOfYear(value);

    /// <summary>
    /// Returns the end of the year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>The DateTime at the end of the year.</returns>
    public static DateTime EndOfYear(this DateTime value)
        => Format.DateTimeHelper.EndOfYear(value);

    #endregion

    #region Queries

    /// <summary>
    /// Determines whether the DateTime is in the past.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is in the past; otherwise, false.</returns>
    public static bool IsInPast(this DateTime value)
        => Format.DateTimeHelper.IsInPast(value);

    /// <summary>
    /// Determines whether the DateTime is in the future.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is in the future; otherwise, false.</returns>
    public static bool IsInFuture(this DateTime value)
        => Format.DateTimeHelper.IsInFuture(value);

    /// <summary>
    /// Determines whether the DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is today; otherwise, false.</returns>
    public static bool IsToday(this DateTime value)
        => Format.DateTimeHelper.IsToday(value);

    /// <summary>
    /// Determines whether the DateTime is a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is a weekend; otherwise, false.</returns>
    public static bool IsWeekend(this DateTime value)
        => Format.DateTimeHelper.IsWeekend(value);

    /// <summary>
    /// Determines whether the DateTime is a weekday (Monday to Friday).
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the DateTime is a weekday; otherwise, false.</returns>
    public static bool IsWeekday(this DateTime value)
        => Format.DateTimeHelper.IsWeekday(value);

    /// <summary>
    /// Determines whether the DateTime is a leap year.
    /// </summary>
    /// <param name="value">The DateTime value.</param>
    /// <returns>True if the year is a leap year; otherwise, false.</returns>
    public static bool IsLeapYear(this DateTime value)
        => Format.DateTimeHelper.IsLeapYear(value);

    #endregion

    #region Age Calculation

    /// <summary>
    /// Calculates the age in years from the DateTime to now.
    /// </summary>
    /// <param name="birthDate">The birth date.</param>
    /// <returns>The age in years.</returns>
    public static int GetAge(this DateTime birthDate)
        => Format.DateTimeHelper.GetAge(birthDate);

    /// <summary>
    /// Calculates the age in years from the DateTime to a specific date.
    /// </summary>
    /// <param name="birthDate">The birth date.</param>
    /// <param name="asOfDate">The date to calculate age as of.</param>
    /// <returns>The age in years.</returns>
    public static int GetAge(this DateTime birthDate, DateTime asOfDate)
        => Format.DateTimeHelper.GetAge(birthDate, asOfDate);

    #endregion
}
