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
}
