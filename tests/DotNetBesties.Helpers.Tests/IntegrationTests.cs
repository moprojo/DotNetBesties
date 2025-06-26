using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegrationTests
{
    [Fact]
    public void DateTime_ToDateTimeOffset_ToUnix_RoundTrip()
    {
        var now = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(now);
        var unix = DateTimeOffsetHelper.ToUnixTimeSeconds(dto);
        var backDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unix);
        Assert.Equal(dto, backDto);
    }

    [Fact]
    public void DateOnly_ToDateTimeOffset_WithZone_RoundTrip()
    {
        var date = new DateOnly(2024, 12, 24);
        var time = new TimeOnly(10, 0);
        var zone = TimeZoneInfo.Utc;
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, zone);
        var backDate = DateOnly.FromDateTime(dto.UtcDateTime);
        Assert.Equal(date, backDate);
    }
}
