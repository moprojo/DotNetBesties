using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="int"/> results.
/// </summary>
public static class IntHelper
{
    #region Mathematical Operations

    /// <summary>
    /// Determines whether the integer is even.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is even; otherwise, <c>false</c>.</returns>
    public static bool IsEven(int value)
        => (value & 1) == 0;

    /// <summary>
    /// Determines whether the integer is odd.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is odd; otherwise, <c>false</c>.</returns>
    public static bool IsOdd(int value)
        => (value & 1) != 0;

    /// <summary>
    /// Determines whether the integer is a prime number.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns><c>true</c> if the value is prime; otherwise, <c>false</c>.</returns>
    public static bool IsPrime(int value)
    {
        if (value <= 1)
            return false;

        if (value == 2)
            return true;

        if (value % 2 == 0)
            return false;

        var boundary = (int)Math.Floor(Math.Sqrt(value));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (value % i == 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static int Clamp(int value, int min, int max)
        => Math.Clamp(value, min, max);

    /// <summary>
    /// Gets the absolute value of the integer.
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>The absolute value.</returns>
    public static int Abs(int value)
        => Math.Abs(value);

    /// <summary>
    /// Returns the sign of the integer (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The integer value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(int value)
        => Math.Sign(value);

    #endregion

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
    /// Gets the ISO week number of the specified <see cref="DateTime"/>.
    /// </summary>
    public static int IsoWeek(DateTime value) => ISOWeek.GetWeekOfYear(value);

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
