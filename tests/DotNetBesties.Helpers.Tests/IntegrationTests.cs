using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegrationTests
{
    [Test]
    public async Task DateTime_ToDateTimeOffset_ToUnix_RoundTrip()
    {
        var now = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(now);
        var unix = LongHelper.ToUnixTimeSeconds(dto);
        var backDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unix);
        await Assert.That(backDto).IsEqualTo(dto);
    }

    [Test]
    public async Task DateOnly_ToDateTimeOffset_WithZone_RoundTrip()
    {
        var date = new DateOnly(2024, 12, 24);
        var time = new TimeOnly(10, 0);
        var zone = TimeZoneInfo.Utc;
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, zone);
        var backDate = DateOnly.FromDateTime(dto.UtcDateTime);
        await Assert.That(backDate).IsEqualTo(date);
    }
}
