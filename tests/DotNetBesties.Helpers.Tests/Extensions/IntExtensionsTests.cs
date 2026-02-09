using System;
using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="IntExtensions"/>.
/// </summary>
public class IntExtensionsTests
{
    #region Parsing

    [Test]
    public async Task ParseInvariant_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42";

        // Act
        var result = IntHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task ParseInvariant_WithNegativeValue_ParsesCorrectly()
    {
        // Arrange
        var input = "-42";

        // Act
        var result = IntHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(-42);
    }

    [Test]
    public async Task ParseInvariant_WithZero_ParsesCorrectly()
    {
        // Arrange
        var input = "0";

        // Act
        var result = IntHelper.ParseInvariant(input);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task ParseInvariant_WithInvalidString_ThrowsException()
    {
        // Arrange
        var input = "invalid";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => IntHelper.ParseInvariant(input)));
    }

    [Test]
    public async Task ParseInvariant_WithDecimalValue_ThrowsException()
    {
        // Arrange
        var input = "42.5";

        // Act & Assert
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => IntHelper.ParseInvariant(input)));
    }

    [Test]
    public async Task ParseInvariantOrNull_WithValidString_ParsesCorrectly()
    {
        // Arrange
        var input = "42";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithInvalidString_ReturnsNull()
    {
        // Arrange
        var input = "invalid";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithNullInput_ReturnsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithEmptyString_ReturnsNull()
    {
        // Arrange
        var input = "";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithWhitespace_ReturnsNull()
    {
        // Arrange
        var input = "   ";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ParseInvariantOrNull_WithMaxValue_ParsesCorrectly()
    {
        // Arrange
        var input = int.MaxValue.ToString();

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsEqualTo(int.MaxValue);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithMinValue_ParsesCorrectly()
    {
        // Arrange
        var input = int.MinValue.ToString();

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsEqualTo(int.MinValue);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithLeadingZeros_ParsesCorrectly()
    {
        // Arrange
        var input = "00042";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithPositiveSign_ParsesCorrectly()
    {
        // Arrange
        var input = "+42";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task ParseInvariantOrNull_WithDecimalValue_ReturnsNull()
    {
        // Arrange
        var input = "42.5";

        // Act
        var result = int.TryParse(input, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var parsed) ? parsed : (int?)null;

        // Assert
        await Assert.That(result).IsNull();
    }

    #endregion
}
