using System;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="long"/> results.
/// </summary>
public static class LongHelper
{
    #region DateTime
    public static long ToUnixTimeSeconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeSeconds();
    public static long ToUnixTimeMilliseconds(DateTime value) => new DateTimeOffset(value).ToUnixTimeMilliseconds();
    #endregion

    #region DateTimeOffset
    public static long ToUnixTimeSeconds(DateTimeOffset value) => value.ToUnixTimeSeconds();
    public static long? ToUnixTimeSeconds(DateTimeOffset? value) => value?.ToUnixTimeSeconds();
    public static long ToUnixTimeMilliseconds(DateTimeOffset value) => value.ToUnixTimeMilliseconds();
    public static long? ToUnixTimeMilliseconds(DateTimeOffset? value) => value?.ToUnixTimeMilliseconds();
    #endregion
}
