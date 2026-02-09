using System;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="DateOnlyExtensions"/>.
/// </summary>
public class DateOnlyExtensionsTests
{
    #region Date Manipulation

    [Test]
    public async Task AddDays_WithPositiveDays_AddsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddDays(10);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 1, 25));
    }

    [Test]
    public async Task AddDays_WithNegativeDays_SubtractsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddDays(-10);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 1, 5));
    }

    [Test]
    public async Task AddDays_WithZeroDays_ReturnsSameDate()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddDays(0);

        // Assert
        await Assert.That(result).IsEqualTo(date);
    }

    [Test]
    public async Task AddMonths_WithPositiveMonths_AddsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddMonths(3);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 4, 15));
    }

    [Test]
    public async Task AddMonths_WithNegativeMonths_SubtractsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 5, 15);

        // Act
        var result = date.AddMonths(-2);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 3, 15));
    }

    [Test]
    public async Task AddMonths_AcrossYearBoundary_HandlesCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 11, 15);

        // Act
        var result = date.AddMonths(3);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2025, 2, 15));
    }

    [Test]
    public async Task AddYears_WithPositiveYears_AddsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddYears(5);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2029, 1, 15));
    }

    [Test]
    public async Task AddYears_WithNegativeYears_SubtractsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 15);

        // Act
        var result = date.AddYears(-5);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2019, 1, 15));
    }

    #endregion

    #region Conversion

    [Test]
    public async Task ToDateTime_WithTimeOnly_CreatesCorrectDateTime()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 15);
        var time = new TimeOnly(14, 30, 45);

        // Act
        var result = date.ToDateTime(time);

        // Assert
        await Assert.That(result.Year).IsEqualTo(2024);
        await Assert.That(result.Month).IsEqualTo(3);
        await Assert.That(result.Day).IsEqualTo(15);
        await Assert.That(result.Hour).IsEqualTo(14);
        await Assert.That(result.Minute).IsEqualTo(30);
        await Assert.That(result.Second).IsEqualTo(45);
    }

    [Test]
    public async Task ToDateTimeWithKind_WithUtcKind_CreatesUtcDateTime()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 15);
        var time = new TimeOnly(14, 30, 0);

        // Act
        var result = date.ToDateTimeWithKind(time, DateTimeKind.Utc);

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Utc);
    }

    [Test]
    public async Task ToDateTimeWithKind_WithLocalKind_CreatesLocalDateTime()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 15);
        var time = new TimeOnly(14, 30, 0);

        // Act
        var result = date.ToDateTimeWithKind(time, DateTimeKind.Local);

        // Assert
        await Assert.That(result.Kind).IsEqualTo(DateTimeKind.Local);
    }

    #endregion

    #region Unix Time

    [Test]
    public async Task ToUnixTimeSeconds_WithValidDate_ReturnsCorrectTimestamp()
    {
        // Arrange
        var date = new DateOnly(1970, 1, 2); // Day after Unix epoch

        // Act
        var result = date.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsEqualTo(86400); // 24 hours in seconds
    }

    [Test]
    public async Task ToUnixTimeSeconds_WithNullableValue_ReturnsNull()
    {
        // Arrange
        DateOnly? date = null;

        // Act
        var result = date.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ToUnixTimeSeconds_WithNullableValue_ReturnsTimestamp()
    {
        // Arrange
        DateOnly? date = new DateOnly(1970, 1, 2);

        // Act
        var result = date.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsEqualTo(86400);
    }

    #endregion

    #region Parsing

    [Test]
    public async Task ParseExactInvariant_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15";
        var format = "yyyy-MM-dd";

        // Act
        var result = DateOnlyHelper.ParseExactInvariant(input, format);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 3, 15));
    }

    [Test]
    public async Task ParseExactInvariant_WithInvalidFormat_ThrowsException()
    {
        // Arrange
        var input = "15/03/2024";
        var format = "yyyy-MM-dd";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => DateOnlyHelper.ParseExactInvariant(input, format)));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15";
        var format = "yyyy-MM-dd";

        // Act
        var result = DateOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 3, 15));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithInvalidFormat_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var format = "yyyy-MM-dd";

        // Act
        var result = DateOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;
        var format = "yyyy-MM-dd";

        // Act
        var result = DateOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormats_UsesFirstMatch()
    {
        // Arrange
        var input = "15-03-2024";
        var formats = new[] { "yyyy-MM-dd", "dd-MM-yyyy" };

        // Act
        var result = DateOnlyHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 3, 15));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormatsNoMatch_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var formats = new[] { "yyyy-MM-dd", "dd-MM-yyyy" };

        // Act
        var result = DateOnlyHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsNull();
    }

    #endregion
}
