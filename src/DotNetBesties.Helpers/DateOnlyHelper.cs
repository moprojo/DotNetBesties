using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="DateOnly"/> values.
/// </summary>
public static class DateOnlyHelper
{
    #region DateOnly

    public static DateOnly AddMonths(DateOnly value, int months)
        => value.AddMonths(months);

    public static DateOnly AddYears(DateOnly value, int years)
        => value.AddYears(years);

    public static DateOnly FromDateTime(DateTime dateTime)
        => DateOnly.FromDateTime(dateTime);

    public static DateOnly FromDateTimeOffset(DateTimeOffset value)
        => DateOnly.FromDateTime(value.DateTime);

    public static DateOnly FromUnixTimeSeconds(long seconds)
        => DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime);

    public static DateOnly? FromUnixTimeSeconds(long? seconds)
        => seconds.HasValue ? FromUnixTimeSeconds(seconds.Value) : (DateOnly?)null;

    public static DateOnly ParseExactInvariant(string input, string format)
        => DateOnly.ParseExact(input, format, CultureInfo.InvariantCulture);

    public static DateOnly? ParseExactInvariantOrNull(string? input, string format)
        => DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : (DateOnly?)null;

    public static DateOnly? ParseExactInvariantOrNull(string? input, string[] formats)
        => DateOnly.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : (DateOnly?)null;
    #endregion
}
