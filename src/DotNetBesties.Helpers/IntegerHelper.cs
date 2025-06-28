using System;
using System.Globalization;

namespace DotNetBesties.Helpers;

/// <summary>
/// Helpers for operations that produce <see cref="int"/> results.
/// </summary>
public static class IntegerHelper
{
    #region DateTime    
    public static int IsoWeek(DateTime value)
        => ISOWeek.GetWeekOfYear(value);
    #endregion
}
