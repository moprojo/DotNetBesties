using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="double"/> results.
/// </summary>
public static class DoubleHelper
{
    #region DateTime
    public static double? ToOADate(DateTime? value) => value?.ToOADate();
    public static double ToOADate(DateTime value) => value.ToOADate();
    #endregion

    #region DateTimeOffset
    public static double? ToOADate(DateTimeOffset? value) => value?.DateTime.ToOADate();
    public static double ToOADate(DateTimeOffset value) => value.DateTime.ToOADate();
    #endregion

    #region TimeSpan
    public static double? TotalDays(TimeSpan? value) => value?.TotalDays;
    public static double TotalDays(TimeSpan value) => value.TotalDays;

    public static double? TotalHours(TimeSpan? value) => value?.TotalHours;
    public static double TotalHours(TimeSpan value) => value.TotalHours;

    public static double? TotalMilliseconds(TimeSpan? value) => value?.TotalMilliseconds;
    public static double TotalMilliseconds(TimeSpan value) => value.TotalMilliseconds;

    public static double? TotalMinutes(TimeSpan? value) => value?.TotalMinutes;
    public static double TotalMinutes(TimeSpan value) => value.TotalMinutes;

    public static double? TotalSeconds(TimeSpan? value) => value?.TotalSeconds;
    public static double TotalSeconds(TimeSpan value) => value.TotalSeconds;
    #endregion
}
