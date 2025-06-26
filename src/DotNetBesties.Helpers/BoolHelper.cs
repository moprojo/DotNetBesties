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

    #region DateOnly
    public static bool TryParseExactDateOnlyInvariant(string? input, string format, out DateOnly result)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    public static bool TryParseExactDateOnlyInvariant(string? input, string[] formats, out DateOnly result)
        => DateOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    #endregion

    #region DateTimeOffset
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region TimeSpan
    public static bool TryParseExactTimeSpanInvariant(string? input, string format, out TimeSpan result)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out result);

    public static bool TryParseExactTimeSpanInvariant(string? input, string[] formats, out TimeSpan result)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out result);
    #endregion
}
