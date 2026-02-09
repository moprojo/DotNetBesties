using System;
using System.Security.Cryptography;
using System.Text;

namespace DotNetBesties.Helpers.Cryptology;

/// <summary>
/// Provides helper methods for encryption, decryption, and hashing operations.
/// </summary>
public static class EncryptionHelper
{
    /// <summary>
    /// Encrypts a plain text string using AES encryption.
    /// </summary>
    /// <param name="plainText">The text to encrypt.</param>
    /// <param name="key">The encryption key (must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256).</param>
    /// <returns>A Base64-encoded string containing the IV and encrypted data separated by a colon.</returns>
    /// <exception cref="ArgumentNullException">Thrown when plainText or key is null.</exception>
    /// <exception cref="ArgumentException">Thrown when key length is not valid for AES.</exception>
    public static string EncryptAES(string plainText, string key)
    {
        ArgumentNullException.ThrowIfNull(plainText);
        ArgumentNullException.ThrowIfNull(key);

        var keyBytes = Encoding.UTF8.GetBytes(key);
        ValidateAesKeyLength(keyBytes.Length);

        using var aes = Aes.Create();
        aes.Key = keyBytes;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// Decrypts an AES-encrypted string.
    /// </summary>
    /// <param name="encryptedText">The encrypted text (IV:EncryptedData format).</param>
    /// <param name="key">The decryption key (must match the key used for encryption).</param>
    /// <returns>The decrypted plain text.</returns>
    /// <exception cref="ArgumentNullException">Thrown when encryptedText or key is null.</exception>
    /// <exception cref="ArgumentException">Thrown when key length is not valid for AES.</exception>
    /// <exception cref="FormatException">Thrown when encryptedText format is invalid.</exception>
    public static string DecryptAES(string encryptedText, string key)
    {
        ArgumentNullException.ThrowIfNull(encryptedText);
        ArgumentNullException.ThrowIfNull(key);

        var parts = encryptedText.Split(':');
        if (parts.Length != 2)
            throw new FormatException("Invalid encrypted text format. Expected 'IV:EncryptedData'.");

        var keyBytes = Encoding.UTF8.GetBytes(key);
        ValidateAesKeyLength(keyBytes.Length);

        var iv = Convert.FromBase64String(parts[0]);
        var encryptedBytes = Convert.FromBase64String(parts[1]);

        using var aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }

    /// <summary>
    /// Computes the SHA-256 hash of the input string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A Base64-encoded hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public static string HashSHA256(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Computes the SHA-256 hash of the input string and returns it as a hexadecimal string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A lowercase hexadecimal hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public static string HashSHA256Hex(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// Computes the SHA-512 hash of the input string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A Base64-encoded hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public static string HashSHA512(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA512.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Computes the SHA-512 hash of the input string and returns it as a hexadecimal string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A lowercase hexadecimal hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public static string HashSHA512Hex(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA512.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// Computes the MD5 hash of the input string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A Base64-encoded hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    /// <remarks>MD5 is considered cryptographically broken and should only be used for non-security purposes like checksums.</remarks>
    [Obsolete("MD5 is cryptographically broken. Use SHA-256 or SHA-512 for security-critical applications.")]
    public static string HashMD5(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = MD5.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Computes the MD5 hash of the input string and returns it as a hexadecimal string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>A lowercase hexadecimal hash string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    /// <remarks>MD5 is considered cryptographically broken and should only be used for non-security purposes like checksums.</remarks>
    [Obsolete("MD5 is cryptographically broken. Use SHA-256 or SHA-512 for security-critical applications.")]
    public static string HashMD5Hex(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = MD5.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    /// <summary>
    /// Generates a cryptographically secure random key of the specified length.
    /// </summary>
    /// <param name="length">The length of the key in bytes (default: 32 for AES-256).</param>
    /// <returns>A Base64-encoded random key.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when length is less than 1.</exception>
    public static string GenerateRandomKey(int length = 32)
    {
        if (length < 1)
            throw new ArgumentOutOfRangeException(nameof(length), "Key length must be at least 1 byte.");

        var key = RandomNumberGenerator.GetBytes(length);
        return Convert.ToBase64String(key);
    }

    /// <summary>
    /// Validates that the key length is valid for AES encryption.
    /// </summary>
    private static void ValidateAesKeyLength(int keyLength)
    {
        if (keyLength != 16 && keyLength != 24 && keyLength != 32)
            throw new ArgumentException(
                "Invalid AES key length. Key must be 16 bytes (AES-128), 24 bytes (AES-192), or 32 bytes (AES-256).",
                "key");
    }
}