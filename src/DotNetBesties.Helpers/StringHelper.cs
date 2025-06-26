using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Conversion helpers that produce <see cref="string"/> results.
/// </summary>
public static class StringHelper
{
    #region DateTime
    public static string? FromDateTime(DateTime? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string FromDateTime(DateTime value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateOnly
    public static string? FromDateOnly(DateOnly? value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string FromDateOnly(DateOnly value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateTimeOffset
    public static string? FromDateTimeOffset(DateTimeOffset? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string FromDateTimeOffset(DateTimeOffset value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    public static string? FromTimeSpan(TimeSpan? value, string format = "c", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    public static string FromTimeSpan(TimeSpan value, string format = "c", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion
}
