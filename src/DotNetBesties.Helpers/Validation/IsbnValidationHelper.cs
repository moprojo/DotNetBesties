using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating International Standard Book Numbers (ISBN).
/// </summary>
public static class IsbnValidationHelper
{
    /// <summary>
    /// Validates an ISBN-10 or ISBN-13 number.
    /// </summary>
    /// <param name="isbn">The ISBN to validate.</param>
    /// <returns><c>true</c> if the ISBN is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIsbn(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        var cleanedIsbn = Normalize(isbn);

        return cleanedIsbn.Length switch
        {
            10 => IsValidIsbn10(cleanedIsbn),
            13 => IsValidIsbn13(cleanedIsbn),
            _ => false
        };
    }

    /// <summary>
    /// Validates an ISBN-10 number.
    /// </summary>
    /// <param name="isbn">The ISBN-10 to validate.</param>
    /// <returns><c>true</c> if the ISBN-10 is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIsbn10(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        var cleanedIsbn = Normalize(isbn);

        if (cleanedIsbn.Length != 10)
            return false;

        // First 9 characters must be digits, last can be digit or 'X'
        for (int i = 0; i < 9; i++)
        {
            if (!char.IsDigit(cleanedIsbn[i]))
                return false;
        }

        var lastChar = cleanedIsbn[9];
        if (!char.IsDigit(lastChar) && lastChar != 'X')
            return false;

        // Calculate check digit using ISBN-10 algorithm
        var sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (cleanedIsbn[i] - '0') * (10 - i);
        }

        var checkDigit = lastChar == 'X' ? 10 : (lastChar - '0');
        sum += checkDigit;

        return sum % 11 == 0;
    }

    /// <summary>
    /// Validates an ISBN-13 number.
    /// </summary>
    /// <param name="isbn">The ISBN-13 to validate.</param>
    /// <returns><c>true</c> if the ISBN-13 is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIsbn13(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        var cleanedIsbn = Normalize(isbn);

        if (cleanedIsbn.Length != 13)
            return false;

        // All characters must be digits
        if (!cleanedIsbn.All(char.IsDigit))
            return false;

        // Calculate check digit using ISBN-13 algorithm
        var sum = 0;
        for (int i = 0; i < 12; i++)
        {
            var digit = cleanedIsbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        var checkDigit = (10 - (sum % 10)) % 10;
        var actualCheckDigit = cleanedIsbn[12] - '0';

        return checkDigit == actualCheckDigit;
    }

    /// <summary>
    /// Converts an ISBN-10 to ISBN-13 format.
    /// </summary>
    /// <param name="isbn10">The ISBN-10 to convert.</param>
    /// <returns>The ISBN-13 equivalent, or empty string if invalid.</returns>
    public static string ConvertIsbn10ToIsbn13(string? isbn10)
    {
        if (string.IsNullOrWhiteSpace(isbn10))
            return string.Empty;

        var cleanedIsbn = Normalize(isbn10);

        if (!IsValidIsbn10(cleanedIsbn))
            return string.Empty;

        // Remove check digit and add 978 prefix
        var isbn13Base = "978" + cleanedIsbn.Substring(0, 9);

        // Calculate new check digit
        var sum = 0;
        for (int i = 0; i < 12; i++)
        {
            var digit = isbn13Base[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        var checkDigit = (10 - (sum % 10)) % 10;
        return isbn13Base + checkDigit;
    }

    /// <summary>
    /// Formats an ISBN with hyphens for better readability.
    /// </summary>
    /// <param name="isbn">The ISBN to format.</param>
    /// <returns>The formatted ISBN.</returns>
    public static string FormatIsbn(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return string.Empty;

        var cleanedIsbn = Normalize(isbn);

        // Simple formatting (full formatting would require prefix tables)
        if (cleanedIsbn.Length == 10)
        {
            // ISBN-10: X-XXX-XXXXX-X
            return $"{cleanedIsbn.Substring(0, 1)}-{cleanedIsbn.Substring(1, 3)}-{cleanedIsbn.Substring(4, 5)}-{cleanedIsbn.Substring(9, 1)}";
        }
        else if (cleanedIsbn.Length == 13)
        {
            // ISBN-13: XXX-X-XXX-XXXXX-X
            return $"{cleanedIsbn.Substring(0, 3)}-{cleanedIsbn.Substring(3, 1)}-{cleanedIsbn.Substring(4, 3)}-{cleanedIsbn.Substring(7, 5)}-{cleanedIsbn.Substring(12, 1)}";
        }

        return cleanedIsbn;
    }

    /// <summary>
    /// Normalizes an ISBN by removing spaces, hyphens, and converting to uppercase.
    /// </summary>
    /// <param name="isbn">The ISBN to normalize.</param>
    /// <returns>The normalized ISBN.</returns>
    public static string Normalize(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return string.Empty;

        return Regex.Replace(isbn, @"[\s\-]", "").ToUpperInvariant();
    }
}
