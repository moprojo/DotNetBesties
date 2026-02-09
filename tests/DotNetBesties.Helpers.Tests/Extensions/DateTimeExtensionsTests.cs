using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="DateTimeExtensions"/>.
/// </summary>
public class DateTimeExtensionsTests
{
    #region Conversion

    [Test]
    public async Task ToUnixTimeMilliseconds_WithDateTime_ReturnsCorrectValue()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = dt.ToUnixTimeMilliseconds();

        // Assert
        await Assert.That(result).IsEqualTo(1704067200000L);
    }

    [Test]
    public async Task ToUnixTimeSeconds_WithDateTime_ReturnsCorrectValue()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = dt.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsEqualTo(1704067200L);
    }

    [Test]
    public async Task ToUtc_WithLocalTime_ConvertsToUtc()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Local);

        // Act
        var result = dt.ToUtc();

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Utc);
    }

    [Test]
    public async Task ToLocal_WithUtcTime_ConvertsToLocal()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = dt.ToLocal();

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Local);
    }

    #endregion

    #region Date/Time Components

    [Test]
    public async Task ToDateOnly_WithDateTime_ReturnsDateOnly()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15, 14, 30, 0);

        // Act
        var result = dt.ToDateOnly();

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 1, 15));
    }

    [Test]
    public async Task ToTimeOnly_WithDateTime_ReturnsTimeOnly()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15, 14, 30, 45);

        // Act
        var result = dt.ToTimeOnly();

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30, 45));
    }

    #endregion

    #region ISO Week

    [Test]
    public async Task GetIsoWeek_WithDateTime_ReturnsCorrectWeek()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15);

        // Act
        var result = dt.GetIsoWeek();

        // Assert
        await Assert.That(result).IsGreaterThan(0);
        await Assert.That(result).IsLessThanOrEqualTo(53);
    }

    [Test]
    public async Task GetIsoWeekYear_WithDateTime_ReturnsCorrectYear()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1);

        // Act
        var result = dt.GetIsoWeekYear();

        // Assert
        await Assert.That(result).IsEqualTo(2024);
    }

    #endregion

    #region Kind Specification

    [Test]
    public async Task WithKind_ChangesDateTimeKind()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = dt.WithKind(DateTimeKind.Utc);

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Utc);
        await Assert.That(result.Ticks).IsEqualTo(dt.Ticks);
    }

    [Test]
    public async Task AsUtc_SetsKindToUtc()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = dt.AsUtc();

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Utc);
    }

    [Test]
    public async Task AsLocal_SetsKindToLocal()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = dt.AsLocal();

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Local);
    }

    [Test]
    public async Task AsUnspecified_SetsKindToUnspecified()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var result = dt.AsUnspecified();

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Unspecified);
    }

    #endregion

    #region Manipulation

    [Test]
    public async Task StartOfDay_ReturnsMiddnight()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15, 14, 30, 45);

        // Act
        var result = dt.StartOfDay();

        // Assert
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 15, 0, 0, 0));
    }

    [Test]
    public async Task EndOfDay_ReturnsLastMoment()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15, 14, 30, 45);

        // Act
        var result = dt.EndOfDay();

        // Assert
        await Assert.That(result.Date).IsEqualTo(dt.Date);
        await Assert.That(result.Hour).IsEqualTo(23);
        await Assert.That(result.Minute).IsEqualTo(59);
        await Assert.That(result.Second).IsEqualTo(59);
    }

    [Test]
    public async Task StartOfWeek_WithMonday_ReturnsMonday()
    {
        // Arrange - Friday
        var dt = new DateTime(2024, 1, 19);

        // Act
        var result = dt.StartOfWeek(DayOfWeek.Monday);

        // Assert
        await Assert.That(result.DayOfWeek).IsEqualTo(DayOfWeek.Monday);
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 15));
    }

    [Test]
    public async Task StartOfMonth_ReturnsFirstDay()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15, 14, 30, 45);

        // Act
        var result = dt.StartOfMonth();

        // Assert
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 1, 0, 0, 0));
    }

    [Test]
    public async Task EndOfMonth_ReturnsLastMoment()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 15);

        // Act
        var result = dt.EndOfMonth();

        // Assert
        await Assert.That(result.Day).IsEqualTo(31);
        await Assert.That(result.Hour).IsEqualTo(23);
        await Assert.That(result.Minute).IsEqualTo(59);
    }

    [Test]
    public async Task StartOfYear_ReturnsFirstDay()
    {
        // Arrange
        var dt = new DateTime(2024, 6, 15, 14, 30, 45);

        // Act
        var result = dt.StartOfYear();

        // Assert
        await Assert.That(result).IsEqualTo(new DateTime(2024, 1, 1, 0, 0, 0));
    }

    [Test]
    public async Task EndOfYear_ReturnsLastMoment()
    {
        // Arrange
        var dt = new DateTime(2024, 6, 15);

        // Act
        var result = dt.EndOfYear();

        // Assert
        await Assert.That(result.Month).IsEqualTo(12);
        await Assert.That(result.Day).IsEqualTo(31);
        await Assert.That(result.Hour).IsEqualTo(23);
    }

    #endregion

    #region Queries

    [Test]
    public async Task IsInPast_WithPastDate_ReturnsTrue()
    {
        // Arrange
        var dt = DateTime.Now.AddDays(-1);

        // Act
        var result = dt.IsInPast();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsInFuture_WithFutureDate_ReturnsTrue()
    {
        // Arrange
        var dt = DateTime.Now.AddDays(1);

        // Act
        var result = dt.IsInFuture();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsToday_WithToday_ReturnsTrue()
    {
        // Arrange
        var dt = DateTime.Now;

        // Act
        var result = dt.IsToday();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsWeekend_WithSaturday_ReturnsTrue()
    {
        // Arrange - Find next Saturday
        var dt = DateTime.Today;
        while (dt.DayOfWeek != DayOfWeek.Saturday)
            dt = dt.AddDays(1);

        // Act
        var result = dt.IsWeekend();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsWeekday_WithMonday_ReturnsTrue()
    {
        // Arrange - Find next Monday
        var dt = DateTime.Today;
        while (dt.DayOfWeek != DayOfWeek.Monday)
            dt = dt.AddDays(1);

        // Act
        var result = dt.IsWeekday();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsLeapYear_With2024_ReturnsTrue()
    {
        // Arrange
        var dt = new DateTime(2024, 1, 1);

        // Act
        var result = dt.IsLeapYear();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsLeapYear_With2023_ReturnsFalse()
    {
        // Arrange
        var dt = new DateTime(2023, 1, 1);

        // Act
        var result = dt.IsLeapYear();

        // Assert
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Age Calculation

    [Test]
    public async Task GetAge_WithBirthDate_ReturnsCorrectAge()
    {
        // Arrange
        var birthDate = new DateTime(2000, 1, 1);
        var asOfDate = new DateTime(2024, 1, 1);

        // Act
        var age = birthDate.GetAge(asOfDate);

        // Assert
        await Assert.That(age).IsEqualTo(24);
    }

    [Test]
    public async Task GetAge_BeforeBirthday_ReturnsYoungerAge()
    {
        // Arrange
        var birthDate = new DateTime(2000, 6, 15);
        var asOfDate = new DateTime(2024, 1, 1);

        // Act
        var age = birthDate.GetAge(asOfDate);

        // Assert
        await Assert.That(age).IsEqualTo(23);
    }

    [Test]
    public async Task GetAge_AfterBirthday_ReturnsOlderAge()
    {
        // Arrange
        var birthDate = new DateTime(2000, 1, 15);
        var asOfDate = new DateTime(2024, 6, 1);

        // Act
        var age = birthDate.GetAge(asOfDate);

        // Assert
        await Assert.That(age).IsEqualTo(24);
    }

    #endregion
}
