using System;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="DecimalExtensions"/>.
/// </summary>
public class DecimalExtensionsTests
{
    #region Mathematical Operations

    [Test]
    public async Task Abs_WithPositiveValue_ReturnsSameValue()
    {
        // Arrange
        var value = 42.5m;

        // Act
        var result = value.Abs();

        // Assert
        await Assert.That(result).IsEqualTo(42.5m);
    }

    [Test]
    public async Task Abs_WithNegativeValue_ReturnsPositiveValue()
    {
        // Arrange
        var value = -42.5m;

        // Act
        var result = value.Abs();

        // Assert
        await Assert.That(result).IsEqualTo(42.5m);
    }

    [Test]
    public async Task Abs_WithZero_ReturnsZero()
    {
        // Arrange
        var value = 0m;

        // Act
        var result = value.Abs();

        // Assert
        await Assert.That(result).IsEqualTo(0m);
    }

    [Test]
    public async Task Ceiling_WithDecimalValue_RoundsUp()
    {
        // Arrange
        var value = 42.3m;

        // Act
        var result = value.Ceiling();

        // Assert
        await Assert.That(result).IsEqualTo(43m);
    }

    [Test]
    public async Task Ceiling_WithIntegerValue_ReturnsSameValue()
    {
        // Arrange
        var value = 42m;

        // Act
        var result = value.Ceiling();

        // Assert
        await Assert.That(result).IsEqualTo(42m);
    }

    [Test]
    public async Task Ceiling_WithNegativeValue_RoundsTowardZero()
    {
        // Arrange
        var value = -42.7m;

        // Act
        var result = value.Ceiling();

        // Assert
        await Assert.That(result).IsEqualTo(-42m);
    }

    [Test]
    public async Task Floor_WithDecimalValue_RoundsDown()
    {
        // Arrange
        var value = 42.7m;

        // Act
        var result = value.Floor();

        // Assert
        await Assert.That(result).IsEqualTo(42m);
    }

    [Test]
    public async Task Floor_WithIntegerValue_ReturnsSameValue()
    {
        // Arrange
        var value = 42m;

        // Act
        var result = value.Floor();

        // Assert
        await Assert.That(result).IsEqualTo(42m);
    }

    [Test]
    public async Task Floor_WithNegativeValue_RoundsAwayFromZero()
    {
        // Arrange
        var value = -42.3m;

        // Act
        var result = value.Floor();

        // Assert
        await Assert.That(result).IsEqualTo(-43m);
    }

    [Test]
    public async Task Round_WithDefaultParameters_RoundsToInteger()
    {
        // Arrange
        var value = 42.6m;

        // Act
        var result = value.Round();

        // Assert
        await Assert.That(result).IsEqualTo(43m);
    }

    [Test]
    public async Task Round_WithDecimalPlaces_RoundsCorrectly()
    {
        // Arrange
        var value = 42.456m;

        // Act
        var result = value.Round(2);

        // Assert
        await Assert.That(result).IsEqualTo(42.46m);
    }

    [Test]
    public async Task Round_WithMidpointRounding_RoundsToEven()
    {
        // Arrange
        var value = 42.5m;

        // Act
        var result = value.Round(0, MidpointRounding.ToEven);

        // Assert
        await Assert.That(result).IsEqualTo(42m); // Rounds to even
    }

    [Test]
    public async Task Round_WithMidpointRoundingAwayFromZero_RoundsUp()
    {
        // Arrange
        var value = 42.5m;

        // Act
        var result = value.Round(0, MidpointRounding.AwayFromZero);

        // Assert
        await Assert.That(result).IsEqualTo(43m);
    }

    [Test]
    public async Task Truncate_RemovesFractionalPart()
    {
        // Arrange
        var value = 42.9m;

        // Act
        var result = value.Truncate();

        // Assert
        await Assert.That(result).IsEqualTo(42m);
    }

    [Test]
    public async Task Truncate_WithNegativeValue_RemovesFractionalPart()
    {
        // Arrange
        var value = -42.9m;

        // Act
        var result = value.Truncate();

        // Assert
        await Assert.That(result).IsEqualTo(-42m);
    }

    [Test]
    public async Task Truncate_WithIntegerValue_ReturnsSameValue()
    {
        // Arrange
        var value = 42m;

        // Act
        var result = value.Truncate();

        // Assert
        await Assert.That(result).IsEqualTo(42m);
    }

    #endregion

    #region Parsing

    [Test]
    public async Task ParseInvariant_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42.5";

        // Act
        var result = DecimalHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.5m);
    }

    [Test]
    public async Task ParseInvariant_WithNegativeValue_ParsesCorrectly()
    {
        // Arrange
        var input = "-42.5";

        // Act
        var result = DecimalHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(-42.5m);
    }

    [Test]
    public async Task ParseInvariant_WithInvalidString_ThrowsException()
    {
        // Arrange
        var input = "invalid";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => DecimalHelper.ParseInvariant(input)));
    }

    [Test]
    public async Task ParseInvariantOrNull_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42.5";

        // Act
        var result = DecimalHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.5m);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInvalidString_ReturnsNull()
    {
        // Arrange
        var input = "invalid";

        // Act
        var result = DecimalHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = DecimalHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithZero_ParsesCorrectly()
    {
        // Arrange
        var input = "0";

        // Act
        var result = DecimalHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(0m);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithLargeNumber_ParsesCorrectly()
    {
        // Arrange
        var input = "123456789.123456789";

        // Act
        var result = DecimalHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(123456789.123456789m);
    }

    #endregion
}
