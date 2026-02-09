using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class DecimalHelperTests
{
    [Test]
    public async Task ParseInvariant_ShouldReturnDecimal()
    {
        var result = DecimalHelper.ParseInvariant("123.45");
        await Assert.That(result).IsEqualTo(123.45m);
    }

    [Test]
    public async Task ParseInvariantOrNull_ValidString_ShouldReturnDecimal()
    {
        var result = DecimalHelper.ParseInvariantOrNull("123.45");
        await Assert.That(result).IsEqualTo(123.45m);
    }

    [Test]
    public async Task ParseInvariantOrNull_InvalidString_ShouldReturnNull()
    {
        var result = DecimalHelper.ParseInvariantOrNull("invalid");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Round_ShouldRoundCorrectly()
    {
        var result = DecimalHelper.Round(123.456m, 2);
        await Assert.That(result).IsEqualTo(123.46m);
    }

    [Test]
    public async Task Abs_ShouldReturnAbsoluteValue()
    {
        var result = DecimalHelper.Abs(-123.45m);
        await Assert.That(result).IsEqualTo(123.45m);
    }

    [Test]
    public async Task Ceiling_ShouldReturnCeiling()
    {
        var result = DecimalHelper.Ceiling(123.45m);
        await Assert.That(result).IsEqualTo(124m);
    }

    [Test]
    public async Task Floor_ShouldReturnFloor()
    {
        var result = DecimalHelper.Floor(123.45m);
        await Assert.That(result).IsEqualTo(123m);
    }

    [Test]
    public async Task Truncate_ShouldTruncateValue()
    {
        var result = DecimalHelper.Truncate(123.45m);
        await Assert.That(result).IsEqualTo(123m);
    }
}
