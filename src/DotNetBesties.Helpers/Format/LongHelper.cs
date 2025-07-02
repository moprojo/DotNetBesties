using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="long"/> results.
/// </summary>
public static class LongHelper
{
    #region DateTime
    /// <summary>
    /// Retrieves the tick count from a <see cref="DateTime"/> value.
    /// </summary>
    public static long Ticks(DateTime value) => value.Ticks;

    /// <summary>
    /// Converts a <see cref="DateTime"/> to Unix time in milliseconds.
    /// </summary>
    public static long ToUnixTimeMilliseconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeMilliseconds();

    /// <summary>
    /// Converts a <see cref="DateTime"/> to Unix time in seconds.
    /// </summary>
    public static long ToUnixTimeSeconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeSeconds();
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Retrieves the tick count from a <see cref="DateTimeOffset"/> value.
    /// </summary>
    public static long Ticks(DateTimeOffset value) => value.Ticks;

    /// <summary>
    /// Retrieves the tick count from a nullable <see cref="DateTimeOffset"/>.
    /// </summary>
    public static long? Ticks(DateTimeOffset? value) => value?.Ticks;

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to Unix time in milliseconds.
    /// </summary>
    public static long ToUnixTimeMilliseconds(DateTimeOffset value) => value.ToUnixTimeMilliseconds();

    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to Unix time in milliseconds.
    /// </summary>
    public static long? ToUnixTimeMilliseconds(DateTimeOffset? value) => value?.ToUnixTimeMilliseconds();

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to Unix time in seconds.
    /// </summary>
    public static long ToUnixTimeSeconds(DateTimeOffset value) => value.ToUnixTimeSeconds();

    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to Unix time in seconds.
    /// </summary>
    public static long? ToUnixTimeSeconds(DateTimeOffset? value) => value?.ToUnixTimeSeconds();
    #endregion

    #region Primitive
    /// <summary>
    /// Parses a string into a <see cref="long"/> using invariant culture.
    /// </summary>
    public static long ParseInvariant(string input) => long.Parse(input, CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Retrieves the tick count for the specified <see cref="TimeSpan"/>.
    /// </summary>
    public static long Ticks(TimeSpan value) => value.Ticks;
    #endregion
}
