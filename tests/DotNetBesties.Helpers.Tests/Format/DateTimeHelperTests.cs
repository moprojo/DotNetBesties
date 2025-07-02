using System;
using System.Globalization;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class DateTimeHelperTests
{
    [Test]
    public async Task Format_RoundTrip_ShouldMatchParse()
    {
        var now = new DateTime(2024, 4, 5, 10, 20, 30, DateTimeKind.Utc);
        var formatted = StringHelper.FromDateTime(now);
        var parsed = DateTimeHelper.ParseExactInvariant(formatted, "O", DateTimeStyles.RoundtripKind);
        await Assert.That(parsed).IsEqualTo(now);
    }

    [Test]
    public async Task TryParseExactInvariant_Invalid_ReturnsFalse()
    {
        var ok = BoolHelper.TryParseExactDateTimeInvariant("invalid", "O", out var _);
        await Assert.That(ok).IsFalse();
    }

    [Test]
    public async Task Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateTime(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateTimeHelper.ParseExactInvariantOrNull("bad", "O");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task UnixTimeConversions_ShouldRoundTrip()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var unix = LongHelper.ToUnixTimeSeconds(dt);
        var back = DateTimeHelper.FromUnixTimeSeconds(unix);
        await Assert.That(DateTime.SpecifyKind(back, DateTimeKind.Utc)).IsEqualTo(dt);
    }

    [Test]
    public async Task FromDateTimeOffset_ShouldReturnDateTime()
    {
        var dto = new DateTimeOffset(2024, 6, 1, 1, 2, 3, TimeSpan.Zero);
        var dt = DateTimeHelper.FromDateTimeOffset(dto);
        await Assert.That(dt).IsEqualTo(dto.DateTime);
    }

    [Test]
    public async Task FromDateOnly_ShouldCombineDateAndTime()
    {
        var date = new DateOnly(2024, 1, 2);
        var time = new TimeOnly(3, 4, 5);
        var dt = DateTimeHelper.FromDateOnly(date, time, DateTimeKind.Utc);
        await Assert.That(dt).IsEqualTo(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc));
    }

    [Test]
    public async Task ToLocalTime_ShouldReturnLocalTime()
    {
        var utc = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var local = DateTimeHelper.ToLocalTime(utc);
        await Assert.That(local.Kind).IsEqualTo(DateTimeKind.Local);
    }

    [Test]
    public async Task ToUniversalTime_Null_ReturnsNull()
    {
        DateTime? input = null;
        var result = DateTimeHelper.ToUniversalTime(input);
        await Assert.That(result).IsNull();
    }
}
