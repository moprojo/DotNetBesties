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

    [Fact]
    public void GetLastOrDefault_ShouldReturnLastChar()
    {
        var result = CharHelper.GetLastOrDefault("abc");
        Assert.Equal('c', result);
    }

    [Fact]
    public void GetAtOrDefault_OutOfRange_ReturnsDefault()
    {
        var result = CharHelper.GetAtOrDefault("abc", 5, '?');
        Assert.Equal('?', result);
    }

    [Fact]
    public void ToLower_ToUpper_ShouldUseInvariantCulture()
    {
        var lower = CharHelper.ToLower('A');
        var upper = CharHelper.ToUpper('a');
        Assert.Equal('a', lower);
        Assert.Equal('A', upper);
    }
}
