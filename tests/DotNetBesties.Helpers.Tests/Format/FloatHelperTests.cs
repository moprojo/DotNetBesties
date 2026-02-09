using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class FloatHelperTests
{
    #region ToOADate Tests

    [Test]
    public async Task ToOADate_FromDateTime_Works()
    {
        var dt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc);
        var oa = FloatHelper.ToOADate(dt);
        await Assert.That(oa).IsGreaterThan(0);
    }

    [Test]
    public async Task ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 3, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = FloatHelper.ToOADate(dto);
        var back = new DateTimeOffset(DateTime.SpecifyKind(DateTime.FromOADate(oa), DateTimeKind.Utc));
        await Assert.That(back.UtcDateTime).IsEqualTo(dto.UtcDateTime);
    }

    [Test]
    public async Task ToOADate_NullableDateTime_ReturnsNull()
    {
        DateTime? dt = null;
        var result = FloatHelper.ToOADate(dt);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ToOADate_NullableDateTimeOffset_ReturnsNull()
    {
        DateTimeOffset? dto = null;
        var result = FloatHelper.ToOADate(dto);
        await Assert.That(result).IsNull();
    }

    #endregion

    #region ParseInvariant Tests

    [Test]
    public async Task ParseInvariant_WithValidString_ReturnsFloat()
    {
        var result = FloatHelper.ParseInvariant("123.45");
        await Assert.That(result).IsEqualTo(123.45f);
    }

    [Test]
    public async Task ParseInvariant_WithScientificNotation_ReturnsFloat()
    {
        var result = FloatHelper.ParseInvariant("1.5e2");
        await Assert.That(result).IsEqualTo(150.0f);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithValidString_ReturnsFloat()
    {
        var result = FloatHelper.ParseInvariantOrNull("123.45");
        await Assert.That(result).IsEqualTo(123.45f);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInvalidString_ReturnsNull()
    {
        var result = FloatHelper.ParseInvariantOrNull("invalid");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNull_ReturnsNull()
    {
        var result = FloatHelper.ParseInvariantOrNull(null);
        await Assert.That(result).IsNull();
    }

    #endregion

    #region TimeSpan Tests

    [Test]
    public async Task TotalHours_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromHours(2);
        var result = FloatHelper.TotalHours(ts);
        await Assert.That(result).IsEqualTo(2f);
    }

    [Test]
    public async Task TotalDays_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromDays(1.5);
        var result = FloatHelper.TotalDays(ts);
        await Assert.That(result).IsEqualTo(1.5f);
    }

    [Test]
    public async Task TotalDays_Nullable_ReturnsNull()
    {
        TimeSpan? ts = null;
        var result = FloatHelper.TotalDays(ts);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task TotalMinutes_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromMinutes(30);
        var result = FloatHelper.TotalMinutes(ts);
        await Assert.That(result).IsEqualTo(30f);
    }

    [Test]
    public async Task TotalSeconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromSeconds(90);
        var result = FloatHelper.TotalSeconds(ts);
        await Assert.That(result).IsEqualTo(90f);
    }

    [Test]
    public async Task TotalMilliseconds_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromMilliseconds(1500);
        var result = FloatHelper.TotalMilliseconds(ts);
        await Assert.That(result).IsEqualTo(1500f);
    }

    #endregion
}
