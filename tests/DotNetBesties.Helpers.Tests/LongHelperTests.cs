using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class LongHelperTests
{
    [Fact]
    public void Ticks_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2024, 1, 1);
        Assert.Equal(dt.Ticks, LongHelper.Ticks(dt));
    }

    [Fact]
    public void ParseInvariant_ShouldParseString()
    {
        var result = LongHelper.ParseInvariant("42");
        Assert.Equal(42L, result);
    }

    [Fact]
    public void ToUnixTimeMilliseconds_FromDateTimeOffset_ShouldRoundTrip()
    {
        var dto = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var unix = LongHelper.ToUnixTimeMilliseconds(dto);
        var back = DateTimeOffset.FromUnixTimeMilliseconds(unix);
        Assert.Equal(dto, back);
    }

    [Fact]
    public void Ticks_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMinutes(1);
        Assert.Equal(ts.Ticks, LongHelper.Ticks(ts));
    }
}
