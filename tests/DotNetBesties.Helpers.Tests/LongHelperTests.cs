using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class LongHelperTests
{
    [Test]
    public async Task Ticks_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2024, 1, 1);
        await Assert.That(LongHelper.Ticks(dt)).IsEqualTo(dt.Ticks);
    }

    [Test]
    public async Task ParseInvariant_ShouldParseString()
    {
        var result = LongHelper.ParseInvariant("42");
        await Assert.That(result).IsEqualTo(42L);
    }

    [Test]
    public async Task Ticks_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMinutes(5);
        await Assert.That(LongHelper.Ticks(ts)).IsEqualTo(ts.Ticks);
    }

    [Test]
    public async Task ToUnixTimeMilliseconds_FromDateTimeOffset_ShouldRoundTrip()
    {
        var dto = new DateTimeOffset(2024, 8, 1, 0, 0, 0, TimeSpan.Zero);
        var unix = LongHelper.ToUnixTimeMilliseconds(dto);
        var back = DateTimeOffset.FromUnixTimeMilliseconds(unix);
        await Assert.That(back).IsEqualTo(dto);
    }
}
