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
}
