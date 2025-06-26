using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="float"/> results.
/// </summary>
public static class FloatHelper
{
    #region DateTime
    public static float? ToOADate(DateTime? value) => value.HasValue ? (float)value.Value.ToOADate() : (float?)null;
    public static float ToOADate(DateTime value) => (float)value.ToOADate();
    #endregion

    #region DateTimeOffset
    public static float? ToOADate(DateTimeOffset? value) => value.HasValue ? (float)value.Value.DateTime.ToOADate() : (float?)null;
    public static float ToOADate(DateTimeOffset value) => (float)value.DateTime.ToOADate();
    #endregion

    #region TimeSpan
    public static float? TotalDays(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalDays : (float?)null;
    public static float TotalDays(TimeSpan value) => (float)value.TotalDays;

    public static float? TotalHours(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalHours : (float?)null;
    public static float TotalHours(TimeSpan value) => (float)value.TotalHours;

    public static float? TotalMilliseconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMilliseconds : (float?)null;
    public static float TotalMilliseconds(TimeSpan value) => (float)value.TotalMilliseconds;

    public static float? TotalMinutes(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMinutes : (float?)null;
    public static float TotalMinutes(TimeSpan value) => (float)value.TotalMinutes;

    public static float? TotalSeconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalSeconds : (float?)null;
    public static float TotalSeconds(TimeSpan value) => (float)value.TotalSeconds;
    #endregion
}
