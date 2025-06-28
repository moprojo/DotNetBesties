using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class TimeSpanHelperTests
{
    [Test]
    public async Task Multiply_ShouldScaleDuration()
    {
        var ts = TimeSpan.FromMinutes(2);
        var result = TimeSpanHelper.Multiply(ts, 1.5);
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(3));
    }

    [Test]
    public async Task Format_ShouldReturnInvariantString()
    {
        var ts = new TimeSpan(1,2,3);
        var formatted = StringHelper.FromTimeSpan(ts);
        await Assert.That(formatted).IsEqualTo("01:02:03");
    }

    [Test]
    public async Task Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromTimeSpan((TimeSpan?)null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = TimeSpanHelper.ParseExactInvariantOrNull("bad", "c");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Add_ShouldSumDurations()
    {
        var a = TimeSpan.FromMinutes(1);
        var b = TimeSpan.FromMinutes(2);
        var result = TimeSpanHelper.Add(a, b);
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(3));
    }

    [Test]
    public async Task Divide_ShouldDivideDuration()
    {
        var ts = TimeSpan.FromMinutes(3);
        var result = TimeSpanHelper.Divide(ts, 3);
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(1));
    }
}
