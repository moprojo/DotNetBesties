using System;
using System.Globalization;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class BoolHelperTests
{
    #region TryParseExactDateOnlyInvariant Tests

    [Test]
    public async Task TryParseExactDateOnlyInvariant_WithValidString_ReturnsTrue()
    {
        var ok = BoolHelper.TryParseExactDateOnlyInvariant("2025-01-02", "yyyy-MM-dd", out var date);
        await Assert.That(ok).IsTrue();
        await Assert.That(date).IsEqualTo(new DateOnly(2025, 1, 2));
    }

    [Test]
    public async Task TryParseExactDateOnlyInvariant_WithFormats_ReturnsTrue()
    {
        var formats = new[] { "yyyy/MM/dd", "yyyy-MM-dd" };
        var ok = BoolHelper.TryParseExactDateOnlyInvariant("2025-01-02", formats, out var date);
        await Assert.That(ok).IsTrue();
        await Assert.That(date).IsEqualTo(new DateOnly(2025, 1, 2));
    }

    [Test]
    public async Task TryParseExactDateOnlyInvariant_WithInvalidString_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateOnlyInvariant("not a date", "yyyy-MM-dd", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseExactDateOnlyInvariant_WithNull_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateOnlyInvariant(null, "yyyy-MM-dd", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseExactDateOnlyInvariant_WithWrongFormat_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateOnlyInvariant("01/02/2025", "yyyy-MM-dd", out _);
        await Assert.That(ok).IsFalse();
    }

    #endregion

    #region TryParseExactDateTimeInvariant Tests

    [Test]
    public async Task TryParseExactDateTimeInvariant_WithValidString_ReturnsTrue()
    {
        var input = "2025-01-02T03:04:05.0000000Z";
        var ok = BoolHelper.TryParseExactDateTimeInvariant(input, "O", out var dt, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        await Assert.That(dt).IsEqualTo(DateTime.ParseExact(input, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
    }

    [Test]
    public async Task TryParseExactDateTimeInvariant_WithFormats_ReturnsTrue()
    {
        var formats = new[] { "O", "yyyy-MM-ddTHH:mm:ssZ" };
        var input = "2025-01-02T03:04:05Z";
        var ok = BoolHelper.TryParseExactDateTimeInvariant(input, formats, out var dt, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        var expected = DateTime.ParseExact(input, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        await Assert.That(dt).IsEqualTo(expected);
    }

    [Test]
    public async Task TryParseExactDateTimeInvariant_WithInvalidString_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeInvariant("not a datetime", "O", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseExactDateTimeInvariant_WithNull_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeInvariant(null, "O", out _);
        await Assert.That(ok).IsFalse();
    }

    #endregion

    #region TryParseExactDateTimeOffsetInvariant Tests

    [Test]
    public async Task TryParseExactDateTimeOffsetInvariant_WithValidString_ReturnsTrue()
    {
        var input = "2025-01-02T03:04:05.0000000+00:00";
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant(input, "O", out var dto, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        await Assert.That(dto).IsEqualTo(DateTimeOffset.ParseExact(input, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
    }

    [Test]
    public async Task TryParseExactDateTimeOffsetInvariant_WithFormats_ReturnsTrue()
    {
        var formats = new[] { "O", "yyyy-MM-ddTHH:mm:sszzz" };
        var input = "2025-01-02T03:04:05+00:00";
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant(input, formats, out var dto, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        var expected = DateTimeOffset.ParseExact(input, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        await Assert.That(dto).IsEqualTo(expected);
    }

    [Test]
    public async Task TryParseExactDateTimeOffsetInvariant_Invalid_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant("not a date", "O", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseExactDateTimeOffsetInvariant_WithNull_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant(null, "O", out _);
        await Assert.That(ok).IsFalse();
    }

    #endregion

    #region TryParseLongInvariant Tests

    [Test]
    public async Task TryParseLongInvariant_WithValidString_ReturnsTrue()
    {
        var ok = BoolHelper.TryParseLongInvariant("42", out var value);
        await Assert.That(ok).IsTrue();
        await Assert.That(value).IsEqualTo(42L);
    }

    [Test]
    public async Task TryParseLongInvariant_WithNegativeNumber_ReturnsTrue()
    {
        var ok = BoolHelper.TryParseLongInvariant("-123", out var value);
        await Assert.That(ok).IsTrue();
        await Assert.That(value).IsEqualTo(-123L);
    }

    [Test]
    public async Task TryParseLongInvariant_WithInvalidString_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseLongInvariant("not a number", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseLongInvariant_WithNull_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseLongInvariant(null, out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseLongInvariant_WithDecimal_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseLongInvariant("123.45", out _);
        await Assert.That(ok).IsFalse();
    }

    #endregion

    #region TryParseExactTimeSpanInvariant Tests

    [Test]
    public async Task TryParseExactTimeSpanInvariant_WithValidString_ReturnsTrue()
    {
        var ok = BoolHelper.TryParseExactTimeSpanInvariant("01:02:03", "c", out var ts);
        await Assert.That(ok).IsTrue();
        await Assert.That(ts).IsEqualTo(new TimeSpan(1, 2, 3));
    }

    [Test]
    public async Task TryParseExactTimeSpanInvariant_WithFormats_ReturnsTrue()
    {
        var formats = new[] { "c", "g" };
        var ok = BoolHelper.TryParseExactTimeSpanInvariant("01:02:03", formats, out var ts);
        await Assert.That(ok).IsTrue();
        await Assert.That(ts).IsEqualTo(new TimeSpan(1, 2, 3));
    }

    [Test]
    public async Task TryParseExactTimeSpanInvariant_WithInvalidString_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactTimeSpanInvariant("not a timespan", "c", out _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task TryParseExactTimeSpanInvariant_WithNull_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactTimeSpanInvariant(null, "c", out _);
        await Assert.That(ok).IsFalse();
    }

    #endregion
}
