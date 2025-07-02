using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class DateTimeOffsetHelperTests
{
    [Test]
    public async Task UnixConversion_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 6, 1, 12, 0, 0, TimeSpan.Zero);
        var seconds = LongHelper.ToUnixTimeSeconds(dto);
        var fromSeconds = DateTimeOffsetHelper.FromUnixTimeSeconds(seconds);
        await Assert.That(fromSeconds).IsEqualTo(dto);
    }

    [Test]
    public async Task Format_NullableNull_ReturnsNull()
    {
        string? result = StringHelper.FromDateTimeOffset(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Format_ShouldUseInvariantCulture()
    {
        var dto = new DateTimeOffset(2025, 1, 1, 8, 30, 0, TimeSpan.Zero);
        var formatted = StringHelper.FromDateTimeOffset(dto, "yyyy-MM-ddTHH:mm:sszzz");
        await Assert.That(formatted).IsEqualTo("2025-01-01T08:30:00+00:00");
    }

    [Test]
    public async Task ParseExactInvariantOrNull_Invalid_ReturnsNull()
    {
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull("bad", "O");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FromDateTime_ShouldReturnDateTimeOffset()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(dt);
        await Assert.That(dto).IsEqualTo(new DateTimeOffset(dt));
    }

    [Test]
    public async Task FromDateOnly_WithOffset_ReturnsDto()
    {
        var date = new DateOnly(2024, 2, 3);
        var time = new TimeOnly(4, 5, 6);
        var offset = TimeSpan.FromHours(1);
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, offset);
        await Assert.That(dto).IsEqualTo(new DateTimeOffset(date.ToDateTime(time), offset));
    }

    [Test]
    public async Task ConvertTime_ShouldChangeTimeZone()
    {
        var utc = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var berlin = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin");
        var converted = DateTimeOffsetHelper.ConvertTime(utc, berlin);
        await Assert.That(converted.Offset).IsEqualTo(berlin.GetUtcOffset(utc));
    }

    [Test]
    public async Task ToOffset_ShouldApplyOffset()
    {
        var dto = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var result = DateTimeOffsetHelper.ToOffset(dto, TimeSpan.FromHours(2));
        await Assert.That(result.Offset).IsEqualTo(TimeSpan.FromHours(2));
    }
}
