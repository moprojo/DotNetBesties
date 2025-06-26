using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helper methods returning <see cref="int"/> values extracted or parsed from other types.
/// </summary>
public static class IntHelper
{
    #region DateOnly
    /// <summary>
    /// Gets the day component of the specified <see cref="DateOnly"/>.
    /// </summary>
    public static int Day(DateOnly value) => value.Day;

    /// <summary>
    /// Gets the day of the year represented by the specified <see cref="DateOnly"/>.
    /// </summary>
    public static int DayOfYear(DateOnly value) => value.DayOfYear;

    /// <summary>
    /// Gets the month component of the specified <see cref="DateOnly"/>.
    /// </summary>
    public static int Month(DateOnly value) => value.Month;

    /// <summary>
    /// Gets the year component of the specified <see cref="DateOnly"/>.
    /// </summary>
    public static int Year(DateOnly value) => value.Year;
    #endregion

    #region DateTime
    /// <summary>
    /// Gets the day component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Day(DateTime value) => value.Day;

    /// <summary>
    /// Gets the day of the year represented by the specified <see cref="DateTime"/>.
    /// </summary>
    public static int DayOfYear(DateTime value) => value.DayOfYear;

    /// <summary>
    /// Gets the hour component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Hour(DateTime value) => value.Hour;

    /// <summary>
    /// Gets the millisecond component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Millisecond(DateTime value) => value.Millisecond;

    /// <summary>
    /// Gets the minute component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Minute(DateTime value) => value.Minute;

    /// <summary>
    /// Gets the month component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Month(DateTime value) => value.Month;

    /// <summary>
    /// Gets the second component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Second(DateTime value) => value.Second;

    /// <summary>
    /// Gets the year component of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int Year(DateTime value) => value.Year;
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Gets the day component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Day(DateTimeOffset value) => value.Day;

    /// <summary>
    /// Gets the day of the year represented by the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int DayOfYear(DateTimeOffset value) => value.DayOfYear;

    /// <summary>
    /// Gets the hour component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Hour(DateTimeOffset value) => value.Hour;

    /// <summary>
    /// Gets the millisecond component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Millisecond(DateTimeOffset value) => value.Millisecond;

    /// <summary>
    /// Gets the minute component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Minute(DateTimeOffset value) => value.Minute;

    /// <summary>
    /// Gets the month component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Month(DateTimeOffset value) => value.Month;

    /// <summary>
    /// Gets the second component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Second(DateTimeOffset value) => value.Second;

    /// <summary>
    /// Gets the year component of the specified <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Year(DateTimeOffset value) => value.Year;
    #endregion

    #region Primitive
    /// <summary>
    /// Parses the specified string using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    public static int ParseInvariant(string input) => int.Parse(input, CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Gets the day component of the specified <see cref="TimeSpan"/> interval.
    /// </summary>
    public static int Days(TimeSpan value) => value.Days;

    /// <summary>
    /// Gets the hour component of the specified <see cref="TimeSpan"/> interval.
    /// </summary>
    public static int Hours(TimeSpan value) => value.Hours;

    /// <summary>
    /// Gets the millisecond component of the specified <see cref="TimeSpan"/> interval.
    /// </summary>
    public static int Milliseconds(TimeSpan value) => value.Milliseconds;

    /// <summary>
    /// Gets the minute component of the specified <see cref="TimeSpan"/> interval.
    /// </summary>
    public static int Minutes(TimeSpan value) => value.Minutes;

    /// <summary>
    /// Gets the second component of the specified <see cref="TimeSpan"/> interval.
    /// </summary>
    public static int Seconds(TimeSpan value) => value.Seconds;
    #endregion
}
