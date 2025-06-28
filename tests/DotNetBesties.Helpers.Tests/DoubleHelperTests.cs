using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DoubleHelperTests
{
    [Test]
    public async Task ToOADate_RoundTrip()
    {
        var dt = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc);
        var oa = DoubleHelper.ToOADate(dt);
        var back = DateTime.FromOADate(oa);
        await Assert.That(DateTime.SpecifyKind(back, DateTimeKind.Utc)).IsEqualTo(dt);
    }

    [Test]
    public async Task TotalMinutes_ShouldReturnExpected()
    {
        var ts = new TimeSpan(1, 2, 0);
        var result = DoubleHelper.TotalMinutes(ts);
        await Assert.That(result).IsEqualTo(ts.TotalMinutes);
    }

    [Test]
    public async Task ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = DoubleHelper.ToOADate(dto);
        var back = new DateTimeOffset(DateTime.FromOADate(oa));
        await Assert.That(back.UtcDateTime).IsEqualTo(dto.UtcDateTime);
    }

    [Test]
    public async Task TotalSeconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromSeconds(123.45);
        var result = DoubleHelper.TotalSeconds(ts);
        await Assert.That(result).IsEqualTo(ts.TotalSeconds);
    }
}
