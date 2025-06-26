using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class CharHelperTests
{
    [Fact]
    public void GetFirstOrDefault_ShouldReturnFirstChar()
    {
        var result = CharHelper.GetFirstOrDefault("abc");
        Assert.Equal('a', result);
    }

    [Fact]
    public void GetFirstOrDefault_Null_ReturnsDefault()
    {
        var result = CharHelper.GetFirstOrDefault(null, '?');
        Assert.Equal('?', result);
    }
}
