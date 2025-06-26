using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Utility methods for working with <see cref="TimeSpan"/> values.
/// </summary>
public static class IntegerHelper
{
    #region DateTime    
    public static int IsoWeek(DateTime value)
        => ISOWeek.GetWeekOfYear(value);
    #endregion
}
