using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="float"/> results.
/// </summary>
public static class FloatHelper
{
    #region Mathematical Operations

    /// <summary>
    /// Rounds the float to the specified number of decimal places.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimals">The number of decimal places.</param>
    /// <returns>The rounded value.</returns>
    public static float Round(float value, int decimals = 0)
        => (float)Math.Round(value, decimals);

    /// <summary>
    /// Clamps the value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static float Clamp(float value, float min, float max)
        => Math.Clamp(value, min, max);

    /// <summary>
    /// Determines whether the value is approximately equal to another value within a tolerance.
    /// </summary>
    /// <param name="value">The first value.</param>
    /// <param name="other">The second value.</param>
    /// <param name="tolerance">The tolerance for comparison. Default is 0.0001f.</param>
    /// <returns><c>true</c> if the values are approximately equal; otherwise, <c>false</c>.</returns>
    public static bool IsApproximately(float value, float other, float tolerance = 0.0001f)
        => Math.Abs(value - other) <= tolerance;

    /// <summary>
    /// Gets the absolute value of the float.
    /// </summary>
    /// <param name="value">The float value.</param>
    /// <returns>The absolute value.</returns>
    public static float Abs(float value)
        => Math.Abs(value);

    /// <summary>
    /// Returns the sign of the float (-1, 0, or 1).
    /// </summary>
    /// <param name="value">The float value.</param>
    /// <returns>-1 if negative, 0 if zero, 1 if positive.</returns>
    public static int Sign(float value)
        => Math.Sign(value);

    #endregion

    #region DateTime
    /// <summary>
    /// Converts a nullable <see cref="DateTime"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float? ToOADate(DateTime? value) => value.HasValue ? (float)value.Value.ToOADate() : null;

    /// <summary>
    /// Converts a <see cref="DateTime"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float ToOADate(DateTime value) => (float)value.ToOADate();
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float? ToOADate(DateTimeOffset? value) => value.HasValue ? (float)value.Value.DateTime.ToOADate() : null;

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float ToOADate(DateTimeOffset value) => (float)value.DateTime.ToOADate();
    #endregion

    #region Primitive
    /// <summary>
    /// Parses the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <returns>The parsed <see cref="float"/> value.</returns>
    public static float ParseInvariant(string input)
        => float.Parse(input, CultureInfo.InvariantCulture);

    /// <summary>
    /// Attempts to parse the string representation of a number using <see cref="CultureInfo.InvariantCulture"/>.
    /// Returns <c>null</c> if parsing fails.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <returns>The parsed <see cref="float"/> value, or <c>null</c> if parsing fails.</returns>
    public static float? ParseInvariantOrNull(string? input)
        => float.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result) ? result : null;
    #endregion

    #region TimeSpan
    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalDays(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalDays : null;

    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float TotalDays(TimeSpan value) => (float)value.TotalDays;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalHours(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalHours : null;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float TotalHours(TimeSpan value) => (float)value.TotalHours;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalMilliseconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMilliseconds : null;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float TotalMilliseconds(TimeSpan value) => (float)value.TotalMilliseconds;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalMinutes(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMinutes : null;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float TotalMinutes(TimeSpan value) => (float)value.TotalMinutes;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalSeconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalSeconds : null;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float TotalSeconds(TimeSpan value) => (float)value.TotalSeconds;
    #endregion
}
