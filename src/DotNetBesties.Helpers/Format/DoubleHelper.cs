using System;
using System.Globalization;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helpers for operations that produce <see cref="double"/> results.
/// </summary>
public static class DoubleHelper
{
    #region DateTime
    /// <summary>
    /// Converts a nullable <see cref="DateTime"/> to its OLE Automation date representation.
    /// </summary>
    public static double? ToOADate(DateTime? value) => value?.ToOADate();

    /// <summary>
    /// Converts a <see cref="DateTime"/> to its OLE Automation date representation.
    /// </summary>
    public static double ToOADate(DateTime value) => value.ToOADate();
    #endregion

    #region DateTimeOffset
    /// <summary>
    /// Converts a nullable <see cref="DateTimeOffset"/> to an OLE Automation date.
    /// </summary>
    public static double? ToOADate(DateTimeOffset? value) => value?.DateTime.ToOADate();

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to an OLE Automation date.
    /// </summary>
    public static double ToOADate(DateTimeOffset value) => value.DateTime.ToOADate();
    #endregion

    #region TimeSpan
    /// <summary>
    /// Gets the total number of days represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalDays(TimeSpan? value) => value?.TotalDays;
    public static double TotalDays(TimeSpan value) => value.TotalDays;

    /// <summary>
    /// Gets the total number of hours represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalHours(TimeSpan? value) => value?.TotalHours;
    public static double TotalHours(TimeSpan value) => value.TotalHours;

    /// <summary>
    /// Gets the total number of milliseconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalMilliseconds(TimeSpan? value) => value?.TotalMilliseconds;
    public static double TotalMilliseconds(TimeSpan value) => value.TotalMilliseconds;

    /// <summary>
    /// Gets the total number of minutes represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalMinutes(TimeSpan? value) => value?.TotalMinutes;
    public static double TotalMinutes(TimeSpan value) => value.TotalMinutes;

    /// <summary>
    /// Gets the total number of seconds represented by the <see cref="TimeSpan"/>.
    /// </summary>
    public static double? TotalSeconds(TimeSpan? value) => value?.TotalSeconds;
    public static double TotalSeconds(TimeSpan value) => value.TotalSeconds;
    #endregion
}
