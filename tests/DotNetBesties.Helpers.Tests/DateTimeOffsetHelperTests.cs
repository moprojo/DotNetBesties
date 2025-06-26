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
        var seconds = LongHelper.ToUnixTimeSeconds(dto);
        var fromSeconds = DateTimeOffsetHelper.FromUnixTimeSeconds(seconds);
        Assert.Equal(dto, fromSeconds);
    }

    [Fact]
    public void Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateTimeOffset((DateTimeOffset?)null);
        Assert.Null(result);
    }

    [Fact]
    public void ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull("bad", "O");
        Assert.Null(result);
    }

    [Fact]
    public void FromDateTime_ShouldReturnDateTimeOffset()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(dt);
        Assert.Equal(new DateTimeOffset(dt), dto);
    }

    [Fact]
    public void FromDateOnly_WithOffset_ReturnsDto()
    {
        var date = new DateOnly(2024, 2, 3);
        var time = new TimeOnly(4, 5, 6);
        var offset = TimeSpan.FromHours(1);
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, offset);
        Assert.Equal(new DateTimeOffset(date.ToDateTime(time), offset), dto);
    }
}
