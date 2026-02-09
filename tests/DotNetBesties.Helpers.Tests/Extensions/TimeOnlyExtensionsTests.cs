using System;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="TimeOnlyExtensions"/>.
/// </summary>
public class TimeOnlyExtensionsTests
{
    #region Time Manipulation

    [Test]
    public async Task AddHours_WithPositiveHours_AddsCorrectly()
    {
        // Arrange
        var time = new TimeOnly(10, 30, 0);

        // Act
        var result = time.AddHours(5);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(15, 30, 0));
    }

    [Test]
    public async Task AddHours_WithNegativeHours_SubtractsCorrectly()
    {
        // Arrange
        var time = new TimeOnly(10, 30, 0);

        // Act
        var result = time.AddHours(-5);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(5, 30, 0));
    }

    [Test]
    public async Task AddHours_WrapsAroundMidnight()
    {
        // Arrange
        var time = new TimeOnly(22, 0, 0);

        // Act
        var result = time.AddHours(5);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(3, 0, 0));
    }

    [Test]
    public async Task AddMinutes_WithPositiveMinutes_AddsCorrectly()
    {
        // Arrange
        var time = new TimeOnly(10, 30, 0);

        // Act
        var result = time.AddMinutes(45);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(11, 15, 0));
    }

    [Test]
    public async Task AddMinutes_WithNegativeMinutes_SubtractsCorrectly()
    {
        // Arrange
        var time = new TimeOnly(10, 30, 0);

        // Act
        var result = time.AddMinutes(-45);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(9, 45, 0));
    }

    [Test]
    public async Task AddMinutes_WrapsAroundMidnight()
    {
        // Arrange
        var time = new TimeOnly(23, 50, 0);

        // Act
        var result = time.AddMinutes(20);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(0, 10, 0));
    }

    #endregion

    #region Conversion

    [Test]
    public async Task ToTimeSpan_ConvertsCorrectly()
    {
        // Arrange
        var time = new TimeOnly(14, 30, 45);

        // Act
        var result = time.ToTimeSpan();

        // Assert
        await Assert.That(result.Hours).IsEqualTo(14);
        await Assert.That(result.Minutes).IsEqualTo(30);
        await Assert.That(result.Seconds).IsEqualTo(45);
    }

    [Test]
    public async Task ToTimeSpan_WithMidnight_ReturnsZero()
    {
        // Arrange
        var time = new TimeOnly(0, 0, 0);

        // Act
        var result = time.ToTimeSpan();

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    public async Task ToTimeSpan_WithNullValue_ReturnsNull()
    {
        // Arrange
        TimeOnly? time = null;

        // Act
        var result = time.ToTimeSpan();

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ToTimeSpan_WithNullableValue_ReturnsTimeSpan()
    {
        // Arrange
        TimeOnly? time = new TimeOnly(14, 30, 45);

        // Act
        var result = time.ToTimeSpan();

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Hours).IsEqualTo(14);
    }

    #endregion

    #region Parsing

    [Test]
    public async Task ParseExactInvariant_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "14:30:45";
        var format = "HH:mm:ss";

        // Act
        var result = TimeOnlyHelper.ParseExactInvariant(input, format);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30, 45));
    }

    [Test]
    public async Task ParseExactInvariant_WithInvalidFormat_ThrowsException()
    {
        // Arrange
        var input = "2:30 PM";
        var format = "HH:mm:ss";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => TimeOnlyHelper.ParseExactInvariant(input, format)));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithValidFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "14:30:45";
        var format = "HH:mm:ss";

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30, 45));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithInvalidFormat_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var format = "HH:mm:ss";

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;
        var format = "HH:mm:ss";

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormats_UsesFirstMatch()
    {
        // Arrange
        var input = "2:30 PM";
        var formats = new[] { "HH:mm:ss", "h:mm tt" };

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(14, 30, 0));
    }

    [Test]
    public async Task ParseExactInvariantOrNull_WithMultipleFormatsNoMatch_ReturnsNull()
    {
        // Arrange
        var input = "invalid";
        var formats = new[] { "HH:mm:ss", "h:mm tt" };

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, formats);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseExactInvariantOrNull_With24HourFormat_ParsesCorrectly()
    {
        // Arrange
        var input = "23:59:59";
        var format = "HH:mm:ss";

        // Act
        var result = TimeOnlyHelper.ParseExactInvariantOrNull(input, format);

        // Assert
        await Assert.That(result).IsEqualTo(new TimeOnly(23, 59, 59));
    }

    #endregion
}
