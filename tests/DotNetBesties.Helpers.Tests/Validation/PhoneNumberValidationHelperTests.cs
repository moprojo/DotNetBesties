using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class PhoneNumberValidationHelperTests
{
    #region IsValidUsPhoneNumber Tests

    [Test]
    public async Task IsValidUsPhoneNumber_WithStandardFormat_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("555-555-5555");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithParentheses_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("(555) 555-5555");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithNoFormatting_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("5555555555");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithCountryCode_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("+1-555-555-5555");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithInvalidFormat_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("123-456-7890");
        await Assert.That(result).IsFalse(); // First digit must be 2-9
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithNull_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithEmpty_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidUsPhoneNumber_WithTooFewDigits_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUsPhoneNumber("555-5555");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region IsValidE164Format Tests

    [Test]
    public async Task IsValidE164Format_WithValidFormat_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format("+14155552671");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidE164Format_WithoutPlus_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format("14155552671");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidE164Format_WithSpaces_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format("+1 415 555 2671");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidE164Format_WithTooManyDigits_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format("+12345678901234567"); // 16 digits
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidE164Format_WithNull_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidE164Format_WithInternationalNumber_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidE164Format("+442071234567");
        await Assert.That(result).IsTrue();
    }

    #endregion

    #region IsValidUkPhoneNumber Tests

    [Test]
    public async Task IsValidUkPhoneNumber_WithValidLandline_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber("02071234567");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPhoneNumber_WithValidMobile_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber("07912345678");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPhoneNumber_WithCountryCode_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber("+442071234567");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPhoneNumber_WithSpaces_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber("020 7123 4567");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidUkPhoneNumber_WithNull_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidUkPhoneNumber_WithInvalidStart_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.IsValidUkPhoneNumber("00123456789");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region ExtractDigits Tests

    [Test]
    public async Task ExtractDigits_WithFormattedNumber_ReturnsOnlyDigits()
    {
        var result = PhoneNumberValidationHelper.ExtractDigits("(555) 555-5555");
        await Assert.That(result).IsEqualTo("5555555555");
    }

    [Test]
    public async Task ExtractDigits_WithCountryCode_ReturnsAllDigits()
    {
        var result = PhoneNumberValidationHelper.ExtractDigits("+1-555-555-5555");
        await Assert.That(result).IsEqualTo("15555555555");
    }

    [Test]
    public async Task ExtractDigits_WithNull_ReturnsEmpty()
    {
        var result = PhoneNumberValidationHelper.ExtractDigits(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ExtractDigits_WithLetters_RemovesLetters()
    {
        var result = PhoneNumberValidationHelper.ExtractDigits("1-800-FLOWERS");
        await Assert.That(result).IsEqualTo("1800");
    }

    #endregion

    #region FormatUsPhoneNumber Tests

    [Test]
    public async Task FormatUsPhoneNumber_With10Digits_FormatsCorrectly()
    {
        var result = PhoneNumberValidationHelper.FormatUsPhoneNumber("5555555555");
        await Assert.That(result).IsEqualTo("(555) 555-5555");
    }

    [Test]
    public async Task FormatUsPhoneNumber_With11Digits_FormatsWithCountryCode()
    {
        var result = PhoneNumberValidationHelper.FormatUsPhoneNumber("15555555555");
        await Assert.That(result).IsEqualTo("+1 (555) 555-5555");
    }

    [Test]
    public async Task FormatUsPhoneNumber_WithAlreadyFormatted_ReformatsCorrectly()
    {
        var result = PhoneNumberValidationHelper.FormatUsPhoneNumber("555-555-5555");
        await Assert.That(result).IsEqualTo("(555) 555-5555");
    }

    [Test]
    public async Task FormatUsPhoneNumber_WithNull_ReturnsEmpty()
    {
        var result = PhoneNumberValidationHelper.FormatUsPhoneNumber(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task FormatUsPhoneNumber_WithInvalidLength_ReturnsOriginal()
    {
        var result = PhoneNumberValidationHelper.FormatUsPhoneNumber("12345");
        await Assert.That(result).IsEqualTo("12345");
    }

    #endregion

    #region FormatToE164 Tests

    [Test]
    public async Task FormatToE164_WithUsNumber_FormatsCorrectly()
    {
        var result = PhoneNumberValidationHelper.FormatToE164("555-555-5555", "1");
        await Assert.That(result).IsEqualTo("+15555555555");
    }

    [Test]
    public async Task FormatToE164_WithExistingCountryCode_DoesNotDuplicate()
    {
        var result = PhoneNumberValidationHelper.FormatToE164("15555555555", "1");
        await Assert.That(result).IsEqualTo("+15555555555");
    }

    [Test]
    public async Task FormatToE164_WithUkNumber_FormatsCorrectly()
    {
        var result = PhoneNumberValidationHelper.FormatToE164("7912345678", "44");
        await Assert.That(result).IsEqualTo("+447912345678");
    }

    [Test]
    public async Task FormatToE164_WithNull_ReturnsEmpty()
    {
        var result = PhoneNumberValidationHelper.FormatToE164(null, "1");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task FormatToE164_WithNullCountryCode_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => PhoneNumberValidationHelper.FormatToE164("5555555555", null!)));
    }

    #endregion

    #region Mask Tests

    [Test]
    public async Task Mask_WithValidNumber_MasksAllButLastFour()
    {
        var result = PhoneNumberValidationHelper.Mask("555-555-5555");
        await Assert.That(result).IsEqualTo("******5555");
    }

    [Test]
    public async Task Mask_WithCustomChar_UsesCustomChar()
    {
        var result = PhoneNumberValidationHelper.Mask("555-555-5555", 'X');
        await Assert.That(result).IsEqualTo("XXXXXX5555");
    }

    [Test]
    public async Task Mask_WithNull_ReturnsEmpty()
    {
        var result = PhoneNumberValidationHelper.Mask(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Mask_WithShortNumber_MasksAll()
    {
        var result = PhoneNumberValidationHelper.Mask("123");
        await Assert.That(result).IsEqualTo("***");
    }

    #endregion

    #region HasMinimumDigits Tests

    [Test]
    public async Task HasMinimumDigits_WithEnoughDigits_ReturnsTrue()
    {
        var result = PhoneNumberValidationHelper.HasMinimumDigits("555-555-5555");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task HasMinimumDigits_WithTooFewDigits_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.HasMinimumDigits("555-5555");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task HasMinimumDigits_WithCustomMinimum_ValidatesCorrectly()
    {
        var result = PhoneNumberValidationHelper.HasMinimumDigits("12345", 5);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task HasMinimumDigits_WithNull_ReturnsFalse()
    {
        var result = PhoneNumberValidationHelper.HasMinimumDigits(null);
        await Assert.That(result).IsFalse();
    }

    #endregion
}
