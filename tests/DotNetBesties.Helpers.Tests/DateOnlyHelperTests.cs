using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DateOnlyHelperTests
{
    [Fact]
    public void ParseExactInvariant_ShouldReturnExpectedDate()
    {
        var date = DateOnlyHelper.ParseExactInvariant("2024-05-01", "yyyy-MM-dd");
        Assert.Equal(new DateOnly(2024, 5, 1), date);
    }

    [Fact]
    public void Format_ShouldUseInvariantCulture()
    {
        var date = new DateOnly(2024, 12, 31);
        var formatted = StringHelper.FromDateOnly(date, "yyyy/MM/dd");
        Assert.Equal("2024/12/31", formatted);
    }

    [Fact]
    public void Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateOnly((DateOnly?)null);
        Assert.Null(result);
    }

    [Fact]
    public void ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateOnlyHelper.ParseExactInvariantOrNull("bad", "yyyy-MM-dd");
        Assert.Null(result);
    }

    [Fact]
    public void FromDateTimeOffset_ShouldReturnDateOnly()
    {
        var dto = new DateTimeOffset(2024, 5, 1, 0, 0, 0, TimeSpan.Zero);
        var date = DateOnlyHelper.FromDateTimeOffset(dto);
        Assert.Equal(new DateOnly(2024, 5, 1), date);
    }
}
