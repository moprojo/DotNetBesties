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
        await Assert.That(IntegerHelper.Year(dt)).IsEqualTo(2025);
    }

    [Test]
    public async Task DayOfYear_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 2, 1);
        await Assert.That(IntegerHelper.DayOfYear(date)).IsEqualTo(date.DayOfYear);
    }

    [Test]
    public async Task Hours_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromHours(5);
        await Assert.That(IntegerHelper.Hours(ts)).IsEqualTo(5);
    }

    [Test]
    public async Task ParseInvariant_ShouldParseString()
    {
        await Assert.That(IntegerHelper.ParseInvariant("10")).IsEqualTo(10);
    }

    [Test]
    public async Task Day_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntegerHelper.Day(date)).IsEqualTo(4);
    }

    [Test]
    public async Task Month_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntegerHelper.Month(date)).IsEqualTo(3);
    }

    [Test]
    public async Task Year_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 3, 4);
        await Assert.That(IntegerHelper.Year(date)).IsEqualTo(2025);
    }

    [Test]
    public async Task Day_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2025, 6, 15, 1, 2, 3);
        await Assert.That(IntegerHelper.Day(dt)).IsEqualTo(15);
    }

    [Test]
    public async Task DayOfYear_FromDateTimeOffset_ReturnsExpected()
    {
        var dto = new DateTimeOffset(2025, 12, 31, 0, 0, 0, TimeSpan.Zero);
        await Assert.That(IntegerHelper.DayOfYear(dto)).IsEqualTo(dto.DayOfYear);
    }

    [Test]
    public async Task Minutes_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMinutes(90);
        await Assert.That(IntegerHelper.Minutes(ts)).IsEqualTo(ts.Minutes);
    }

    [Test]
    public async Task Milliseconds_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromMilliseconds(1234);
        await Assert.That(IntegerHelper.Milliseconds(ts)).IsEqualTo(ts.Milliseconds);
    }

    [Test]
    public async Task Days_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromDays(5);
        await Assert.That(IntegerHelper.Days(ts)).IsEqualTo(5);
    }

    [Test]
    public async Task Seconds_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromSeconds(123);
        await Assert.That(IntegerHelper.Seconds(ts)).IsEqualTo(ts.Seconds);
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
