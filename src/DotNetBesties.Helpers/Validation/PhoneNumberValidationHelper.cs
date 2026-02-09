using System;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating phone numbers.
/// </summary>
public static class PhoneNumberValidationHelper
{
    /// <summary>
    /// Validates a US phone number in various formats.
    /// Accepts formats like: (555) 555-5555, 555-555-5555, 5555555555, +1-555-555-5555
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns><c>true</c> if the phone number is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidUsPhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Pattern for US phone numbers in various formats
        var pattern = @"^(\+?1[-.\s]?)?(\()?[2-9]\d{2}(\))?[-.\s]?[2-9]\d{2}[-.\s]?\d{4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    /// <summary>
    /// Validates an international phone number in E.164 format.
    /// Format: +[country code][number] (e.g., +14155552671)
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns><c>true</c> if the phone number is valid E.164 format; otherwise, <c>false</c>.</returns>
    public static bool IsValidE164Format(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // E.164 format: + followed by 1-15 digits
        var pattern = @"^\+[1-9]\d{1,14}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    /// <summary>
    /// Validates a UK phone number.
    /// Accepts various UK formats including landline and mobile.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns><c>true</c> if the phone number is valid UK format; otherwise, <c>false</c>.</returns>
    public static bool IsValidUkPhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Remove all non-digit characters except +
        var cleaned = Regex.Replace(phoneNumber, @"[^\d+]", "");

        // UK numbers start with +44 or 0 and have 10-11 digits
        var pattern = @"^(\+44|0)[1-9]\d{8,9}$";
        return Regex.IsMatch(cleaned, pattern);
    }

    /// <summary>
    /// Extracts only the digits from a phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number to clean.</param>
    /// <returns>A string containing only the digits.</returns>
    public static string ExtractDigits(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;

        return Regex.Replace(phoneNumber, @"\D", "");
    }

    /// <summary>
    /// Formats a US phone number to (XXX) XXX-XXXX format.
    /// </summary>
    /// <param name="phoneNumber">The phone number to format.</param>
    /// <returns>The formatted phone number, or the original if invalid.</returns>
    public static string FormatUsPhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;

        var digits = ExtractDigits(phoneNumber);

        // Handle 10-digit numbers
        if (digits.Length == 10)
        {
            return $"({digits[..3]}) {digits[3..6]}-{digits[6..]}";
        }

        // Handle 11-digit numbers (with country code 1)
        if (digits.Length == 11 && digits[0] == '1')
        {
            return $"+1 ({digits[1..4]}) {digits[4..7]}-{digits[7..]}";
        }

        return phoneNumber; // Return original if not recognized
    }

    /// <summary>
    /// Formats a phone number to E.164 format for a given country code.
    /// </summary>
    /// <param name="phoneNumber">The phone number to format.</param>
    /// <param name="countryCode">The country code (e.g., "1" for US).</param>
    /// <returns>The formatted phone number in E.164 format.</returns>
    public static string FormatToE164(string? phoneNumber, string countryCode)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;

        ArgumentNullException.ThrowIfNull(countryCode);

        var digits = ExtractDigits(phoneNumber);

        if (string.IsNullOrWhiteSpace(digits))
            return string.Empty;

        // Remove country code if already present
        if (digits.StartsWith(countryCode))
            digits = digits[countryCode.Length..];

        return $"+{countryCode}{digits}";
    }

    /// <summary>
    /// Masks a phone number, showing only the last 4 digits.
    /// </summary>
    /// <param name="phoneNumber">The phone number to mask.</param>
    /// <param name="maskChar">The character to use for masking. Default is '*'.</param>
    /// <returns>The masked phone number.</returns>
    public static string Mask(string? phoneNumber, char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;

        var digits = ExtractDigits(phoneNumber);

        if (digits.Length <= 4)
            return new string(maskChar, digits.Length);

        var lastFour = digits[^4..];
        var masked = new string(maskChar, digits.Length - 4) + lastFour;

        return masked;
    }

    /// <summary>
    /// Validates that a phone number has a minimum number of digits.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <param name="minDigits">The minimum number of digits required. Default is 10.</param>
    /// <returns><c>true</c> if the phone number has at least the minimum digits; otherwise, <c>false</c>.</returns>
    public static bool HasMinimumDigits(string? phoneNumber, int minDigits = 10)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        var digits = ExtractDigits(phoneNumber);
        return digits.Length >= minDigits;
    }
}
