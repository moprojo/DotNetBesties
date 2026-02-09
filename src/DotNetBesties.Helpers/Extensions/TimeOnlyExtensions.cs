using System;
using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="TimeOnly"/> operations.
/// </summary>
public static class TimeOnlyExtensions
{
    #region Time Manipulation

    /// <summary>
    /// Adds the specified number of hours to this <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="value">The time value.</param>
    /// <param name="hours">The number of hours to add.</param>
    /// <returns>A new <see cref="TimeOnly"/> with the specified hours added.</returns>
    public static TimeOnly AddHours(this TimeOnly value, int hours)
        => TimeOnlyHelper.AddHours(value, hours);

    /// <summary>
    /// Adds the specified number of minutes to this <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="value">The time value.</param>
    /// <param name="minutes">The number of minutes to add.</param>
    /// <returns>A new <see cref="TimeOnly"/> with the specified minutes added.</returns>
    public static TimeOnly AddMinutes(this TimeOnly value, int minutes)
        => TimeOnlyHelper.AddMinutes(value, minutes);

    #endregion

    #region Conversion

    /// <summary>
    /// Converts this <see cref="TimeOnly"/> to a <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="value">The time value.</param>
    /// <returns>A <see cref="TimeSpan"/> representing the time duration since midnight.</returns>
    public static TimeSpan ToTimeSpan(this TimeOnly value)
        => TimeOnlyHelper.ToTimeSpan(value);

    /// <summary>
    /// Converts a nullable <see cref="TimeOnly"/> to a nullable <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="value">The nullable time value.</param>
    /// <returns>A nullable <see cref="TimeSpan"/>, or <c>null</c> if the value is <c>null</c>.</returns>
    public static TimeSpan? ToTimeSpan(this TimeOnly? value)
        => TimeOnlyHelper.ToTimeSpan(value);

    #endregion

    #region String Formatting

    /// <summary>
    /// Formats the TimeOnly using the specified format and provider.
    /// </summary>
    /// <param name="value">The TimeOnly value.</param>
    /// <param name="format">The format string. Default is "HH:mm:ss".</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation.</returns>
    public static string ToInvariantString(this TimeOnly value, string format = "HH:mm:ss", IFormatProvider? provider = null)
        => StringHelper.FromTimeOnly(value, format, provider);

    /// <summary>
    /// Formats the nullable TimeOnly using the specified format and provider.
    /// </summary>
    /// <param name="value">The nullable TimeOnly value.</param>
    /// <param name="format">The format string. Default is "HH:mm:ss".</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation or null.</returns>
    public static string? ToInvariantString(this TimeOnly? value, string format = "HH:mm:ss", IFormatProvider? provider = null)
        => StringHelper.FromTimeOnly(value, format, provider);

    #endregion
}
