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

    [Fact]
    public void ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = DoubleHelper.ToOADate(dto);
        var back = DateTime.FromOADate(oa);
        Assert.Equal(dto.DateTime, back);
    }

    [Fact]
    public void TotalSeconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromSeconds(90);
        var result = DoubleHelper.TotalSeconds(ts);
        Assert.Equal(ts.TotalSeconds, result);
    }
}
