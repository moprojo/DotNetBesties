using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegerHelperTests
{
    [Test]
    public async Task IsoWeek_ShouldReturnExpectedWeek()
    {
        var date = new DateTime(2025, 1, 1); // 1 Jan 2025 is week 1
        await Assert.That(IntegerHelper.IsoWeek(date)).IsEqualTo(1);
    }

    [Test]
    public async Task IsIsoWeek_ShouldMatchIsoWeekOfYear()
    {
        var date = new DateTime(2025, 12, 31);
        var expected = System.Globalization.ISOWeek.GetWeekOfYear(date);
        await Assert.That(IntegerHelper.IsoWeek(date)).IsEqualTo(expected);
    }
}
