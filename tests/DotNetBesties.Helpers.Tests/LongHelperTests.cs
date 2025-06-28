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
}
