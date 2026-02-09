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

    [Test]
    public async Task FromUnixTimeMilliseconds_ShouldReturnExpectedDateTime()
    {
        var milliseconds = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds();
        var result = DateTimeHelper.FromUnixTimeMilliseconds(milliseconds);
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    [Test]
    public async Task FromUnixTimeMilliseconds_NullableNull_ReturnsNull()
    {
        long? milliseconds = null;
        var result = DateTimeHelper.FromUnixTimeMilliseconds(milliseconds);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task SpecifyKind_ShouldSetCorrectKind()
    {
        var dt = new DateTime(2024, 1, 1, 0, 0, 0);
        var result = DateTimeHelper.SpecifyKind(dt, DateTimeKind.Utc);
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Utc);
    }

    #region New Method Tests

    [Test]
    public async Task ToDateOnly_ExtractsDatePortion()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.ToDateOnly(dt);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 6, 15));
    }

    [Test]
    public async Task ToTimeOnly_ExtractsTimePortion()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.ToTimeOnly(dt);
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30, 45));
    }

    [Test]
    public async Task GetIsoWeekYear_ReturnsCorrectYear()
    {
        // January 1, 2024 is a Monday (Week 1 of 2024)
        var dt = new DateTime(2024, 1, 1);
        var result = DateTimeHelper.GetIsoWeekYear(dt);
        await Assert.That(result).IsEqualTo(2024);
    }

    [Test]
    public async Task GetIsoWeekYear_EndOfYearInNextYear()
    {
        // December 31, 2023 is a Sunday (Week 52 of 2023, but ISO week year might be different)
        var dt = new DateTime(2023, 12, 31);
        var result = DateTimeHelper.GetIsoWeekYear(dt);
        // This date is in ISO week year 2023
        await Assert.That(result).IsEqualTo(2023);
    }

    [Test]
    public async Task StartOfDay_ReturnsMiddnight()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.StartOfDay(dt);
        await Assert.That(result).IsEqualTo(new DateTime(2024, 6, 15, 0, 0, 0));
    }

    [Test]
    public async Task EndOfDay_ReturnsLastMoment()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.EndOfDay(dt);
        await Assert.That(result.Date).IsEqualTo(new DateTime(2024, 6, 15));
        await Assert.That(result.Hour).IsEqualTo(23);
        await Assert.That(result.Minute).IsEqualTo(59);
        await Assert.That(result.Second).IsEqualTo(59);
    }

    [Test]
    public async Task StartOfWeek_ReturnsMonday()
    {
        // June 15, 2024 is a Saturday
        var dt = new DateTime(2024, 6, 15);
        var result = DateTimeHelper.StartOfWeek(dt, DayOfWeek.Monday);
        // Should return Monday, June 10, 2024
        await Assert.That(result).IsEqualTo(new DateTime(2024, 6, 10));
        await Assert.That(result.DayOfWeek).IsEqualTo(DayOfWeek.Monday);
    }

    [Test]
    public async Task StartOfMonth_ReturnsFirstDay()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.StartOfMonth(dt);
        await Assert.That(result).IsEqualTo(new DateTime(2024, 6, 1, 0, 0, 0, dt.Kind));
    }

    [Test]
    public async Task EndOfMonth_ReturnsLastDay()
    {
        var dt = new DateTime(2024, 6, 15);
        var result = DateTimeHelper.EndOfMonth(dt);
        await Assert.That(result.Date).IsEqualTo(new DateTime(2024, 6, 30));
        await Assert.That(result.Hour).IsEqualTo(23);
        await Assert.That(result.Minute).IsEqualTo(59);
    }

    [Test]
    public async Task StartOfYear_ReturnsJanuaryFirst()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);
        var result = DateTimeHelper.StartOfYear(dt);
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 1, 0, 0, 0, dt.Kind));
    }

    [Test]
    public async Task EndOfYear_ReturnsDecember31()
    {
        var dt = new DateTime(2024, 6, 15);
        var result = DateTimeHelper.EndOfYear(dt);
        await Assert.That(result.Date).IsEqualTo(new DateTime(2024, 12, 31));
        await Assert.That(result.Hour).IsEqualTo(23);
        await Assert.That(result.Minute).IsEqualTo(59);
    }

    [Test]
    public async Task IsInPast_WithPastDate_ReturnsTrue()
    {
        var past = DateTime.Now.AddDays(-1);
        await Assert.That(DateTimeHelper.IsInPast(past)).IsTrue();
    }

    [Test]
    public async Task IsInFuture_WithFutureDate_ReturnsTrue()
    {
        var future = DateTime.Now.AddDays(1);
        await Assert.That(DateTimeHelper.IsInFuture(future)).IsTrue();
    }

    [Test]
    public async Task IsToday_WithToday_ReturnsTrue()
    {
        var today = DateTime.Today.AddHours(12);
        await Assert.That(DateTimeHelper.IsToday(today)).IsTrue();
    }

    [Test]
    public async Task IsWeekend_WithSaturday_ReturnsTrue()
    {
        // Find next Saturday
        var today = DateTime.Today;
        var daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
        var saturday = today.AddDays(daysUntilSaturday);
        await Assert.That(DateTimeHelper.IsWeekend(saturday)).IsTrue();
    }

    [Test]
    public async Task IsWeekday_WithMonday_ReturnsTrue()
    {
        // Find next Monday
        var today = DateTime.Today;
        var daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
        var monday = today.AddDays(daysUntilMonday == 0 ? 7 : daysUntilMonday);
        await Assert.That(DateTimeHelper.IsWeekday(monday)).IsTrue();
    }

    [Test]
    public async Task IsLeapYear_With2024_ReturnsTrue()
    {
        var dt = new DateTime(2024, 6, 15);
        await Assert.That(DateTimeHelper.IsLeapYear(dt)).IsTrue();
    }

    [Test]
    public async Task IsLeapYear_With2023_ReturnsFalse()
    {
        var dt = new DateTime(2023, 6, 15);
        await Assert.That(DateTimeHelper.IsLeapYear(dt)).IsFalse();
    }

    [Test]
    public async Task GetAge_ReturnsCorrectAge()
    {
        var birthDate = new DateTime(2000, 1, 1);
        var asOfDate = new DateTime(2024, 6, 15);
        var age = DateTimeHelper.GetAge(birthDate, asOfDate);
        await Assert.That(age).IsEqualTo(24);
    }

    [Test]
    public async Task GetAge_BeforeBirthday_ReturnsCorrectAge()
    {
        var birthDate = new DateTime(2000, 12, 31);
        var asOfDate = new DateTime(2024, 6, 15);
        var age = DateTimeHelper.GetAge(birthDate, asOfDate);
        await Assert.That(age).IsEqualTo(23);
    }

    #endregion
}
