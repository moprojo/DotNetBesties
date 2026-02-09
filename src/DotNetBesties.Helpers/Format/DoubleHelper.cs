using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="double"/> results.
/// </summary>
public static class DoubleHelper
{
    #region Mathematical Operations
    /// <summary>
    /// Rounds the double to the specified number of decimal places.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimals">The number of decimal places.</param>
    /// <returns>The rounded value.</returns>
    public static double Round(double value, int decimals = 0)
        => Math.Round(value, decimals);

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static double Clamp(double value, double min, double max)
        => Math.Clamp(value, min, max);

    /// <summary>
    /// Determines whether the value is approximately equal to another value within a tolerance.
    /// </summary>
    /// <param name="value">The first value.</param>
    /// <param name="other">The second value.</param>
    /// <param name="tolerance">The tolerance for comparison. Default is 1e-9.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    public static bool IsApproximately(double value, double other, double tolerance = 1e-9)
        => Math.Abs(value - other) <= tolerance;

    /// <summary>
    /// Gets the absolute value of the double.
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>The absolute value.</returns>
    public static double Abs(double value)
        => Math.Abs(value);

    /// <summary>
    /// Returns the sign of the double (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The double value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(double value)
        => Math.Sign(value);
    #endregion

    #region DateTime
    /// <summary>
    /// Converts a nullable <see cref="DateTime"/> to its OLE Automation date representation.
    /// </summary>
    public static double? ToOADate(DateTime? value) => value?.ToOADate();

    /// <summary>
    /// Converts a <see cref="DateTime"/> to its OLE Automation date representation.
    /// </summary>
    public static double ToOADate(DateTime value) => value.ToOADate();
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to an OLE Automation date.
    /// </summary>
    public static double? ToOADate(DateTimeOffset? value) => value?.DateTime.ToOADate();

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to an OLE Automation date.
    /// </summary>
    public static double ToOADate(DateTimeOffset value) => value.DateTime.ToOADate();
    #endregion

    #region Primitive
    /// <summary>
    /// Parses the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <returns>The parsed <see cref="double"/> value.</returns>
    public static double ParseInvariant(string input)
        => double.Parse(input, CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>.
    /// Returns <c>null</c> if parsing fails.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <returns>The parsed <see cref="double"/> value, or <c>null</c> if parsing fails.</returns>
    public static double? ParseInvariantOrNull(string? input)
        => double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result) ? result : null;
    #endregion

    #region TimeSpan
    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalDays(TimeSpan? value) => value?.TotalDays;

    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double TotalDays(TimeSpan value) => value.TotalDays;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalHours(TimeSpan? value) => value?.TotalHours;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double TotalHours(TimeSpan value) => value.TotalHours;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalMilliseconds(TimeSpan? value) => value?.TotalMilliseconds;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double TotalMilliseconds(TimeSpan value) => value.TotalMilliseconds;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalMinutes(TimeSpan? value) => value?.TotalMinutes;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double TotalMinutes(TimeSpan value) => value.TotalMinutes;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalSeconds(TimeSpan? value) => value?.TotalSeconds;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double TotalSeconds(TimeSpan value) => value.TotalSeconds;
    #endregion
}
