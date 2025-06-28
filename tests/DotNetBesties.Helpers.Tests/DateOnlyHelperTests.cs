using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DateOnlyHelperTests
{
    [Test]
    public async Task ParseExactInvariant_ShouldReturnExpectedDate()
    {
        var date = DateOnlyHelper.ParseExactInvariant("2024-05-01", "yyyy-MM-dd");
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 5, 1));
    }

    [Test]
    public async Task Format_ShouldUseInvariantCulture()
    {
        var date = new DateOnly(2024, 12, 31);
        var formatted = StringHelper.FromDateOnly(date, "yyyy/MM/dd");
        await Assert.That(formatted).IsEqualTo("2024/12/31");
    }

    [Test]
    public async Task Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateOnly((DateOnly?)null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateOnlyHelper.ParseExactInvariantOrNull("bad", "yyyy-MM-dd");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FromDateTimeOffset_ShouldReturnDateOnly()
    {
        var dto = new DateTimeOffset(2024, 5, 1, 0, 0, 0, TimeSpan.Zero);
        var date = DateOnlyHelper.FromDateTimeOffset(dto);
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 5, 1));
    }
}
