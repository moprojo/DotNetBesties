using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegerHelperTests
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

    [Test]
    public async Task Day_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntHelper.Day(date)).IsEqualTo(4);
    }

    [Test]
    public async Task Month_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntHelper.Month(date)).IsEqualTo(3);
    }

    [Test]
    public async Task Year_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntHelper.Year(date)).IsEqualTo(2025);
    }

    [Test]
    public async Task Day_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2025, 6, 15, 1, 2, 3);
        await Assert.That(IntHelper.Day(dt)).IsEqualTo(15);
    }

    [Test]
    public async Task DayOfYear_FromDateTimeOffset_ReturnsExpected()
    {
        var dto = new DateTimeOffset(2025, 12, 31, 0, 0, 0, TimeSpan.Zero);
        await Assert.That(IntHelper.DayOfYear(dto)).IsEqualTo(dto.DayOfYear);
    }

    [Test]
    public async Task Minutes_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMinutes(90);
        await Assert.That(IntHelper.Minutes(ts)).IsEqualTo(ts.Minutes);
    }

    [Test]
    public async Task Milliseconds_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMilliseconds(1234);
        await Assert.That(IntHelper.Milliseconds(ts)).IsEqualTo(ts.Milliseconds);
    }

    [Test]
    public async Task IsIsoWeek_ShouldMatchIsoWeekOfYear()
    {
        var date = new DateTime(2025, 1, 1);
        var isoWeek = IntegerHelper.IsoWeek(date);
        await Assert.That(isoWeek).IsEqualTo(System.Globalization.ISOWeek.GetWeekOfYear(date));
    }

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
