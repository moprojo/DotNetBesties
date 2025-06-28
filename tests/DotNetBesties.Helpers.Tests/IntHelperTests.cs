using System;
using DotNetBesties.Helpers;
using Xunit;

namespace DotNetBesties.Helpers.Tests;

public class IntHelperTests
{
    [Fact]
    public void Year_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2025, 6, 15);
        Assert.Equal(2025, IntHelper.Year(dt));
    }

    [Fact]
    public void DayOfYear_FromDateOnly_ReturnsExpected()
    {
        var date = new DateOnly(2025, 2, 1);
        Assert.Equal(date.DayOfYear, IntHelper.DayOfYear(date));
    }

    [Fact]
    public void Hours_FromTimeSpan_ReturnsExpected()
    {
        var ts = TimeSpan.FromHours(5);
        Assert.Equal(5, IntHelper.Hours(ts));
    }

    [Fact]
    public void ParseInvariant_ShouldParseString()
    {
        Assert.Equal(10, IntHelper.ParseInvariant("10"));
    }

    [Fact]
    public void Millisecond_FromDateTime_ReturnsExpected()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, 123);
        Assert.Equal(123, IntHelper.Millisecond(dt));
    }
}
