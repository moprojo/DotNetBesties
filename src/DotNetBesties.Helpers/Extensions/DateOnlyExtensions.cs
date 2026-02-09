using System;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="DateOnly"/> operations.
/// </summary>
public static class DateOnlyExtensions
{
    #region Date Manipulation

    /// <summary>
    /// Adds the specified number of days to this <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="days">The number of days to add.</param>
    /// <returns>A new <see cref="DateOnly"/> with the specified days added.</returns>
    public static DateOnly AddDays(this DateOnly value, int days)
        => DateOnlyHelper.AddDays(value, days);

    /// <summary>
    /// Adds the specified number of months to this <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="months">The number of months to add.</param>
    /// <returns>A new <see cref="DateOnly"/> with the specified months added.</returns>
    public static DateOnly AddMonths(this DateOnly value, int months)
        => DateOnlyHelper.AddMonths(value, months);

    /// <summary>
    /// Adds the specified number of years to this <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="years">The number of years to add.</param>
    /// <returns>A new <see cref="DateOnly"/> with the specified years added.</returns>
    public static DateOnly AddYears(this DateOnly value, int years)
        => DateOnlyHelper.AddYears(value, years);

    #endregion

    #region Conversion

    /// <summary>
    /// Converts this <see cref="DateOnly"/> to a <see cref="DateTime"/> using the specified <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="time">The time component.</param>
    /// <returns>A <see cref="DateTime"/> combining the date and time.</returns>
    public static DateTime ToDateTime(this DateOnly value, TimeOnly time)
        => DateOnlyHelper.ToDateTime(value, time);

    /// <summary>
    /// Converts this <see cref="DateOnly"/> to a <see cref="DateTime"/> using the specified <see cref="TimeOnly"/> and <see cref="DateTimeKind"/>.
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <param name="time">The time component.</param>
    /// <param name="kind">The kind of date and time.</param>
    /// <returns>A <see cref="DateTime"/> combining the date and time.</returns>
    public static DateTime ToDateTimeWithKind(this DateOnly value, TimeOnly time, DateTimeKind kind)
        => DateOnlyHelper.ToDateTime(value, time, kind);

    #endregion

    #region String Formatting

    /// <summary>
    /// Formats the DateOnly using the specified format and provider.
    /// </summary>
    /// <param name="value">The DateOnly value.</param>
    /// <param name="format">The format string. Default is "yyyy-MM-dd".</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation.</returns>
    public static string ToInvariantString(this DateOnly value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => StringHelper.FromDateOnly(value, format, provider);

    /// <summary>
    /// Formats the nullable DateOnly using the specified format and provider.
    /// </summary>
    /// <param name="value">The nullable DateOnly value.</param>
    /// <param name="format">The format string. Default is "yyyy-MM-dd".</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation or null.</returns>
    public static string? ToInvariantString(this DateOnly? value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => StringHelper.FromDateOnly(value, format, provider);

    #endregion

    #region Unix Time

    /// <summary>
    /// Converts this <see cref="DateOnly"/> to Unix time in seconds (UTC).
    /// </summary>
    /// <param name="value">The date value.</param>
    /// <returns>The number of seconds since Unix epoch.</returns>
    public static long ToUnixTimeSeconds(this DateOnly value)
        => DateOnlyHelper.ToUnixTimeSeconds(value);

    /// <summary>
    /// Converts a nullable <see cref="DateOnly"/> to Unix time in seconds (UTC).
    /// </summary>
    /// <param name="value">The nullable date value.</param>
    /// <returns>The number of seconds since Unix epoch, or <c>null</c> if the value is <c>null</c>.</returns>
    public static long? ToUnixTimeSeconds(this DateOnly? value)
        => DateOnlyHelper.ToUnixTimeSeconds(value);

    #endregion
}
