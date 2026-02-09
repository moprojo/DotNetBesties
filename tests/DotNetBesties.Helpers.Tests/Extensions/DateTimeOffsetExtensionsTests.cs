using System;
using System.Globalization;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="DateTimeOffsetExtensions"/>.
/// </summary>
public class DateTimeOffsetExtensionsTests
{
    #region TimeZone Conversion

    [Test]
    public async Task ConvertTime_ToUtc_ConvertsCorrectly()
    {
        // Arrange
        var dto = new DateTimeOffset(2024, 3, 15, 14, 30, 0, TimeSpan.FromHours(-5));
        var utcZone = TimeZoneInfo.Utc;

        // Act
        var result = dto.ConvertTime(utcZone);

        // Assert
        await Assert.That(result.Offset).IsEqualTo(TimeSpan.Zero);
        await Assert.That(result.Hour).IsEqualTo(19); // 14:30 -5 = 19:30 UTC
    }

    [Test]
    public async Task ConvertTime_BetweenTimeZones_ConvertsCorrectly()
    {
        // Arrange
        var dto = new DateTimeOffset(2024, 3, 15, 14, 30, 0, TimeSpan.Zero);
        var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        // Act
        var result = dto.ConvertTime(estZone);

        // Assert - EST is UTC-5 or UTC-4 depending on DST
        await Assert.That(result.UtcDateTime).IsEqualTo(dto.UtcDateTime); // UTC time should be same
    }

    [Test]
    public async Task ToOffset_ChangesOffset_MaintainsUtcTime()
    {
        // Arrange
        var dto = new DateTimeOffset(2024, 3, 15, 14, 30, 0, TimeSpan.FromHours(-5));
        var newOffset = TimeSpan.FromHours(2);

        // Act
        var result = dto.ToOffset(newOffset);

        // Assert
        await Assert.That(result.Offset).IsEqualTo(newOffset);
        await Assert.That(result.UtcDateTime).IsEqualTo(dto.UtcDateTime); // UTC time unchanged
        await Assert.That(result.Hour).IsEqualTo(21); // 14:30 -5 = 19:30 UTC = 21:30 +2
    }

    [Test]
    public async Task ToOffset_ToZeroOffset_ConvertsToUtc()
    {
        // Arrange
        var dto = new DateTimeOffset(2024, 3, 15, 14, 30, 0, TimeSpan.FromHours(-5));

        // Act
        var result = dto.ToOffset(TimeSpan.Zero);

        // Assert
        await Assert.That(result.Offset).IsEqualTo(TimeSpan.Zero);
        await Assert.That(result.Hour).IsEqualTo(19);
    }

    #endregion

    #region Unix Time

    [Test]
    public async Task ToUnixTimeMilliseconds_ReturnsCorrectValue()
    {
        // Arrange
        var dto = DateTimeOffset.FromUnixTimeMilliseconds(1000);

        // Act
        var result = dto.ToUnixTimeMilliseconds();

        // Assert
        await Assert.That(result).IsEqualTo(1000);
    }

    [Test]
    public async Task ToUnixTimeSeconds_ReturnsCorrectValue()
    {
        // Arrange
        var dto = DateTimeOffset.FromUnixTimeSeconds(3600);

        // Act
        var result = dto.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsEqualTo(3600);
    }

    [Test]
    public async Task ToUnixTimeSeconds_WithEpoch_ReturnsZero()
    {
        // Arrange
        var dto = DateTimeOffset.UnixEpoch;

        // Act
        var result = dto.ToUnixTimeSeconds();

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    #endregion

    #region Parsing

    [Test]
    public async Task ParseExactInvariant_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15T14:30:00+00:00";
        var format = "yyyy-MM-ddTHH:mm:sszzz";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariant(input, format);

        // Assert
        await Assert.That(result.Year).IsEqualTo(2024);
        await Assert.That(result.Month).IsEqualTo(3);
        await Assert.That(result.Day).IsEqualTo(15);
        await Assert.That(result.Hour).IsEqualTo(14);
        await Assert.That(result.Minute).IsEqualTo(30);
    }

    [Test]
    public async Task ParseExactInvariant_WithInvalidFormat_ThrowsException()
    {
        // Arrange
        var input = "invalid";
        var format = "yyyy-MM-ddTHH:mm:sszzz";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => DateTimeOffsetHelper.ParseExactInvariant(input, format)));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15T14:30:00+00:00";
        var format = "yyyy-MM-ddTHH:mm:sszzz";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Year).IsEqualTo(2024);
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithInvalidFormat_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var format = "yyyy-MM-ddTHH:mm:sszzz";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;
        var format = "yyyy-MM-ddTHH:mm:sszzz";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormats_UsesFirstMatch()
    {
        // Arrange
        var input = "2024-03-15 14:30:00";
        var formats = new[] { "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd HH:mm:ss" };

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Year).IsEqualTo(2024);
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormatsNoMatch_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var formats = new[] { "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd HH:mm:ss" };

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariant_WithDateTimeStyles_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15T14:30:00";
        var format = "yyyy-MM-ddTHH:mm:ss";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariant(input, format, DateTimeStyles.AssumeUniversal);

        // Assert
        await Assert.That(result.Year).IsEqualTo(2024);
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithDateTimeStyles_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15T14:30:00";
        var format = "yyyy-MM-ddTHH:mm:ss";

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, format, DateTimeStyles.AssumeUniversal);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Year).IsEqualTo(2024);
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormatsAndStyles_ParsesCorrectly()
    {
        // Arrange
        var input = "2024-03-15T14:30:00";
        var formats = new[] { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-ddTHH:mm:ss" };

        // Act
        var result = DateTimeOffsetHelper.ParseExactInvariantOrNull(input, formats, DateTimeStyles.AssumeUniversal);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    #endregion
}
