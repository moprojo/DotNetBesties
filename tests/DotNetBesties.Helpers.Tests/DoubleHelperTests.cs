using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DoubleHelperTests
{
    [Fact]
    public void ToOADate_RoundTrip()
    {
        var dt = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc);
        var oa = DoubleHelper.ToOADate(dt);
        var back = DateTime.FromOADate(oa);
        Assert.Equal(dt, DateTime.SpecifyKind(back, DateTimeKind.Utc));
    }

    [Fact]
    public void TotalMinutes_ShouldReturnExpected()
    {
        var ts = new TimeSpan(1, 2, 0);
        var result = DoubleHelper.TotalMinutes(ts);
        Assert.Equal(ts.TotalMinutes, result);
    }
}
