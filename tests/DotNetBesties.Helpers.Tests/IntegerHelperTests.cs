using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegerHelperTests
{
    [Fact]
    public void IsoWeek_ShouldMatchIsoWeekOfYear()
    {
        var date = new DateTime(2024, 1, 4);
        Assert.Equal(System.Globalization.ISOWeek.GetWeekOfYear(date), IntegerHelper.IsoWeek(date));
    }
}
