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
        var formatted = DateOnlyHelper.Format(date, "yyyy/MM/dd");
        Assert.Equal("2024/12/31", formatted);
    }
}
