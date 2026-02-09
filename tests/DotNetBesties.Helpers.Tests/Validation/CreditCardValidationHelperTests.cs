using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class CreditCardValidationHelperTests
{
    #region IsValidLuhn Tests

    [Test]
    public async Task IsValidLuhn_WithValidVisaNumber_ReturnsTrue()
    {
        // Valid test Visa number
        var result = CreditCardValidationHelper.IsValidLuhn("4532015112830366");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidLuhn_WithValidMasterCardNumber_ReturnsTrue()
    {
        // Valid test MasterCard number
        var result = CreditCardValidationHelper.IsValidLuhn("5425233430109903");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidLuhn_WithInvalidNumber_ReturnsFalse()
    {
        var result = CreditCardValidationHelper.IsValidLuhn("4532015112830367");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidLuhn_WithSpaces_ReturnsTrue()
    {
        var result = CreditCardValidationHelper.IsValidLuhn("4532 0151 1283 0366");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidLuhn_WithDashes_ReturnsTrue()
    {
        var result = CreditCardValidationHelper.IsValidLuhn("4532-0151-1283-0366");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidLuhn_WithNull_ReturnsFalse()
    {
        var result = CreditCardValidationHelper.IsValidLuhn(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidLuhn_WithEmpty_ReturnsFalse()
    {
        var result = CreditCardValidationHelper.IsValidLuhn("");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidLuhn_WithTooFewDigits_ReturnsFalse()
    {
        var result = CreditCardValidationHelper.IsValidLuhn("123456");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region GetCardType Tests

    [Test]
    public async Task GetCardType_WithVisa_ReturnsVisa()
    {
        var result = CreditCardValidationHelper.GetCardType("4532015112830366");
        await Assert.That(result).IsEqualTo("Visa");
    }

    [Test]
    public async Task GetCardType_WithMasterCard_ReturnsMasterCard()
    {
        var result = CreditCardValidationHelper.GetCardType("5425233430109903");
        await Assert.That(result).IsEqualTo("MasterCard");
    }

    [Test]
    public async Task GetCardType_WithAmex_ReturnsAmericanExpress()
    {
        var result = CreditCardValidationHelper.GetCardType("374245455400126");
        await Assert.That(result).IsEqualTo("American Express");
    }

    [Test]
    public async Task GetCardType_WithDiscover_ReturnsDiscover()
    {
        var result = CreditCardValidationHelper.GetCardType("6011000990139424");
        await Assert.That(result).IsEqualTo("Discover");
    }

    [Test]
    public async Task GetCardType_WithUnknownPattern_ReturnsUnknown()
    {
        var result = CreditCardValidationHelper.GetCardType("1234567890123456");
        await Assert.That(result).IsEqualTo("Unknown");
    }

    [Test]
    public async Task GetCardType_WithNull_ReturnsUnknown()
    {
        var result = CreditCardValidationHelper.GetCardType(null);
        await Assert.That(result).IsEqualTo("Unknown");
    }

    [Test]
    public async Task GetCardType_WithSpaces_DetectsType()
    {
        var result = CreditCardValidationHelper.GetCardType("4532 0151 1283 0366");
        await Assert.That(result).IsEqualTo("Visa");
    }

    #endregion

    #region IsCardType Tests

    [Test]
    public async Task IsCardType_WithMatchingType_ReturnsTrue()
    {
        var result = CreditCardValidationHelper.IsCardType("4532015112830366", "Visa");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsCardType_WithNonMatchingType_ReturnsFalse()
    {
        var result = CreditCardValidationHelper.IsCardType("4532015112830366", "MasterCard");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsCardType_CaseInsensitive_ReturnsTrue()
    {
        var result = CreditCardValidationHelper.IsCardType("4532015112830366", "visa");
        await Assert.That(result).IsTrue();
    }

    #endregion

    #region Mask Tests

    [Test]
    public async Task Mask_WithValidNumber_MasksAllButLastFour()
    {
        var result = CreditCardValidationHelper.Mask("4532015112830366");
        await Assert.That(result).IsEqualTo("************0366");
    }

    [Test]
    public async Task Mask_WithCustomChar_UsesCustomChar()
    {
        var result = CreditCardValidationHelper.Mask("4532015112830366", 'X');
        await Assert.That(result).IsEqualTo("XXXXXXXXXXXX0366");
    }

    [Test]
    public async Task Mask_WithSpaces_RemovesSpaces()
    {
        var result = CreditCardValidationHelper.Mask("4532 0151 1283 0366");
        await Assert.That(result).IsEqualTo("************0366");
    }

    [Test]
    public async Task Mask_WithNull_ReturnsEmpty()
    {
        var result = CreditCardValidationHelper.Mask(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Mask_WithShortNumber_MasksAll()
    {
        var result = CreditCardValidationHelper.Mask("123");
        await Assert.That(result).IsEqualTo("***");
    }

    #endregion

    #region Format Tests

    [Test]
    public async Task Format_WithValidNumber_FormatsWithSpaces()
    {
        var result = CreditCardValidationHelper.Format("4532015112830366");
        await Assert.That(result).IsEqualTo("4532 0151 1283 0366");
    }

    [Test]
    public async Task Format_WithAlreadyFormatted_ReformatsCorrectly()
    {
        var result = CreditCardValidationHelper.Format("4532-0151-1283-0366");
        await Assert.That(result).IsEqualTo("4532 0151 1283 0366");
    }

    [Test]
    public async Task Format_WithNull_ReturnsEmpty()
    {
        var result = CreditCardValidationHelper.Format(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Format_WithEmpty_ReturnsEmpty()
    {
        var result = CreditCardValidationHelper.Format("");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Format_WithAmexNumber_Formats15Digits()
    {
        var result = CreditCardValidationHelper.Format("374245455400126");
        await Assert.That(result).IsEqualTo("3742 4545 5400 126");
    }

    #endregion
}
