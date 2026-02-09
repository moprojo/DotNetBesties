using System;
using System.Globalization;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="long"/> operations.
/// </summary>
public static class LongExtensions
{
    #region Unix Time Conversion

    /// <summary>
    /// Converts Unix milliseconds to a <see cref="DateTime"/> in UTC.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds since Unix epoch.</param>
    /// <returns>A <see cref="DateTime"/> in UTC.</returns>
    public static DateTime FromUnixTimeMilliseconds(this long milliseconds)
        => DateTimeHelper.FromUnixTimeMilliseconds(milliseconds);

    /// <summary>
    /// Converts nullable Unix milliseconds to a nullable <see cref="DateTime"/> in UTC.
    /// </summary>
    /// <param name="milliseconds">The nullable number of milliseconds since Unix epoch.</param>
    /// <returns>A nullable <see cref="DateTime"/> in UTC.</returns>
    public static DateTime? FromUnixTimeMilliseconds(this long? milliseconds)
        => DateTimeHelper.FromUnixTimeMilliseconds(milliseconds);

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateTime"/> in UTC.
    /// </summary>
    /// <param name="seconds">The number of seconds since Unix epoch.</param>
    /// <returns>A <see cref="DateTime"/> in UTC.</returns>
    public static DateTime FromUnixTimeSeconds(this long seconds)
        => DateTimeHelper.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateTime"/> in UTC.
    /// </summary>
    /// <param name="seconds">The nullable number of seconds since Unix epoch.</param>
    /// <returns>A nullable <see cref="DateTime"/> in UTC.</returns>
    public static DateTime? FromUnixTimeSeconds(this long? seconds)
        => DateTimeHelper.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts Unix milliseconds to a <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds since Unix epoch.</param>
    /// <returns>A <see cref="DateTimeOffset"/> in UTC.</returns>
    public static DateTimeOffset FromUnixTimeMillisecondsToDateTimeOffset(this long milliseconds)
        => DateTimeOffsetHelper.FromUnixTimeMilliseconds(milliseconds);

    /// <summary>
    /// Converts nullable Unix milliseconds to a nullable <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    /// <param name="milliseconds">The nullable number of milliseconds since Unix epoch.</param>
    /// <returns>A nullable <see cref="DateTimeOffset"/> in UTC.</returns>
    public static DateTimeOffset? FromUnixTimeMillisecondsToDateTimeOffset(this long? milliseconds)
        => DateTimeOffsetHelper.FromUnixTimeMilliseconds(milliseconds);

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    /// <param name="seconds">The number of seconds since Unix epoch.</param>
    /// <returns>A <see cref="DateTimeOffset"/> in UTC.</returns>
    public static DateTimeOffset FromUnixTimeSecondsToDateTimeOffset(this long seconds)
        => DateTimeOffsetHelper.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateTimeOffset"/> in UTC.
    /// </summary>
    /// <param name="seconds">The nullable number of seconds since Unix epoch.</param>
    /// <returns>A nullable <see cref="DateTimeOffset"/> in UTC.</returns>
    public static DateTimeOffset? FromUnixTimeSecondsToDateTimeOffset(this long? seconds)
        => DateTimeOffsetHelper.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts Unix seconds to a <see cref="DateOnly"/> in UTC.
    /// </summary>
    /// <param name="seconds">The number of seconds since Unix epoch.</param>
    /// <returns>A <see cref="DateOnly"/> in UTC.</returns>
    public static DateOnly FromUnixTimeSecondsToDateOnly(this long seconds)
        => DateOnlyHelper.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts nullable Unix seconds to a nullable <see cref="DateOnly"/> in UTC.
    /// </summary>
    /// <param name="seconds">The nullable number of seconds since Unix epoch.</param>
    /// <returns>A nullable <see cref="DateOnly"/> in UTC.</returns>
    public static DateOnly? FromUnixTimeSecondsToDateOnly(this long? seconds)
        => DateOnlyHelper.FromUnixTimeSeconds(seconds);

    #endregion
}
