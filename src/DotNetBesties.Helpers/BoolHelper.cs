using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="bool"/> results.
/// </summary>
public static class BoolHelper
{
    #region DateTime
    public static bool TryParseExactDateTimeInvariant(string? input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static bool TryParseExactDateTimeInvariant(string? input, string[] formats, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region DateTimeOffset
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region Long
    public static bool TryParseLongInvariant(string? input, out long result)
        => long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    #endregion

    #region TimeSpan
    public static bool TryParseExactTimeSpanInvariant(string? input, string format, out TimeSpan result)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out result);

    public static bool TryParseExactTimeSpanInvariant(string? input, string[] formats, out TimeSpan result)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out result);
    #endregion
}
