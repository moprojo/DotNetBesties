using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DateTimeOffsetHelperTests
{
    [Fact]
    public void UnixConversion_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero);
        var seconds = DateTimeOffsetHelper.ToUnixTimeSeconds(dto);
        var fromSeconds = DateTimeOffsetHelper.FromUnixTimeSeconds(seconds);
        Assert.Equal(dto, fromSeconds);
    }
}
