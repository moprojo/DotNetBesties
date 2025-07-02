using System.Globalization;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class CharHelperTests
{
    [Test]
    public async Task ToLower_WithCustomCulture_ShouldConvertCorrectly()
    {
        var culture = new CultureInfo("tr-TR");
        var lower = CharHelper.ToLower('I', culture);
        await Assert.That(lower).IsEqualTo('ı');
    }

    [Test]
    public async Task ToUpper_WithCustomCulture_ShouldConvertCorrectly()
    {
        var culture = new CultureInfo("tr-TR");
        var upper = CharHelper.ToUpper('i', culture);
        await Assert.That(upper).IsEqualTo('İ');
    }

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

    [Test]
    public async Task GetAtOrDefault_OutOfRange_ReturnsDefault()
    {
        var result = CharHelper.GetAtOrDefault("abc", 5, '!');
        await Assert.That(result).IsEqualTo('!');
    }

    [Test]
    public async Task GetLastOrDefault_ShouldReturnLastChar()
    {
        var result = CharHelper.GetLastOrDefault("xyz");
        await Assert.That(result).IsEqualTo('z');
    }

    [Test]
    public async Task ToLower_ToUpper_ShouldUseInvariantCulture()
    {
        var lower = CharHelper.ToLower('A');
        var upper = CharHelper.ToUpper('ß');
        await Assert.That(lower).IsEqualTo('a');
        await Assert.That(upper).IsEqualTo(char.ToUpperInvariant('ß'));
    }
}
