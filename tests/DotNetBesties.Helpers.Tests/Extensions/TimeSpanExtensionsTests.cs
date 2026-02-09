using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="TimeSpanExtensions"/>.
/// </summary>
public class TimeSpanExtensionsTests
{
    #region Arithmetic

    [Test]
    public async Task Multiply_WithFactor_ReturnsMultiplied()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(10);

        // Act
        var result = ts.Multiply(2.5);

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(25));
    }

    [Test]
    public async Task Divide_WithDivisor_ReturnsDivided()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(30);

        // Act
        var result = ts.Divide(3);

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(10));
    }

    #endregion

    #region Conversion

    [Test]
    public async Task Abs_WithNegative_ReturnsPositive()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(-10);

        // Act
        var result = ts.Abs();

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(10));
    }

    [Test]
    public async Task Abs_WithPositive_ReturnsSame()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(10);

        // Act
        var result = ts.Abs();

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(10));
    }

    #endregion

    #region Queries

    [Test]
    public async Task IsZero_WithZero_ReturnsTrue()
    {
        // Arrange
        var ts = TimeSpan.Zero;

        // Act
        var result = ts.IsZero();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsPositive_WithPositiveValue_ReturnsTrue()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(10);

        // Act
        var result = ts.IsPositive();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNegative_WithNegativeValue_ReturnsTrue()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(-10);

        // Act
        var result = ts.IsNegative();

        // Assert
        await Assert.That(result).IsTrue();
    }

    #endregion

    #region Formatting

    [Test]
    public async Task ToHumanReadable_WithDaysAndHours_ReturnsReadableString()
    {
        // Arrange
        var ts = new TimeSpan(2, 3, 15, 30);

        // Act
        var result = ts.ToHumanReadable();

        // Assert
        await Assert.That(result).IsEqualTo("2 days, 3 hours");
    }

    [Test]
    public async Task ToHumanReadable_WithZero_ReturnsZeroSeconds()
    {
        // Arrange
        var ts = TimeSpan.Zero;

        // Act
        var result = ts.ToHumanReadable();

        // Assert
        await Assert.That(result).IsEqualTo("0 seconds");
    }

    [Test]
    public async Task ToHumanReadable_WithNegative_IncludesMinusSign()
    {
        // Arrange
        var ts = TimeSpan.FromHours(-2);

        // Act
        var result = ts.ToHumanReadable();

        // Assert
        await Assert.That(result).StartsWith("-");
    }

    [Test]
    public async Task ToCompactString_WithDaysAndHours_ReturnsCompactString()
    {
        // Arrange
        var ts = new TimeSpan(2, 3, 15, 0);

        // Act
        var result = ts.ToCompactString();

        // Assert
        await Assert.That(result).IsEqualTo("2d 3h 15m");
    }

    [Test]
    public async Task ToCompactString_WithZero_ReturnsZeroSeconds()
    {
        // Arrange
        var ts = TimeSpan.Zero;

        // Act
        var result = ts.ToCompactString();

        // Assert
        await Assert.That(result).IsEqualTo("0s");
    }

    #endregion

    #region Rounding

    [Test]
    public async Task Round_ToMinutes_RoundsCorrectly()
    {
        // Arrange
        var ts = TimeSpan.FromSeconds(90); // 1 minute 30 seconds

        // Act
        var result = ts.Round(TimeSpan.FromMinutes(1));

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(2));
    }

    [Test]
    public async Task Ceiling_ToMinutes_RoundsUp()
    {
        // Arrange
        var ts = TimeSpan.FromSeconds(61); // 1 minute 1 second

        // Act
        var result = ts.Ceiling(TimeSpan.FromMinutes(1));

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(2));
    }

    [Test]
    public async Task Floor_ToMinutes_RoundsDown()
    {
        // Arrange
        var ts = TimeSpan.FromSeconds(119); // 1 minute 59 seconds

        // Act
        var result = ts.Floor(TimeSpan.FromMinutes(1));

        // Assert
        await Assert.That(result).IsEqualTo(TimeSpan.FromMinutes(1));
    }

    [Test]
    public async Task Round_WithZeroInterval_ThrowsArgumentException()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(5);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await Task.Run(() => ts.Round(TimeSpan.Zero)));
    }

    #endregion
}
