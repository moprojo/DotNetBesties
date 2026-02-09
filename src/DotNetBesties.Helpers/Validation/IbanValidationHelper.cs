using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating International Bank Account Numbers (IBAN).
/// </summary>
public static class IbanValidationHelper
{
    /// <summary>
    /// Validates an IBAN (International Bank Account Number).
    /// </summary>
    /// <param name="iban">The IBAN to validate.</param>
    /// <returns><c>true</c> if the IBAN is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIban(string? iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            return false;

        // Remove spaces and convert to uppercase
        var cleanedIban = Regex.Replace(iban, @"\s+", "").ToUpperInvariant();

        // IBAN must be between 15 and 34 characters
        if (cleanedIban.Length < 15 || cleanedIban.Length > 34)
            return false;

        // First two characters must be letters (country code)
        if (!char.IsLetter(cleanedIban[0]) || !char.IsLetter(cleanedIban[1]))
            return false;

        // Next two characters must be digits (check digits)
        if (!char.IsDigit(cleanedIban[2]) || !char.IsDigit(cleanedIban[3]))
            return false;

        // Remaining characters must be alphanumeric
        if (!cleanedIban.Skip(4).All(c => char.IsLetterOrDigit(c)))
            return false;

        // Validate using mod-97 algorithm
        return ValidateMod97(cleanedIban);
    }

    /// <summary>
    /// Formats an IBAN with spaces for better readability (groups of 4).
    /// </summary>
    /// <param name="iban">The IBAN to format.</param>
    /// <returns>The formatted IBAN.</returns>
    public static string FormatIban(string? iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            return string.Empty;

        var cleanedIban = Regex.Replace(iban, @"\s+", "").ToUpperInvariant();

        if (cleanedIban.Length < 4)
            return cleanedIban;

        var formatted = new StringBuilder();
        for (int i = 0; i < cleanedIban.Length; i++)
        {
            if (i > 0 && i % 4 == 0)
                formatted.Append(' ');
            formatted.Append(cleanedIban[i]);
        }

        return formatted.ToString();
    }

    /// <summary>
    /// Extracts the country code from an IBAN.
    /// </summary>
    /// <param name="iban">The IBAN to extract from.</param>
    /// <returns>The two-letter country code, or empty string if invalid.</returns>
    public static string GetCountryCode(string? iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            return string.Empty;

        var cleanedIban = Regex.Replace(iban, @"\s+", "").ToUpperInvariant();

        if (cleanedIban.Length < 2)
            return string.Empty;

        return cleanedIban.Substring(0, 2);
    }

    /// <summary>
    /// Normalizes an IBAN by removing spaces and converting to uppercase.
    /// </summary>
    /// <param name="iban">The IBAN to normalize.</param>
    /// <returns>The normalized IBAN.</returns>
    public static string Normalize(string? iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            return string.Empty;

        return Regex.Replace(iban, @"\s+", "").ToUpperInvariant();
    }

    private static bool ValidateMod97(string iban)
    {
        // Move first 4 characters to the end
        var rearranged = iban.Substring(4) + iban.Substring(0, 4);

        // Replace letters with numbers (A=10, B=11, ..., Z=35)
        var numericString = new StringBuilder();
        foreach (char c in rearranged)
        {
            if (char.IsDigit(c))
            {
                numericString.Append(c);
            }
            else if (char.IsLetter(c))
            {
                numericString.Append(c - 'A' + 10);
            }
        }

        // Calculate mod 97
        var remainder = CalculateMod97(numericString.ToString());
        return remainder == 1;
    }

    private static int CalculateMod97(string numericString)
    {
        // Process in chunks to avoid overflow
        var remainder = 0;
        foreach (char digit in numericString)
        {
            remainder = (remainder * 10 + (digit - '0')) % 97;
        }
        return remainder;
    }
}
