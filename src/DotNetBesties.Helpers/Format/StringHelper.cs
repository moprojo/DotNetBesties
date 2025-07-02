using System;
using System.Drawing;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Conversion helpers that produce <see cref="string"/> results.
/// </summary>
public static class StringHelper
{
    #region Color
    /// <summary>
    /// Converts RGB values to a Hex string
    /// </summary>
    /// <param name="r">Red component</param>
    /// <param name="g">Green component</param>
    /// <param name="b">Blue component</param>
    public static string RGBToHex(int r, int g, int b)
    {
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    /// <summary>
    /// Converts Color values to a Hex string
    /// </summary>
    public static string ColorToHexString(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
    
    /// <summary>
    /// Converts RGB values to a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Current color</param>
    /// <returns>Rgb color string</returns>
    public static string ColorToRgbString(Color color)
    {
        return $"rgb({color.R}, {color.G}, {color.B})";
    }

    /// <summary>
    /// Converts ARGB values to a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Current color</param>
    /// <returns>Rgb color string</returns>
    public static string ColorToARgbString(Color color)
    {
        return $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
    }
    #endregion

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
