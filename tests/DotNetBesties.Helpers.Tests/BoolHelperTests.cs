using System;
using System.Globalization;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class BoolHelperTests
{
    [Fact]
    public void TryParseExactDateOnlyInvariant_ShouldParse()
    {
        var ok = BoolHelper.TryParseExactDateOnlyInvariant("2024-06-01", "yyyy-MM-dd", out var date);
        Assert.True(ok);
        Assert.Equal(new DateOnly(2024, 6, 1), date);
    }

    [Fact]
    public void TryParseExactDateTimeInvariant_Array_ShouldParse()
    {
        var formats = new[] { "O" };
        var dt = DateTime.UtcNow;
        var text = dt.ToString("O", CultureInfo.InvariantCulture);
        var ok = BoolHelper.TryParseExactDateTimeInvariant(text, formats, out var result, DateTimeStyles.RoundtripKind);
        Assert.True(ok);
        Assert.Equal(dt, result);
    }

    [Fact]
    public void TryParseExactDateTimeOffsetInvariant_Invalid_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant("invalid", "O", out var _);
        Assert.False(ok);
    }

    [Fact]
    public void TryParseLongInvariant_ShouldParse()
    {
        var ok = BoolHelper.TryParseLongInvariant("123", out var value);
        Assert.True(ok);
        Assert.Equal(123L, value);
    }

    [Fact]
    public void TryParseExactTimeSpanInvariant_Array_ShouldParse()
    {
        var formats = new[] { "c" };
        var ok = BoolHelper.TryParseExactTimeSpanInvariant("01:00:00", formats, out var ts);
        Assert.True(ok);
        Assert.Equal(TimeSpan.FromHours(1), ts);
    }
}
