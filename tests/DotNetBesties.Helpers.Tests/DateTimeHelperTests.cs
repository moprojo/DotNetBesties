using System;
using System.Globalization;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DateTimeHelperTests
{
    [Fact]
    public void Format_RoundTrip_ShouldMatchParse()
    {
        var now = new DateTime(2024, 4, 5, 10, 20, 30, DateTimeKind.Utc);
        var formatted = StringHelper.FromDateTime(now);
        var parsed = DateTimeHelper.ParseExactInvariant(formatted, "O", DateTimeStyles.RoundtripKind);
        Assert.Equal(now, parsed);
    }

    [Fact]
    public void TryParseExactInvariant_Invalid_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeInvariant("invalid", "O", out var _);
        Assert.False(ok);
    }

    [Fact]
    public void Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateTime((DateTime?)null);
        Assert.Null(result);
    }

    [Fact]
    public void ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateTimeHelper.ParseExactInvariantOrNull("bad", "O");
        Assert.Null(result);
    }

    [Fact]
    public void UnixTimeConversions_ShouldRoundTrip()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var unix = LongHelper.ToUnixTimeSeconds(dt);
        var back = DateTimeHelper.FromUnixTimeSeconds(unix);
        Assert.Equal(dt, DateTime.SpecifyKind(back, DateTimeKind.Utc));
    }

    [Fact]
    public void FromDateTimeOffset_ShouldReturnDateTime()
    {
        var dto = new DateTimeOffset(2024, 6, 1, 1, 2, 3, TimeSpan.Zero);
        var dt = DateTimeHelper.FromDateTimeOffset(dto);
        Assert.Equal(dto.DateTime, dt);
    }

    [Fact]
    public void FromDateOnly_ShouldCombineDateAndTime()
    {
        var date = new DateOnly(2024, 1, 2);
        var time = new TimeOnly(3, 4, 5);
        var dt = DateTimeHelper.FromDateOnly(date, time, DateTimeKind.Utc);
        Assert.Equal(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc), dt);
    }
}
