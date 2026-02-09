using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class TimeOnlyHelperTests
{
    [Test]
    public async Task ParseExactInvariant_ShouldReturnExpectedTime()
    {
        var time = TimeOnlyHelper.ParseExactInvariant("14:30:00", "HH:mm:ss");
        await Assert.That(time).IsEqualTo(new TimeOnly(14, 30, 0));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = TimeOnlyHelper.ParseExactInvariantOrNull("bad", "HH:mm:ss");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_MultipleFormats_ShouldReturnExpected()
    {
        var formats = new[] { "HH:mm:ss", "hh:mm tt" };
        var result = TimeOnlyHelper.ParseExactInvariantOrNull("02:30 PM", formats);
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30));
    }

    [Test]
    public async Task FromDateTime_ShouldReturnTimeOnly()
    {
        var dt = new DateTime(2024, 5, 1, 14, 30, 0);
        var time = TimeOnlyHelper.FromDateTime(dt);
        await Assert.That(time).IsEqualTo(new TimeOnly(14, 30, 0));
    }

    [Test]
    public async Task FromDateTimeOffset_ShouldReturnTimeOnly()
    {
        var dto = new DateTimeOffset(2024, 5, 1, 14, 30, 0, TimeSpan.Zero);
        var time = TimeOnlyHelper.FromDateTimeOffset(dto);
        await Assert.That(time).IsEqualTo(new TimeOnly(14, 30, 0));
    }

    [Test]
    public async Task FromTimeSpan_ShouldReturnTimeOnly()
    {
        var ts = new TimeSpan(14, 30, 0);
        var time = TimeOnlyHelper.FromTimeSpan(ts);
        await Assert.That(time).IsEqualTo(new TimeOnly(14, 30, 0));
    }

    [Test]
    public async Task AddHours_ShouldAddCorrectly()
    {
        var time = new TimeOnly(10, 0);
        var result = TimeOnlyHelper.AddHours(time, 2);
        await Assert.That(result).IsEqualTo(new TimeOnly(12, 0));
    }

    [Test]
    public async Task AddMinutes_ShouldAddCorrectly()
    {
        var time = new TimeOnly(10, 0);
        var result = TimeOnlyHelper.AddMinutes(time, 30);
        await Assert.That(result).IsEqualTo(new TimeOnly(10, 30));
    }
}
