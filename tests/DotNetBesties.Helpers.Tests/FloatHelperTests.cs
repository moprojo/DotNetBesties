using System;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class FloatHelperTests
{
    [Fact]
    public void TotalHours_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromHours(2);
        var result = FloatHelper.TotalHours(ts);
        Assert.Equal(2f, result);
    }

    [Fact]
    public void ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = FloatHelper.ToOADate(dto);
        var back = DateTime.FromOADate(oa);
        Assert.Equal(dto.DateTime, back);
    }
}
