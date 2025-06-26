using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Conversion helpers that produce <see cref="string"/> results.
/// </summary>
public static class StringHelper
{
    #region DateOnly
    /// <summary>
    /// Formats a nullable <see cref="DateOnly"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateOnly(DateOnly? value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateOnly"/> using the specified format and provider.
    /// </summary>
    public static string FromDateOnly(DateOnly value, string format = "yyyy-MM-dd", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateTime
    /// <summary>
    /// Formats a nullable <see cref="DateTime"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateTime(DateTime? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateTime"/> using the specified format and provider.
    /// </summary>
    public static string FromDateTime(DateTime value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Formats a nullable <see cref="DateTimeOffset"/> using the specified format and provider.
    /// </summary>
    public static string? FromDateTimeOffset(DateTimeOffset? value, string format = "O", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="DateTimeOffset"/> using the specified format and provider.
    /// </summary>
    public static string FromDateTimeOffset(DateTimeOffset value, string format = "O", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Formats a nullable <see cref="TimeSpan"/> using the specified format and provider.
    /// </summary>
    public static string? FromTimeSpan(TimeSpan? value, string format = "c", IFormatProvider? provider = null)
        => value?.ToString(format, provider ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Formats a <see cref="TimeSpan"/> using the specified format and provider.
    /// </summary>
    public static string FromTimeSpan(TimeSpan value, string format = "c", IFormatProvider? provider = null)
        => value.ToString(format, provider ?? CultureInfo.InvariantCulture);
    #endregion
}
