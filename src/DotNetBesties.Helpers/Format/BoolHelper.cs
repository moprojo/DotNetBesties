using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="bool"/> results.
/// </summary>
public static class BoolHelper
{
    #region DateOnly
    /// <summary>
    /// Attempts to parse a <see cref="DateOnly"/> using the specified format and invariant culture.
    /// </summary>
    public static bool TryParseExactDateOnlyInvariant(string? input, string format, out DateOnly result)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateOnly"/> using any of the provided formats and invariant culture.
    /// </summary>
    public static bool TryParseExactDateOnlyInvariant(string? input, string[] formats, out DateOnly result)
        => DateOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    #endregion

    #region DateTime
    /// <summary>
    /// Attempts to parse a <see cref="DateTime"/> using the specified format, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeInvariant(string? input, string format, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateTime"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeInvariant(string? input, string[] formats, out DateTime result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Attempts to parse a <see cref="DateTimeOffset"/> using the specified format, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string format, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, styles, out result);

    /// <summary>
    /// Attempts to parse a <see cref="DateTimeOffset"/> using any of the provided formats, invariant culture and styles.
    /// </summary>
    public static bool TryParseExactDateTimeOffsetInvariant(string? input, string[] formats, out DateTimeOffset result, DateTimeStyles styles = DateTimeStyles.None)
        => DateTimeOffset.TryParseExact(input, formats, CultureInfo.InvariantCulture, styles, out result);
    #endregion

    #region Long
    /// <summary>
    /// Attempts to parse an <see cref="long"/> using invariant culture.
    /// </summary>
    public static bool TryParseLongInvariant(string? input, out long result)
        => long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    #endregion

    #region TimeSpan
    /// <summary>
    /// Attempts to parse a <see cref="TimeSpan"/> using the specified format and invariant culture.
    /// </summary>
    public static bool TryParseExactTimeSpanInvariant(string? input, string format, out TimeSpan result)
        => TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out result);

    /// <summary>
    /// Attempts to parse a <see cref="TimeSpan"/> using any of the provided formats and invariant culture.
    /// </summary>
    public static bool TryParseExactTimeSpanInvariant(string? input, string[] formats, out TimeSpan result)
        => TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out result);
    #endregion
}
