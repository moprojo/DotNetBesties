using System;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="DoubleExtensions"/>.
/// </summary>
public class DoubleExtensionsTests
{
    #region Parsing

    [Test]
    public async Task ParseInvariant_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42.5";

        // Act
        var result = DoubleHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.5);
    }

    [Test]
    public async Task ParseInvariant_WithNegativeValue_ParsesCorrectly()
    {
        // Arrange
        var input = "-42.5";

        // Act
        var result = DoubleHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(-42.5);
    }

    [Test]
    public async Task ParseInvariant_WithIntegerValue_ParsesCorrectly()
    {
        // Arrange
        var input = "42";

        // Act
        var result = DoubleHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.0);
    }

    [Test]
    public async Task ParseInvariant_WithInvalidString_ThrowsException()
    {
        // Arrange
        var input = "invalid";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => DoubleHelper.ParseInvariant(input)));
    }

    [Test]
    public async Task ParseInvariant_WithScientificNotation_ParsesCorrectly()
    {
        // Arrange
        var input = "1.5E+3";

        // Act
        var result = DoubleHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(1500.0);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42.5";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.5);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInvalidString_ReturnsNull()
    {
        // Arrange
        var input = "invalid";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithEmptyString_ReturnsNull()
    {
        // Arrange
        var input = "";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithWhitespace_ReturnsNull()
    {
        // Arrange
        var input = "   ";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithZero_ParsesCorrectly()
    {
        // Arrange
        var input = "0";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(0.0);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithPositiveSign_ParsesCorrectly()
    {
        // Arrange
        var input = "+42.5";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(42.5);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithScientificNotation_ParsesCorrectly()
    {
        // Arrange
        var input = "1.5E+3";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsEqualTo(1500.0);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNaN_ParsesCorrectly()
    {
        // Arrange
        var input = "NaN";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(double.IsNaN(result!.Value)).IsTrue();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInfinity_ParsesCorrectly()
    {
        // Arrange
        var input = "Infinity";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(double.IsInfinity(result!.Value)).IsTrue();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNegativeInfinity_ParsesCorrectly()
    {
        // Arrange
        var input = "-Infinity";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(double.IsNegativeInfinity(result!.Value)).IsTrue();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithVeryLargeNumber_ParsesCorrectly()
    {
        // Arrange
        var input = "1.7976931348623157E+308";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value).IsGreaterThan(1E+308);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithVerySmallNumber_ParsesCorrectly()
    {
        // Arrange
        var input = "2.2250738585072014E-308";

        // Act
        var result = DoubleHelper.ParseInvariantOrNull(input);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value).IsLessThan(1E-307);
    }

    #endregion
}
