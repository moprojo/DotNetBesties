using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class CharHelperTests
{
    [Test]
    public async Task GetFirstOrDefault_ShouldReturnFirstChar()
    {
        var result = CharHelper.GetFirstOrDefault("abc");
        await Assert.That(result).IsEqualTo('a');
    }

    [Test]
    public async Task GetFirstOrDefault_Null_ReturnsDefault()
    {
        var result = CharHelper.GetFirstOrDefault(null, '?');
        await Assert.That(result).IsEqualTo('?');
    }
}
