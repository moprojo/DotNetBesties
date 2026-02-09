using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating credit card numbers.
/// </summary>
public static class CreditCardValidationHelper
{
    /// <summary>
    /// Validates a credit card number using the Luhn algorithm (also known as modulus 10).
    /// </summary>
    /// <param name="cardNumber">The credit card number to validate.</param>
    /// <returns><c>true</c> if the card number is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidLuhn(string? cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        // Remove all non-digit characters
        var digitsOnly = Regex.Replace(cardNumber, @"\D", "");

        // Must have at least 13 digits (minimum for credit cards)
        if (digitsOnly.Length < 13)
            return false;

        // Apply Luhn algorithm
        int sum = 0;
        bool alternate = false;

        // Process digits from right to left
        for (int i = digitsOnly.Length - 1; i >= 0; i--)
        {
            int digit = digitsOnly[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }

    /// <summary>
    /// Determines the type of credit card based on the card number pattern.
    /// </summary>
    /// <param name="cardNumber">The credit card number.</param>
    /// <returns>The card type or "Unknown" if not recognized.</returns>
    public static string GetCardType(string? cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return "Unknown";

        var digitsOnly = Regex.Replace(cardNumber, @"\D", "");

        if (Regex.IsMatch(digitsOnly, @"^4[0-9]{12}(?:[0-9]{3})?$"))
            return "Visa";

        if (Regex.IsMatch(digitsOnly, @"^5[1-5][0-9]{14}$"))
            return "MasterCard";

        if (Regex.IsMatch(digitsOnly, @"^3[47][0-9]{13}$"))
            return "American Express";

        if (Regex.IsMatch(digitsOnly, @"^6(?:011|5[0-9]{2})[0-9]{12}$"))
            return "Discover";

        if (Regex.IsMatch(digitsOnly, @"^(?:2131|1800|35\d{3})\d{11}$"))
            return "JCB";

        if (Regex.IsMatch(digitsOnly, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"))
            return "Diners Club";

        return "Unknown";
    }

    /// <summary>
    /// Validates if the card number matches a specific card type pattern.
    /// </summary>
    /// <param name="cardNumber">The credit card number.</param>
    /// <param name="cardType">The expected card type (Visa, MasterCard, etc.).</param>
    /// <returns><c>true</c> if the card matches the type; otherwise, <c>false</c>.</returns>
    public static bool IsCardType(string? cardNumber, string cardType)
    {
        return GetCardType(cardNumber).Equals(cardType, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Masks a credit card number, showing only the last 4 digits.
    /// </summary>
    /// <param name="cardNumber">The credit card number to mask.</param>
    /// <param name="maskChar">The character to use for masking. Default is '*'.</param>
    /// <returns>The masked card number.</returns>
    public static string Mask(string? cardNumber, char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return string.Empty;

        var digitsOnly = Regex.Replace(cardNumber, @"\D", "");

        if (digitsOnly.Length <= 4)
            return new string(maskChar, digitsOnly.Length);

        var lastFour = digitsOnly[^4..];
        var masked = new string(maskChar, digitsOnly.Length - 4) + lastFour;

        return masked;
    }

    /// <summary>
    /// Formats a credit card number with spaces every 4 digits.
    /// </summary>
    /// <param name="cardNumber">The credit card number to format.</param>
    /// <returns>The formatted card number.</returns>
    public static string Format(string? cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return string.Empty;

        var digitsOnly = Regex.Replace(cardNumber, @"\D", "");

        // Format as XXXX XXXX XXXX XXXX (or appropriate for card length)
        return Regex.Replace(digitsOnly, @"(\d{4})", "$1 ").Trim();
    }
}
