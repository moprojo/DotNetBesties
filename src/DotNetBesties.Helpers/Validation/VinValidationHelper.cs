using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Helper methods for validating Vehicle Identification Numbers (VIN).
/// </summary>
public static class VinValidationHelper
{
    private static readonly char[] InvalidChars = { 'I', 'O', 'Q' };
    private static readonly int[] Weights = { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };

    /// <summary>
    /// Validates a Vehicle Identification Number (VIN).
    /// VINs are 17 characters long and use a check digit algorithm.
    /// </summary>
    /// <param name="vin">The VIN to validate.</param>
    /// <returns><c>true</c> if the VIN is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidVin(string? vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return false;

        var cleanedVin = Normalize(vin);

        // VIN must be exactly 17 characters
        if (cleanedVin.Length != 17)
            return false;

        // Must be alphanumeric
        if (!cleanedVin.All(c => char.IsLetterOrDigit(c)))
            return false;

        // Must not contain I, O, or Q
        if (cleanedVin.Any(c => InvalidChars.Contains(c)))
            return false;

        // Validate check digit (9th character)
        return ValidateCheckDigit(cleanedVin);
    }

    /// <summary>
    /// Validates a VIN and extracts basic information.
    /// </summary>
    /// <param name="vin">The VIN to validate.</param>
    /// <param name="worldManufacturerId">The World Manufacturer Identifier (first 3 characters).</param>
    /// <param name="modelYear">The model year character (10th position).</param>
    /// <param name="plantCode">The plant code (11th position).</param>
    /// <returns><c>true</c> if the VIN is valid; otherwise, <c>false</c>.</returns>
    public static bool TryParseVin(string? vin, out string worldManufacturerId, out char modelYear, out char plantCode)
    {
        worldManufacturerId = string.Empty;
        modelYear = '\0';
        plantCode = '\0';

        if (!IsValidVin(vin))
            return false;

        var cleanedVin = Normalize(vin!);

        worldManufacturerId = cleanedVin.Substring(0, 3);
        modelYear = cleanedVin[9];
        plantCode = cleanedVin[10];

        return true;
    }

    /// <summary>
    /// Gets the World Manufacturer Identifier (WMI) from a VIN.
    /// </summary>
    /// <param name="vin">The VIN to extract from.</param>
    /// <returns>The WMI (first 3 characters), or empty string if invalid.</returns>
    public static string GetWorldManufacturerId(string? vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return string.Empty;

        var cleanedVin = Normalize(vin);

        if (cleanedVin.Length < 3)
            return string.Empty;

        return cleanedVin.Substring(0, 3);
    }

    /// <summary>
    /// Gets the model year from a VIN (10th character).
    /// </summary>
    /// <param name="vin">The VIN to extract from.</param>
    /// <returns>The model year character, or null if invalid.</returns>
    public static char? GetModelYearCode(string? vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return null;

        var cleanedVin = Normalize(vin);

        if (cleanedVin.Length < 10)
            return null;

        return cleanedVin[9];
    }

    /// <summary>
    /// Converts a VIN model year code to an approximate year.
    /// Note: VIN years cycle every 30 years, so this is an approximation.
    /// </summary>
    /// <param name="yearCode">The year code from position 10 of the VIN.</param>
    /// <param name="currentYear">The current year for reference (default: current year).</param>
    /// <returns>The approximate model year, or null if invalid.</returns>
    public static int? GetModelYear(char yearCode, int? currentYear = null)
    {
        var refYear = currentYear ?? DateTime.UtcNow.Year;
        
        // VIN year codes: A=1980/2010, B=1981/2011, ..., Y=2000/2030
        // Numbers: 1=2001/2031, 2=2002/2032, ..., 9=2009/2039
        
        var baseYear = yearCode switch
        {
            >= 'A' and <= 'H' => 1980 + (yearCode - 'A'),
            'J' => 1988,
            >= 'K' and <= 'N' => 1989 + (yearCode - 'K'),
            'P' => 1993,
            >= 'R' and <= 'T' => 1994 + (yearCode - 'R'),
            >= 'V' and <= 'Y' => 1997 + (yearCode - 'V'),
            >= '1' and <= '9' => 2001 + (yearCode - '1'),
            _ => -1
        };

        if (baseYear == -1)
            return null;

        // Adjust for 30-year cycle
        while (baseYear + 30 <= refYear)
            baseYear += 30;

        if (baseYear > refYear + 1) // Allow one year ahead for next model year
            baseYear -= 30;

        return baseYear;
    }

    /// <summary>
    /// Normalizes a VIN by removing spaces and converting to uppercase.
    /// </summary>
    /// <param name="vin">The VIN to normalize.</param>
    /// <returns>The normalized VIN.</returns>
    public static string Normalize(string? vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return string.Empty;

        return Regex.Replace(vin, @"\s+", "").ToUpperInvariant();
    }

    private static bool ValidateCheckDigit(string vin)
    {
        var sum = 0;

        for (int i = 0; i < 17; i++)
        {
            var value = GetCharValue(vin[i]);
            sum += value * Weights[i];
        }

        var checkDigit = sum % 11;
        var expectedChar = checkDigit == 10 ? 'X' : (char)('0' + checkDigit);

        return vin[8] == expectedChar;
    }

    private static int GetCharValue(char c)
    {
        if (char.IsDigit(c))
            return c - '0';

        // Letter values for VIN check digit calculation
        return c switch
        {
            'A' or 'J' => 1,
            'B' or 'K' or 'S' => 2,
            'C' or 'L' or 'T' => 3,
            'D' or 'M' or 'U' => 4,
            'E' or 'N' or 'V' => 5,
            'F' or 'W' => 6,
            'G' or 'P' or 'X' => 7,
            'H' or 'Y' => 8,
            'R' or 'Z' => 9,
            _ => 0
        };
    }
}
