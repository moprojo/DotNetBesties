using System;
using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="DateTimeOffset"/> operations.
/// </summary>
public static class DateTimeOffsetExtensions
{
    #region TimeZone Conversion

    /// <summary>
    /// Converts this <see cref="DateTimeOffset"/> value to the specified <see cref="TimeZoneInfo"/>.
    /// </summary>
    /// <param name="value">The date and time offset value.</param>
    /// <param name="destination">The destination time zone.</param>
    /// <returns>A <see cref="DateTimeOffset"/> converted to the destination time zone.</returns>
    public static DateTimeOffset ConvertTime(this DateTimeOffset value, TimeZoneInfo destination)
        => DateTimeOffsetHelper.ConvertTime(value, destination);

    /// <summary>
    /// Adjusts this <see cref="DateTimeOffset"/> to the specified offset while keeping the same UTC time.
    /// </summary>
    /// <param name="value">The date and time offset value.</param>
    /// <param name="offset">The target offset.</param>
    /// <returns>A <see cref="DateTimeOffset"/> with the specified offset.</returns>
    public static DateTimeOffset ToOffset(this DateTimeOffset value, TimeSpan offset)
        => DateTimeOffsetHelper.ToOffset(value, offset);

    #endregion

    #region Conversion

    /// <summary>
    /// Converts the DateTimeOffset to its OLE Automation date representation.
    /// </summary>
    /// <param name="value">The DateTimeOffset to convert.</param>
    /// <returns>The OLE Automation date equivalent.</returns>
    public static double ToOADate(this DateTimeOffset value)
        => DoubleHelper.ToOADate(value);

    /// <summary>
    /// Converts the nullable DateTimeOffset to its OLE Automation date representation.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset to convert.</param>
    /// <returns>The OLE Automation date equivalent or null.</returns>
    public static double? ToOADate(this DateTimeOffset? value)
        => DoubleHelper.ToOADate(value);

    #endregion

    #region String Formatting

    /// <summary>
    /// Formats the DateTimeOffset using the specified format and provider.
    /// </summary>
    /// <param name="value">The DateTimeOffset value.</param>
    /// <param name="format">The format string. Default is "O" (round-trip format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation.</returns>
    public static string ToInvariantString(this DateTimeOffset value, string format = "O", IFormatProvider? provider = null)
        => StringHelper.FromDateTimeOffset(value, format, provider);

    /// <summary>
    /// Formats the nullable DateTimeOffset using the specified format and provider.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value.</param>
    /// <param name="format">The format string. Default is "O" (round-trip format).</param>
    /// <param name="provider">The format provider. Default is InvariantCulture.</param>
    /// <returns>The formatted string representation or null.</returns>
    public static string? ToInvariantString(this DateTimeOffset? value, string format = "O", IFormatProvider? provider = null)
        => StringHelper.FromDateTimeOffset(value, format, provider);

    #endregion

    #region Unix Time

    /// <summary>
    /// Converts this <see cref="DateTimeOffset"/> to Unix time in milliseconds.
    /// </summary>
    /// <param name="value">The date and time offset value.</param>
    /// <returns>The number of milliseconds since Unix epoch.</returns>
    public static long ToUnixTimeMilliseconds(this DateTimeOffset value)
        => LongHelper.ToUnixTimeMilliseconds(value);

    /// <summary>
    /// Converts this <see cref="DateTimeOffset"/> to Unix time in seconds.
    /// </summary>
    /// <param name="value">The date and time offset value.</param>
    /// <returns>The number of seconds since Unix epoch.</returns>
    public static long ToUnixTimeSeconds(this DateTimeOffset value)
        => LongHelper.ToUnixTimeSeconds(value);

    /// <summary>
    /// Converts the nullable DateTimeOffset to Unix time in milliseconds.
    /// </summary>
    /// <param name="value">The nullable date and time offset value.</param>
    /// <returns>The number of milliseconds since Unix epoch or null.</returns>
    public static long? ToUnixTimeMilliseconds(this DateTimeOffset? value)
        => LongHelper.ToUnixTimeMilliseconds(value);

    /// <summary>
    /// Converts the nullable DateTimeOffset to Unix time in seconds.
    /// </summary>
    /// <param name="value">The nullable date and time offset value.</param>
    /// <returns>The number of seconds since Unix epoch or null.</returns>
    public static long? ToUnixTimeSeconds(this DateTimeOffset? value)
        => LongHelper.ToUnixTimeSeconds(value);

    #endregion
}
