using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Cryptology;

namespace DotNetBesties.Helpers.Tests.Cryptology;

public class EncryptionHelperTests
{
    private const string ValidKey16 = "1234567890123456"; // 16-byte key for AES-128
    private const string ValidKey24 = "123456789012345678901234"; // 24-byte key for AES-192
    private const string ValidKey32 = "12345678901234567890123456789012"; // 32-byte key for AES-256

    #region EncryptAES/DecryptAES Tests

    [Test]
    public async Task EncryptAES_DecryptAES_RoundTrip_ReturnsOriginalText()
    {
        var plainText = "Hello World";

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey16);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey16);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_DecryptAES_WithAES192_Works()
    {
        var plainText = "Test with AES-192";

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey24);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey24);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_DecryptAES_WithAES256_Works()
    {
        var plainText = "Test with AES-256";

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey32);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey32);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_WithEmptyString_Works()
    {
        var plainText = "";

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey16);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey16);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_WithUnicodeText_Works()
    {
        var plainText = "Héllo Wörld ?? ??";

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey16);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey16);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_WithLongText_Works()
    {
        var plainText = new string('a', 10000);

        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey16);
        var decrypted = EncryptionHelper.DecryptAES(encrypted, ValidKey16);

        await Assert.That(decrypted).IsEqualTo(plainText);
    }

    [Test]
    public async Task EncryptAES_ProducesDifferentOutputEachTime()
    {
        var plainText = "Hello World";

        var encrypted1 = EncryptionHelper.EncryptAES(plainText, ValidKey16);
        var encrypted2 = EncryptionHelper.EncryptAES(plainText, ValidKey16);

        // Different IVs should produce different encrypted results
        await Assert.That(encrypted1).IsNotEqualTo(encrypted2);
    }

    [Test]
    public async Task EncryptAES_WithNullPlainText_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.EncryptAES(null!, ValidKey16)));
    }

    [Test]
    public async Task EncryptAES_WithNullKey_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.EncryptAES("test", null!)));
    }

    [Test]
    public async Task EncryptAES_WithInvalidKeyLength_ThrowsException()
    {
        var invalidKeys = new[] { "short", "1234567890123", "12345678901234567", "123456789012345678901" };

        foreach (var key in invalidKeys)
        {
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await Task.Run(() => EncryptionHelper.EncryptAES("test", key)));
        }
    }

    [Test]
    public async Task DecryptAES_WithWrongKey_ThrowsOrReturnsGarbage()
    {
        var plainText = "Hello World";
        var encrypted = EncryptionHelper.EncryptAES(plainText, ValidKey16);

        // Attempting to decrypt with wrong key should fail
        await Assert.ThrowsAsync<Exception>(
            async () => await Task.Run(() => EncryptionHelper.DecryptAES(encrypted, "0000000000000000")));
    }

    [Test]
    public async Task DecryptAES_WithInvalidFormat_ThrowsException()
    {
        var invalidFormats = new[] { "invalid", "no:colon:here::", "onlyonepart", "" };

        foreach (var format in invalidFormats)
        {
            await Assert.ThrowsAsync<FormatException>(
                async () => await Task.Run(() => EncryptionHelper.DecryptAES(format, ValidKey16)));
        }
    }

    [Test]
    public async Task DecryptAES_WithNullEncryptedText_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.DecryptAES(null!, ValidKey16)));
    }

    [Test]
    public async Task DecryptAES_WithNullKey_ThrowsException()
    {
        var encrypted = EncryptionHelper.EncryptAES("test", ValidKey16);
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.DecryptAES(encrypted, null!)));
    }

    [Test]
    public async Task EncryptAES_OutputContainsColon()
    {
        var encrypted = EncryptionHelper.EncryptAES("test", ValidKey16);
        await Assert.That(encrypted).Contains(":");
    }

    #endregion

    #region HashSHA256 Tests

    [Test]
    public async Task HashSHA256_ReturnsConsistentHash()
    {
        var input = "Hello World";
        var hash1 = EncryptionHelper.HashSHA256(input);
        var hash2 = EncryptionHelper.HashSHA256(input);

        await Assert.That(hash1).IsEqualTo(hash2);
    }

    [Test]
    public async Task HashSHA256_DifferentInputs_ReturnsDifferentHashes()
    {
        var hash1 = EncryptionHelper.HashSHA256("Hello");
        var hash2 = EncryptionHelper.HashSHA256("World");

        await Assert.That(hash1).IsNotEqualTo(hash2);
    }

    [Test]
    public async Task HashSHA256_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashSHA256(null!)));
    }

    [Test]
    public async Task HashSHA256_ReturnsBase64String()
    {
        var hash = EncryptionHelper.HashSHA256("test");
        
        // SHA-256 produces 32 bytes, Base64 encoded = 44 characters (with padding)
        await Assert.That(hash.Length).IsEqualTo(44);
    }

    [Test]
    public async Task HashSHA256_WithEmptyString_ReturnsValidHash()
    {
        var hash = EncryptionHelper.HashSHA256("");
        await Assert.That(hash).IsNotNull();
        await Assert.That(hash.Length).IsEqualTo(44);
    }

    #endregion

    #region HashSHA256Hex Tests

    [Test]
    public async Task HashSHA256Hex_ReturnsLowercaseHexString()
    {
        var hash = EncryptionHelper.HashSHA256Hex("test");

        // SHA-256 produces 32 bytes = 64 hex characters
        await Assert.That(hash.Length).IsEqualTo(64);
        await Assert.That(hash).IsEqualTo(hash.ToLowerInvariant());
    }

    [Test]
    public async Task HashSHA256Hex_WithKnownInput_ReturnsExpectedHash()
    {
        // Known SHA-256 hash of "test"
        var expected = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
        var hash = EncryptionHelper.HashSHA256Hex("test");

        await Assert.That(hash).IsEqualTo(expected);
    }

    [Test]
    public async Task HashSHA256Hex_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashSHA256Hex(null!)));
    }

    [Test]
    public async Task HashSHA256Hex_ContainsOnlyHexCharacters()
    {
        var hash = EncryptionHelper.HashSHA256Hex("test");
        var isHex = System.Text.RegularExpressions.Regex.IsMatch(hash, "^[0-9a-f]+$");
        await Assert.That(isHex).IsTrue();
    }

    #endregion

    #region HashSHA512 Tests

    [Test]
    public async Task HashSHA512_ReturnsConsistentHash()
    {
        var input = "Hello World";
        var hash1 = EncryptionHelper.HashSHA512(input);
        var hash2 = EncryptionHelper.HashSHA512(input);

        await Assert.That(hash1).IsEqualTo(hash2);
    }

    [Test]
    public async Task HashSHA512_ReturnsBase64String()
    {
        var hash = EncryptionHelper.HashSHA512("test");

        // SHA-512 produces 64 bytes, Base64 encoded = 88 characters (with padding)
        await Assert.That(hash.Length).IsEqualTo(88);
    }

    [Test]
    public async Task HashSHA512_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashSHA512(null!)));
    }

    [Test]
    public async Task HashSHA512_DifferentFromSHA256()
    {
        var input = "test";
        var sha256 = EncryptionHelper.HashSHA256(input);
        var sha512 = EncryptionHelper.HashSHA512(input);

        await Assert.That(sha256).IsNotEqualTo(sha512);
    }

    #endregion

    #region HashSHA512Hex Tests

    [Test]
    public async Task HashSHA512Hex_ReturnsLowercaseHexString()
    {
        var hash = EncryptionHelper.HashSHA512Hex("test");

        // SHA-512 produces 64 bytes = 128 hex characters
        await Assert.That(hash.Length).IsEqualTo(128);
        await Assert.That(hash).IsEqualTo(hash.ToLowerInvariant());
    }

    [Test]
    public async Task HashSHA512Hex_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashSHA512Hex(null!)));
    }

    [Test]
    public async Task HashSHA512Hex_ContainsOnlyHexCharacters()
    {
        var hash = EncryptionHelper.HashSHA512Hex("test");
        var isHex = System.Text.RegularExpressions.Regex.IsMatch(hash, "^[0-9a-f]+$");
        await Assert.That(isHex).IsTrue();
    }

    #endregion

    #region HashMD5 Tests

#pragma warning disable CS0618 // Type or member is obsolete
    [Test]
    public async Task HashMD5_ReturnsConsistentHash()
    {
        var input = "Hello World";
        var hash1 = EncryptionHelper.HashMD5(input);
        var hash2 = EncryptionHelper.HashMD5(input);

        await Assert.That(hash1).IsEqualTo(hash2);
    }

    [Test]
    public async Task HashMD5_ReturnsBase64String()
    {
        var hash = EncryptionHelper.HashMD5("test");

        // MD5 produces 16 bytes, Base64 encoded = 24 characters (with padding)
        await Assert.That(hash.Length).IsEqualTo(24);
    }

    [Test]
    public async Task HashMD5_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashMD5(null!)));
    }

    [Test]
    public async Task HashMD5Hex_ReturnsLowercaseHexString()
    {
        var hash = EncryptionHelper.HashMD5Hex("test");

        // MD5 produces 16 bytes = 32 hex characters
        await Assert.That(hash.Length).IsEqualTo(32);
    }

    [Test]
    public async Task HashMD5Hex_WithKnownInput_ReturnsExpectedHash()
    {
        // Known MD5 hash of "test"
        var expected = "098f6bcd4621d373cade4e832627b4f6";
        var hash = EncryptionHelper.HashMD5Hex("test");

        await Assert.That(hash).IsEqualTo(expected);
    }

    [Test]
    public async Task HashMD5Hex_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => EncryptionHelper.HashMD5Hex(null!)));
    }
#pragma warning restore CS0618

    #endregion

    #region GenerateRandomKey Tests

    [Test]
    public async Task GenerateRandomKey_ReturnsNonEmptyString()
    {
        var key = EncryptionHelper.GenerateRandomKey();

        await Assert.That(key).IsNotNull();
        await Assert.That(key.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GenerateRandomKey_ReturnsUniqueKeys()
    {
        var key1 = EncryptionHelper.GenerateRandomKey();
        var key2 = EncryptionHelper.GenerateRandomKey();

        await Assert.That(key1).IsNotEqualTo(key2);
    }

    [Test]
    public async Task GenerateRandomKey_WithCustomLength_ReturnsCorrectLength()
    {
        var key = EncryptionHelper.GenerateRandomKey(16);

        // 16 bytes = 24 characters in Base64 (with padding)
        var decoded = Convert.FromBase64String(key);
        await Assert.That(decoded.Length).IsEqualTo(16);
    }

    [Test]
    public async Task GenerateRandomKey_WithZeroLength_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => EncryptionHelper.GenerateRandomKey(0)));
    }

    [Test]
    public async Task GenerateRandomKey_WithNegativeLength_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => EncryptionHelper.GenerateRandomKey(-1)));
    }

    [Test]
    public async Task GenerateRandomKey_Default32Bytes_Works()
    {
        var key = EncryptionHelper.GenerateRandomKey();
        var decoded = Convert.FromBase64String(key);
        await Assert.That(decoded.Length).IsEqualTo(32);
    }

    [Test]
    public async Task GenerateRandomKey_MultipleGenerations_ProduceUniqueKeys()
    {
        var keys = new HashSet<string>();
        for (int i = 0; i < 100; i++)
        {
            keys.Add(EncryptionHelper.GenerateRandomKey());
        }
        await Assert.That(keys).HasCount(100);
    }

    #endregion
}