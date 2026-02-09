using System;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating postal codes from different countries.
/// </summary>
public static class PostalCodeValidationHelper
{
    /// <summary>
    /// Validates a US ZIP code (5 digits or 5+4 format).
    /// Accepts formats: 12345 or 12345-6789
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidUsZipCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{5}(-\d{4})?$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates a Canadian postal code.
    /// Format: A1A 1A1 (letter-digit-letter space digit-letter-digit)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidCanadianPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates a UK postal code.
    /// Accepts various UK postcode formats.
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidUkPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        // UK postcode format: A9 9AA, A99 9AA, AA9 9AA, AA99 9AA, A9A 9AA, AA9A 9AA
        var pattern = @"^[A-Z]{1,2}\d{1,2}[A-Z]?\s?\d[A-Z]{2}$";
        return Regex.IsMatch(postalCode.ToUpperInvariant(), pattern);
    }

    /// <summary>
    /// Validates a German postal code.
    /// Format: 5 digits (e.g., 12345)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidGermanPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{5}$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates a French postal code.
    /// Format: 5 digits (e.g., 75001)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidFrenchPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{5}$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates an Australian postal code.
    /// Format: 4 digits (e.g., 2000)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidAustralianPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{4}$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates an Indian postal code (PIN code).
    /// Format: 6 digits (e.g., 110001)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIndianPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{6}$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Validates a Japanese postal code.
    /// Format: 123-4567 (3 digits, hyphen, 4 digits)
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <returns><c>true</c> if the postal code is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidJapanesePostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return false;

        var pattern = @"^\d{3}-\d{4}$";
        return Regex.IsMatch(postalCode, pattern);
    }

    /// <summary>
    /// Formats a US ZIP code to 5 or 5+4 format.
    /// </summary>
    /// <param name="postalCode">The postal code to format.</param>
    /// <returns>The formatted postal code.</returns>
    public static string FormatUsZipCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return string.Empty;

        var digitsOnly = Regex.Replace(postalCode, @"\D", "");

        if (digitsOnly.Length == 5)
            return digitsOnly;

        if (digitsOnly.Length == 9)
            return $"{digitsOnly[..5]}-{digitsOnly[5..]}";

        return postalCode; // Return original if not recognized
    }

    /// <summary>
    /// Formats a Canadian postal code to A1A 1A1 format.
    /// </summary>
    /// <param name="postalCode">The postal code to format.</param>
    /// <returns>The formatted postal code.</returns>
    public static string FormatCanadianPostalCode(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return string.Empty;

        var cleaned = Regex.Replace(postalCode, @"[^A-Za-z0-9]", "").ToUpperInvariant();

        if (cleaned.Length == 6)
            return $"{cleaned[..3]} {cleaned[3..]}";

        return postalCode; // Return original if not recognized
    }

    /// <summary>
    /// Normalizes a postal code by removing spaces and converting to uppercase.
    /// </summary>
    /// <param name="postalCode">The postal code to normalize.</param>
    /// <returns>The normalized postal code.</returns>
    public static string Normalize(string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return string.Empty;

        return Regex.Replace(postalCode, @"\s+", "").ToUpperInvariant();
    }

    /// <summary>
    /// Validates a postal code for a specific country.
    /// </summary>
    /// <param name="postalCode">The postal code to validate.</param>
    /// <param name="countryCode">The ISO 3166-1 alpha-2 country code (e.g., "US", "CA", "GB").</param>
    /// <returns><c>true</c> if the postal code is valid for the country; otherwise, <c>false</c>.</returns>
    public static bool IsValidForCountry(string? postalCode, string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return false;

        return countryCode.ToUpperInvariant() switch
        {
            "US" => IsValidUsZipCode(postalCode),
            "CA" => IsValidCanadianPostalCode(postalCode),
            "GB" or "UK" => IsValidUkPostalCode(postalCode),
            "DE" => IsValidGermanPostalCode(postalCode),
            "FR" => IsValidFrenchPostalCode(postalCode),
            "AU" => IsValidAustralianPostalCode(postalCode),
            "IN" => IsValidIndianPostalCode(postalCode),
            "JP" => IsValidJapanesePostalCode(postalCode),
            _ => false
        };
    }
}
