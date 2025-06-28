using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using System.Globalization;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class IntegrationTests
{
    [Test]
    public async Task DateTime_ToDateTimeOffset_ToUnix_RoundTrip()
    {
        var now = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(now);
        var unix = LongHelper.ToUnixTimeSeconds(dto);
        var backDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unix);
        await Assert.That(backDto).IsEqualTo(dto);
    }

    [Test]
    public async Task DateOnly_ToDateTimeOffset_WithZone_RoundTrip()
    {
        var date = new DateOnly(2024, 12, 24);
        var time = new TimeOnly(10, 0);
        var zone = TimeZoneInfo.Utc;
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, zone);
        var backDate = DateOnly.FromDateTime(dto.UtcDateTime);
        await Assert.That(backDate).IsEqualTo(date);
    }

    [Test]
    public async Task DateTimeOffset_FormatParse_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 8, 15, 12, 0, 0, TimeSpan.Zero);
        var text = StringHelper.FromDateTimeOffset(dto);
        var parsed = DateTimeOffsetHelper.ParseExactInvariant(text, "O", DateTimeStyles.RoundtripKind);
        await Assert.That(parsed).IsEqualTo(dto);
    }

    [Test]
    public async Task TryParseDateTimeOffset_ToDateOnly_RoundTrip()
    {
        var input = "2024-06-30T23:00:00.0000000+00:00";
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant(input, "O", out var dto, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        var date = DateOnlyHelper.FromDateTimeOffset(dto);
        var text = StringHelper.FromDateOnly(date);
        var parsed = DateOnlyHelper.ParseExactInvariant(text, "yyyy-MM-dd");
        await Assert.That(parsed).IsEqualTo(date);
    }

    [Test]
    public async Task TimeSpan_String_Parse_Total()
    {
        var ts = new TimeSpan(1, 30, 45);
        var formatted = StringHelper.FromTimeSpan(ts);
        var parsed = TimeSpanHelper.ParseExactInvariant(formatted, "c");
        await Assert.That(parsed).IsEqualTo(ts);
        await Assert.That(DoubleHelper.TotalSeconds(parsed)).IsEqualTo(ts.TotalSeconds);
        await Assert.That(FloatHelper.TotalHours(parsed)).IsEqualTo((float)ts.TotalHours);
    }
}
