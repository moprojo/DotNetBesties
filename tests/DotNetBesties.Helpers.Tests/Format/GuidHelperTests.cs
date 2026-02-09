using System;
using System.Linq;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class GuidHelperTests
{
    [Test]
    public async Task Parse_ShouldReturnGuid()
    {
        var guidStr = "d7065963-c715-4fa1-92bb-92ac0658fa99";
        var result = GuidHelper.Parse(guidStr);
        await Assert.That(result).IsEqualTo(Guid.Parse(guidStr));
    }

    [Test]
    public async Task ParseOrNull_ValidString_ShouldReturnGuid()
    {
        var guidStr = "d7065963-c715-4fa1-92bb-92ac0658fa99";
        var result = GuidHelper.ParseOrNull(guidStr);
        await Assert.That(result).IsEqualTo(Guid.Parse(guidStr));
    }

    [Test]
    public async Task ParseOrNull_InvalidString_ShouldReturnNull()
    {
        var result = GuidHelper.ParseOrNull("invalid");
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Empty_ShouldReturnEmptyGuid()
    {
        var result = GuidHelper.Empty();
        await Assert.That(result).IsEqualTo(Guid.Empty);
    }

    [Test]
    public async Task NewGuid_ShouldReturnNewGuid()
    {
        var result = GuidHelper.NewGuid();
        await Assert.That(result).IsNotEqualTo(Guid.Empty);
    }

    [Test]
    public async Task IsEmpty_WithEmptyGuid_ReturnsTrue()
    {
        var result = GuidHelper.IsEmpty(Guid.Empty);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsEmpty_WithNonEmptyGuid_ReturnsFalse()
    {
        var result = GuidHelper.IsEmpty(Guid.NewGuid());
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNullOrEmpty_WithNull_ReturnsTrue()
    {
        Guid? guid = null;
        var result = GuidHelper.IsNullOrEmpty(guid);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithEmpty_ReturnsTrue()
    {
        Guid? guid = Guid.Empty;
        var result = GuidHelper.IsNullOrEmpty(guid);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithValue_ReturnsFalse()
    {
        Guid? guid = Guid.NewGuid();
        var result = GuidHelper.IsNullOrEmpty(guid);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToString_WithCharFormat_ReturnsFormattedString()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToString(guid, 'N');
        await Assert.That(result).IsEqualTo("12345678123412341234123456789abc");
    }

    [Test]
    public async Task ToStringN_ReturnsStringWithoutHyphens()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToStringN(guid);
        await Assert.That(result).IsEqualTo("12345678123412341234123456789abc");
        await Assert.That(result.Length).IsEqualTo(32);
    }

    [Test]
    public async Task ToStringD_ReturnsStringWithHyphens()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToStringD(guid);
        await Assert.That(result).IsEqualTo("12345678-1234-1234-1234-123456789abc");
        await Assert.That(result.Length).IsEqualTo(36);
    }

    [Test]
    public async Task ToStringB_ReturnsStringWithBraces()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToStringB(guid);
        await Assert.That(result).IsEqualTo("{12345678-1234-1234-1234-123456789abc}");
        await Assert.That(result.Length).IsEqualTo(38);
    }

    [Test]
    public async Task ToStringP_ReturnsStringWithParentheses()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToStringP(guid);
        await Assert.That(result).IsEqualTo("(12345678-1234-1234-1234-123456789abc)");
        await Assert.That(result.Length).IsEqualTo(38);
    }

    [Test]
    public async Task ToStringX_ReturnsHexArrayFormat()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToStringX(guid);
        await Assert.That(result).Contains("0x12345678");
        await Assert.That(result).Contains("0x1234");
    }

    [Test]
    public async Task ToBase64String_ReturnsBase64()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToBase64String(guid);

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Length).IsEqualTo(24); // Base64 of 16 bytes = 24 chars
    }

    [Test]
    public async Task ToBase64UrlString_ReturnsUrlSafeBase64()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var result = GuidHelper.ToBase64UrlString(guid);

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Length).IsEqualTo(22); // Base64 without padding
        await Assert.That(result).DoesNotContain("+");
        await Assert.That(result).DoesNotContain("/");
        await Assert.That(result).DoesNotContain("=");
    }

    [Test]
    public async Task ToShortString_Returns22CharString()
    {
        var guid = Guid.NewGuid();
        var result = GuidHelper.ToShortString(guid);

        await Assert.That(result.Length).IsEqualTo(22);
    }

    [Test]
    public async Task IsAnyOf_WithMatchingGuid_ReturnsTrue()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();

        var result = GuidHelper.IsAnyOf(guid2, guid1, guid2, guid3);

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsAnyOf_WithNonMatchingGuid_ReturnsFalse()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        var guid4 = Guid.NewGuid();

        var result = GuidHelper.IsAnyOf(guid4, guid1, guid2, guid3);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsAnyOf_WithNullArray_ThrowsArgumentNullException()
    {
        var guid = Guid.NewGuid();
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => GuidHelper.IsAnyOf(guid, null!)));
    }

    [Test]
    public async Task IsAnyOf_WithEmptyArray_ReturnsFalse()
    {
        var guid = Guid.NewGuid();
        var result = GuidHelper.IsAnyOf(guid);

        await Assert.That(result).IsFalse();
    }
}
