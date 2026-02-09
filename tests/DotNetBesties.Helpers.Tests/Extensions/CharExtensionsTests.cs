using System.Globalization;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="CharExtensions"/>.
/// </summary>
public class CharExtensionsTests
{
    #region Case Conversion

    [Test]
    public async Task ToLowerInvariant_WithUpperCase_ReturnsLowerCase()
    {
        // Arrange
        var input = 'A';

        // Act
        var result = input.ToLowerInvariant();

        // Assert
        await Assert.That(result).IsEqualTo('a');
    }

    [Test]
    public async Task ToLowerInvariant_WithLowerCase_ReturnsSame()
    {
        // Arrange
        var input = 'a';

        // Act
        var result = input.ToLowerInvariant();

        // Assert
        await Assert.That(result).IsEqualTo('a');
    }

    [Test]
    public async Task ToLowerInvariant_WithCulture_UsesSpecifiedCulture()
    {
        // Arrange
        var input = 'I';
        var culture = new CultureInfo("tr-TR"); // Turkish

        // Act
        var result = input.ToLowerInvariant(culture);

        // Assert
        // In Turkish, uppercase I becomes lowercase ? (dotless i)
        await Assert.That(result).IsEqualTo('\u0131'); // Unicode for ?
    }

    [Test]
    public async Task ToUpperInvariant_WithLowerCase_ReturnsUpperCase()
    {
        // Arrange
        var input = 'a';

        // Act
        var result = input.ToUpperInvariant();

        // Assert
        await Assert.That(result).IsEqualTo('A');
    }

    [Test]
    public async Task ToUpperInvariant_WithUpperCase_ReturnsSame()
    {
        // Arrange
        var input = 'A';

        // Act
        var result = input.ToUpperInvariant();

        // Assert
        await Assert.That(result).IsEqualTo('A');
    }

    [Test]
    public async Task ToUpperInvariant_WithCulture_UsesSpecifiedCulture()
    {
        // Arrange
        var input = 'i';
        var culture = new CultureInfo("tr-TR"); // Turkish

        // Act
        var result = input.ToUpperInvariant(culture);

        // Assert
        // In Turkish, lowercase i becomes uppercase ? (dotted I)
        await Assert.That(result).IsEqualTo('\u0130'); // Unicode for ?
    }

    #endregion

    #region Character Classification

    [Test]
    [Arguments('a')]
    [Arguments('e')]
    [Arguments('i')]
    [Arguments('o')]
    [Arguments('u')]
    [Arguments('A')]
    [Arguments('E')]
    [Arguments('I')]
    [Arguments('O')]
    [Arguments('U')]
    public async Task IsVowel_WithVowel_ReturnsTrue(char input)
    {
        // Act
        var result = input.IsVowel();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments('b')]
    [Arguments('c')]
    [Arguments('z')]
    [Arguments('1')]
    [Arguments(' ')]
    public async Task IsVowel_WithNonVowel_ReturnsFalse(char input)
    {
        // Act
        var result = input.IsVowel();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments('b')]
    [Arguments('c')]
    [Arguments('z')]
    [Arguments('B')]
    [Arguments('Z')]
    public async Task IsConsonant_WithConsonant_ReturnsTrue(char input)
    {
        // Act
        var result = input.IsConsonant();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments('a')]
    [Arguments('e')]
    [Arguments('1')]
    [Arguments(' ')]
    public async Task IsConsonant_WithNonConsonant_ReturnsFalse(char input)
    {
        // Act
        var result = input.IsConsonant();

        // Assert
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Repetition

    [Test]
    public async Task Repeat_WithPositiveCount_ReturnsRepeatedString()
    {
        // Arrange
        var input = 'A';

        // Act
        var result = input.Repeat(5);

        // Assert
        await Assert.That(result).IsEqualTo("AAAAA");
    }

    [Test]
    public async Task Repeat_WithZeroCount_ReturnsEmptyString()
    {
        // Arrange
        var input = 'A';

        // Act
        var result = input.Repeat(0);

        // Assert
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Repeat_WithNegativeCount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var input = 'A';

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => input.Repeat(-1)));
    }

    #endregion

    #region Comparison

    [Test]
    [Arguments('c', 'a', 'e', true)]
    [Arguments('a', 'a', 'e', true)]
    [Arguments('e', 'a', 'e', true)]
    [Arguments('f', 'a', 'e', false)]
    [Arguments('z', 'a', 'e', false)]
    public async Task IsInRange_WithVariousInputs_ReturnsExpectedResult(char value, char start, char end, bool expected)
    {
        // Act
        var result = value.IsInRange(start, end);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task IsAnyOf_WithMatchingChar_ReturnsTrue()
    {
        // Arrange
        var input = 'b';

        // Act
        var result = input.IsAnyOf('a', 'b', 'c');

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsAnyOf_WithNonMatchingChar_ReturnsFalse()
    {
        // Arrange
        var input = 'd';

        // Act
        var result = input.IsAnyOf('a', 'b', 'c');

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsAnyOf_WithNullArray_ThrowsArgumentNullException()
    {
        // Arrange
        var input = 'a';

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => input.IsAnyOf(null!)));
    }

    [Test]
    public async Task IsAnyOf_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        var input = 'a';

        // Act
        var result = input.IsAnyOf();

        // Assert
        await Assert.That(result).IsFalse();
    }

    #endregion
}
