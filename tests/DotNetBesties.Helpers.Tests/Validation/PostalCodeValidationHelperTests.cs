using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class PostalCodeValidationHelperTests
{
    #region US ZIP Code Tests

    [Test]
    public async Task IsValidUsZipCode_With5Digits_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidUsZipCode("12345");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsZipCode_WithPlus4_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidUsZipCode("12345-6789");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsZipCode_WithLetters_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidUsZipCode("1234A");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidUsZipCode_WithTooFewDigits_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidUsZipCode("1234");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidUsZipCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidUsZipCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Canadian Postal Code Tests

    [Test]
    public async Task IsValidCanadianPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidCanadianPostalCode("K1A 0B1");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidCanadianPostalCode_WithoutSpace_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidCanadianPostalCode("K1A0B1");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidCanadianPostalCode_WithLowercase_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidCanadianPostalCode("k1a 0b1");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidCanadianPostalCode_WithInvalidFormat_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidCanadianPostalCode("123 456");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidCanadianPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidCanadianPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region UK Postal Code Tests

    [Test]
    public async Task IsValidUkPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidUkPostalCode("SW1A 1AA");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPostalCode_WithoutSpace_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidUkPostalCode("SW1A1AA");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPostalCode_WithLowercase_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidUkPostalCode("sw1a 1aa");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPostalCode_WithVariousFormats_ReturnsTrue()
    {
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("M1 1AA")).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("M60 1NW")).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("CR2 6XH")).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("DN55 1PT")).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("W1A 1HQ")).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode("EC1A 1BB")).IsTrue();
    }

    [Test]
    public async Task IsValidUkPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidUkPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region German Postal Code Tests

    [Test]
    public async Task IsValidGermanPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidGermanPostalCode("10115");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidGermanPostalCode_WithInvalidLength_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidGermanPostalCode("1011");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidGermanPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidGermanPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region French Postal Code Tests

    [Test]
    public async Task IsValidFrenchPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidFrenchPostalCode("75001");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidFrenchPostalCode_WithInvalidLength_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidFrenchPostalCode("7500");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidFrenchPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidFrenchPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Australian Postal Code Tests

    [Test]
    public async Task IsValidAustralianPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidAustralianPostalCode("2000");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidAustralianPostalCode_WithInvalidLength_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidAustralianPostalCode("200");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidAustralianPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidAustralianPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Indian Postal Code Tests

    [Test]
    public async Task IsValidIndianPostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidIndianPostalCode("110001");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIndianPostalCode_WithInvalidLength_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidIndianPostalCode("11000");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIndianPostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidIndianPostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Japanese Postal Code Tests

    [Test]
    public async Task IsValidJapanesePostalCode_WithValidFormat_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidJapanesePostalCode("123-4567");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidJapanesePostalCode_WithoutHyphen_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidJapanesePostalCode("1234567");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidJapanesePostalCode_WithNull_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidJapanesePostalCode(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Format Tests

    [Test]
    public async Task FormatUsZipCode_With5Digits_Formats()
    {
        var result = PostalCodeValidationHelper.FormatUsZipCode("12345");
        await Assert.That(result).IsEqualTo("12345");
    }

    [Test]
    public async Task FormatUsZipCode_With9Digits_FormatsWithHyphen()
    {
        var result = PostalCodeValidationHelper.FormatUsZipCode("123456789");
        await Assert.That(result).IsEqualTo("12345-6789");
    }

    [Test]
    public async Task FormatUsZipCode_WithNull_ReturnsEmpty()
    {
        var result = PostalCodeValidationHelper.FormatUsZipCode(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task FormatCanadianPostalCode_WithValidCode_FormatsWithSpace()
    {
        var result = PostalCodeValidationHelper.FormatCanadianPostalCode("K1A0B1");
        await Assert.That(result).IsEqualTo("K1A 0B1");
    }

    [Test]
    public async Task FormatCanadianPostalCode_WithLowercase_ConvertsToUppercase()
    {
        var result = PostalCodeValidationHelper.FormatCanadianPostalCode("k1a0b1");
        await Assert.That(result).IsEqualTo("K1A 0B1");
    }

    [Test]
    public async Task FormatCanadianPostalCode_WithNull_ReturnsEmpty()
    {
        var result = PostalCodeValidationHelper.FormatCanadianPostalCode(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Normalize Tests

    [Test]
    public async Task Normalize_RemovesSpaces()
    {
        var result = PostalCodeValidationHelper.Normalize("K1A 0B1");
        await Assert.That(result).IsEqualTo("K1A0B1");
    }

    [Test]
    public async Task Normalize_ConvertsToUppercase()
    {
        var result = PostalCodeValidationHelper.Normalize("k1a 0b1");
        await Assert.That(result).IsEqualTo("K1A0B1");
    }

    [Test]
    public async Task Normalize_WithNull_ReturnsEmpty()
    {
        var result = PostalCodeValidationHelper.Normalize(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Normalize_WithMultipleSpaces_RemovesAll()
    {
        var result = PostalCodeValidationHelper.Normalize("K1A   0B1");
        await Assert.That(result).IsEqualTo("K1A0B1");
    }

    #endregion

    #region IsValidForCountry Tests

    [Test]
    public async Task IsValidForCountry_WithValidUsZipCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("12345", "US");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidCanadianCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("K1A 0B1", "CA");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidUkCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("SW1A 1AA", "GB");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidGermanCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("10115", "DE");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidFrenchCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("75001", "FR");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidAustralianCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("2000", "AU");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidIndianCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("110001", "IN");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithValidJapaneseCode_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("123-4567", "JP");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_WithInvalidCountryCode_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("12345", "XX");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidForCountry_WithNullCountryCode_ReturnsFalse()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("12345", null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidForCountry_WithUkAlias_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("SW1A 1AA", "UK");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidForCountry_CaseInsensitive_ReturnsTrue()
    {
        var result = PostalCodeValidationHelper.IsValidForCountry("12345", "us");
        await Assert.That(result).IsTrue();
    }

    #endregion
}
