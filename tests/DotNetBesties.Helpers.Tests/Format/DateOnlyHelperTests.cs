using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

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
        string? result = StringHelper.FromDateOnly(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateOnlyHelper.ParseExactInvariantOrNull("bad", "yyyy-MM-dd");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_MultipleFormats_ShouldReturnExpected()
    {
        var formats = new[] { "yyyy-MM-dd", "MM/dd/yyyy" };
        var result = DateOnlyHelper.ParseExactInvariantOrNull("05/01/2024", formats);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 5, 1));
    }

    [Test]
    public async Task FromDateTimeOffset_ShouldReturnDateOnly()
    {
        var dto = new DateTimeOffset(2024, 5, 1, 0, 0, 0, TimeSpan.Zero);
        var date = DateOnlyHelper.FromDateTimeOffset(dto);
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 5, 1));
    }

    [Test]
    public async Task AddDays_ShouldAddCorrectly()
    {
        var date = new DateOnly(2024, 5, 1);
        var result = DateOnlyHelper.AddDays(date, 10);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 5, 11));
    }

    [Test]
    public async Task AddMonths_ShouldAddCorrectly()
    {
        var date = new DateOnly(2024, 5, 1);
        var result = DateOnlyHelper.AddMonths(date, 2);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 7, 1));
    }

    [Test]
    public async Task AddYears_ShouldAddCorrectly()
    {
        var date = new DateOnly(2024, 5, 1);
        var result = DateOnlyHelper.AddYears(date, 1);
        await Assert.That(result).IsEqualTo(new DateOnly(2025, 5, 1));
    }

    [Test]
    public async Task FromUnixTimeSeconds_ShouldReturnDateOnly()
    {
        var seconds = new DateTimeOffset(2024, 6, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds();
        var date = DateOnlyHelper.FromUnixTimeSeconds(seconds);
        await Assert.That(date).IsEqualTo(new DateOnly(2024, 6, 1));
    }
}
