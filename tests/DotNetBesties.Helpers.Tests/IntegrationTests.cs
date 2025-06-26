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
        var dto = DateTimeHelper.ToDateTimeOffset(now);
        var unix = DateTimeOffsetHelper.ToUnixTimeSeconds(dto);
        var backDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unix);
        Assert.Equal(dto, backDto);
    }
}
