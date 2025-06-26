using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class TimeSpanHelperTests
{
    [Fact]
    public void Multiply_ShouldScaleDuration()
    {
        var ts = TimeSpan.FromMinutes(2);
        var result = TimeSpanHelper.Multiply(ts, 1.5);
        Assert.Equal(TimeSpan.FromMinutes(3), result);
    }

    [Fact]
    public void Format_ShouldReturnInvariantString()
    {
        var ts = new TimeSpan(1,2,3);
        var formatted = TimeSpanHelper.Format(ts);
        Assert.Equal("01:02:03", formatted);
    }

    [Fact]
    public void Format_NullableNull_ReturnsNull()
    {
        string? result = TimeSpanHelper.Format((TimeSpan?)null);
        Assert.Null(result);
    }

    [Fact]
    public void ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = TimeSpanHelper.ParseExactInvariantOrNull("bad", "c");
        Assert.Null(result);
    }
}
