using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="float"/> results.
/// </summary>
public static class FloatHelper
{
    #region DateTime
    /// <summary>
    /// Converts a nullable <see cref="DateTime"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float? ToOADate(DateTime? value) => value.HasValue ? (float)value.Value.ToOADate() : (float?)null;

    /// <summary>
    /// Converts a <see cref="DateTime"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float ToOADate(DateTime value) => (float)value.ToOADate();
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float? ToOADate(DateTimeOffset? value) => value.HasValue ? (float)value.Value.DateTime.ToOADate() : (float?)null;

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to its OLE Automation date as a <see cref="float"/>.
    /// </summary>
    public static float ToOADate(DateTimeOffset value) => (float)value.DateTime.ToOADate();
    #endregion

    #region TimeSpan
    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalDays(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalDays : (float?)null;
    public static float TotalDays(TimeSpan value) => (float)value.TotalDays;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalHours(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalHours : (float?)null;
    public static float TotalHours(TimeSpan value) => (float)value.TotalHours;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalMilliseconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMilliseconds : (float?)null;
    public static float TotalMilliseconds(TimeSpan value) => (float)value.TotalMilliseconds;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalMinutes(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalMinutes : (float?)null;
    public static float TotalMinutes(TimeSpan value) => (float)value.TotalMinutes;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static float? TotalSeconds(TimeSpan? value) => value.HasValue ? (float)value.Value.TotalSeconds : (float?)null;
    public static float TotalSeconds(TimeSpan value) => (float)value.TotalSeconds;
    #endregion
}
