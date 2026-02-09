using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class DoubleHelperTests
{
    #region ToOADate Tests

    [Test]
    public async Task ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = DoubleHelper.ToOADate(dto);
        var back = new DateTimeOffset(DateTime.SpecifyKind(DateTime.FromOADate(oa), DateTimeKind.Utc));
        await Assert.That(back.UtcDateTime).IsEqualTo(dto.UtcDateTime);
    }

    [Test]
    public async Task ToOADate_RoundTrip()
    {
        var dt = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc);
        var oa = DoubleHelper.ToOADate(dt);
        var back = DateTime.FromOADate(oa);
        await Assert.That(DateTime.SpecifyKind(back, DateTimeKind.Utc)).IsEqualTo(dt);
    }

    [Test]
    public async Task ToOADate_NullableDateTime_ReturnsNull()
    {
        DateTime? dt = null;
        var result = DoubleHelper.ToOADate(dt);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ToOADate_NullableDateTimeOffset_ReturnsNull()
    {
        DateTimeOffset? dto = null;
        var result = DoubleHelper.ToOADate(dto);
        await Assert.That(result).IsNull();
    }

    #endregion

    #region ParseInvariant Tests

    [Test]
    public async Task ParseInvariant_WithValidString_ReturnsDouble()
    {
        var result = DoubleHelper.ParseInvariant("123.45");
        await Assert.That(result).IsEqualTo(123.45);
    }

    [Test]
    public async Task ParseInvariant_WithScientificNotation_ReturnsDouble()
    {
        var result = DoubleHelper.ParseInvariant("1.5e2");
        await Assert.That(result).IsEqualTo(150.0);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithValidString_ReturnsDouble()
    {
        var result = DoubleHelper.ParseInvariantOrNull("123.45");
        await Assert.That(result).IsEqualTo(123.45);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInvalidString_ReturnsNull()
    {
        var result = DoubleHelper.ParseInvariantOrNull("invalid");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNull_ReturnsNull()
    {
        var result = DoubleHelper.ParseInvariantOrNull(null);
        await Assert.That(result).IsNull();
    }

    #endregion

    #region TimeSpan Tests

    [Test]
    public async Task TotalDays_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromDays(1.5);
        var result = DoubleHelper.TotalDays(ts);
        await Assert.That(result).IsEqualTo(1.5);
    }

    [Test]
    public async Task TotalDays_Nullable_ReturnsNull()
    {
        TimeSpan? ts = null;
        var result = DoubleHelper.TotalDays(ts);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task TotalHours_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromHours(36);
        var result = DoubleHelper.TotalHours(ts);
        await Assert.That(result).IsEqualTo(36);
    }

    [Test]
    public async Task TotalMilliseconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromMilliseconds(12345);
        var result = DoubleHelper.TotalMilliseconds(ts);
        await Assert.That(result).IsEqualTo(12345);
    }

    [Test]
    public async Task TotalMinutes_ShouldReturnExpected()
    {
        var ts = new TimeSpan(1, 2, 0);
        var result = DoubleHelper.TotalMinutes(ts);
        await Assert.That(result).IsEqualTo(ts.TotalMinutes);
    }

    [Test]
    public async Task TotalSeconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromSeconds(123.45);
        var result = DoubleHelper.TotalSeconds(ts);
        await Assert.That(result).IsEqualTo(ts.TotalSeconds);
    }

    #endregion
}
