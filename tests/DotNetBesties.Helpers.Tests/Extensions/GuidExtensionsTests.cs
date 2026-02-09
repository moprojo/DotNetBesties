using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="GuidExtensions"/>.
/// </summary>
public class GuidExtensionsTests
{
    #region Queries

    [Test]
    public async Task IsEmpty_WithEmptyGuid_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.Empty;

        // Act
        var result = guid.IsEmpty();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsEmpty_WithNonEmptyGuid_ReturnsFalse()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = guid.IsEmpty();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNullOrEmpty_WithNull_ReturnsTrue()
    {
        // Arrange
        Guid? guid = null;

        // Act
        var result = guid.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithEmpty_ReturnsTrue()
    {
        // Arrange
        Guid? guid = Guid.Empty;

        // Act
        var result = guid.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithValue_ReturnsFalse()
    {
        // Arrange
        Guid? guid = Guid.NewGuid();

        // Act
        var result = guid.IsNullOrEmpty();

        // Assert
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Formatting

    [Test]
    public async Task ToString_WithFormatChar_ReturnsCorrectFormat()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToString('N');

        // Assert
        await Assert.That(result).IsEqualTo("12345678123412341234123456789abc");
    }

    [Test]
    public async Task ToString_WithFormatCharD_ReturnsCorrectFormat()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToString('D');

        // Assert
        await Assert.That(result).IsEqualTo("12345678-1234-1234-1234-123456789abc");
    }

    [Test]
    public async Task ToStringN_ReturnsFormatWithoutHyphens()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToStringN();

        // Assert
        await Assert.That(result).IsEqualTo("12345678123412341234123456789abc");
        await Assert.That(result.Length).IsEqualTo(32);
    }

    [Test]
    public async Task ToStringD_ReturnsFormatWithHyphens()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToStringD();

        // Assert
        await Assert.That(result).IsEqualTo("12345678-1234-1234-1234-123456789abc");
        await Assert.That(result.Length).IsEqualTo(36);
    }

    [Test]
    public async Task ToStringB_ReturnsFormatWithBraces()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToStringB();

        // Assert
        await Assert.That(result).IsEqualTo("{12345678-1234-1234-1234-123456789abc}");
        await Assert.That(result.Length).IsEqualTo(38);
    }

    [Test]
    public async Task ToStringP_ReturnsFormatWithParentheses()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToStringP();

        // Assert
        await Assert.That(result).IsEqualTo("(12345678-1234-1234-1234-123456789abc)");
        await Assert.That(result.Length).IsEqualTo(38);
    }

    [Test]
    public async Task ToStringX_ReturnsHexArrayFormat()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToStringX();

        // Assert
        await Assert.That(result).Contains("0x12345678");
        await Assert.That(result).Contains("0x1234");
    }

    #endregion

    #region Conversion

    [Test]
    public async Task ToBase64String_ReturnsBase64()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToBase64String();

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result.Length).IsEqualTo(24); // Base64 of 16 bytes = 24 chars
    }

    [Test]
    public async Task ToBase64UrlString_ReturnsUrlSafeBase64()
    {
        // Arrange
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        // Act
        var result = guid.ToBase64UrlString();

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result.Length).IsEqualTo(22); // Base64 without padding
        await Assert.That(result).DoesNotContain("+");
        await Assert.That(result).DoesNotContain("/");
        await Assert.That(result).DoesNotContain("=");
    }

    [Test]
    public async Task ToShortString_Returns22CharString()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = guid.ToShortString();

        // Assert
        await Assert.That(result.Length).IsEqualTo(22);
    }

    #endregion

    #region Comparison

    [Test]
    public async Task IsAnyOf_WithMatchingGuid_ReturnsTrue()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();

        // Act
        var result = guid2.IsAnyOf(guid1, guid2, guid3);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsAnyOf_WithNonMatchingGuid_ReturnsFalse()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        var guid4 = Guid.NewGuid();

        // Act
        var result = guid4.IsAnyOf(guid1, guid2, guid3);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsAnyOf_WithNullArray_ThrowsArgumentNullException()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => guid.IsAnyOf(null!)));
    }

    [Test]
    public async Task IsAnyOf_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = guid.IsAnyOf();

        // Assert
        await Assert.That(result).IsFalse();
    }

    #endregion
}
