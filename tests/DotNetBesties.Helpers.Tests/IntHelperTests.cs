using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntHelperTests
{
    [Test]
    public async Task Year_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2025, 6, 15);
        await Assert.That(IntHelper.Year(dt)).IsEqualTo(2025);
    }

    [Test]
    public async Task DayOfYear_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 2, 1);
        await Assert.That(IntHelper.DayOfYear(date)).IsEqualTo(date.DayOfYear);
    }

    [Test]
    public async Task Hours_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromHours(5);
        await Assert.That(IntHelper.Hours(ts)).IsEqualTo(5);
    }

    [Test]
    public async Task ParseInvariant_ShouldParseString()
    {
        await Assert.That(IntHelper.ParseInvariant("10")).IsEqualTo(10);
    }
}
