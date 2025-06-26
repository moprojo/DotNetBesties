using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="long"/> results.
/// </summary>
public static class LongHelper
{
    #region DateTime
    public static long Ticks(DateTime value) => value.Ticks;
    public static long ToUnixTimeMilliseconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeMilliseconds();
    public static long ToUnixTimeSeconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeSeconds();
    #endregion

    #region DateTimeOffset
    public static long Ticks(DateTimeOffset value) => value.Ticks;
    public static long? Ticks(DateTimeOffset? value) => value?.Ticks;
    public static long ToUnixTimeMilliseconds(DateTimeOffset value) => value.ToUnixTimeMilliseconds();
    public static long? ToUnixTimeMilliseconds(DateTimeOffset? value) => value?.ToUnixTimeMilliseconds();
    public static long ToUnixTimeSeconds(DateTimeOffset value) => value.ToUnixTimeSeconds();
    public static long? ToUnixTimeSeconds(DateTimeOffset? value) => value?.ToUnixTimeSeconds();
    #endregion

    #region Primitive
    public static long ParseInvariant(string input) => long.Parse(input, CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    public static long Ticks(TimeSpan value) => value.Ticks;
    #endregion
}
