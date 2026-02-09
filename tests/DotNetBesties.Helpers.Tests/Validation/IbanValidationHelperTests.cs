using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class IbanValidationHelperTests
{
    #region IsValidIban Tests

    [Test]
    public async Task IsValidIban_WithValidGermanIban_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("DE89370400440532013000");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIban_WithValidGermanIbanWithSpaces_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("DE89 3704 0044 0532 0130 00");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIban_WithValidFrenchIban_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("FR1420041010050500013M02606");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIban_WithValidItalianIban_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("IT60X0542811101000000123456");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIban_WithValidSpanishIban_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("ES9121000418450200051332");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIban_WithInvalidCheckDigit_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban("DE89370400440532013001"); // Changed last digit
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithTooShort_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban("DE8937040044");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithTooLong_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban("DE89370400440532013000123456789012");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithInvalidFormat_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban("1234567890123456");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithNull_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithEmpty_ReturnsFalse()
    {
        var result = IbanValidationHelper.IsValidIban("");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIban_WithLowercase_ReturnsTrue()
    {
        var result = IbanValidationHelper.IsValidIban("de89370400440532013000");
        await Assert.That(result).IsTrue();
    }

    #endregion

    #region FormatIban Tests

    [Test]
    public async Task FormatIban_WithValidIban_FormatsWithSpaces()
    {
        var result = IbanValidationHelper.FormatIban("DE89370400440532013000");
        await Assert.That(result).IsEqualTo("DE89 3704 0044 0532 0130 00");
    }

    [Test]
    public async Task FormatIban_WithAlreadyFormattedIban_ReformatsCorrectly()
    {
        var result = IbanValidationHelper.FormatIban("DE89 3704 0044 0532 0130 00");
        await Assert.That(result).IsEqualTo("DE89 3704 0044 0532 0130 00");
    }

    [Test]
    public async Task FormatIban_WithLowercase_ConvertsToUppercase()
    {
        var result = IbanValidationHelper.FormatIban("de89370400440532013000");
        await Assert.That(result).IsEqualTo("DE89 3704 0044 0532 0130 00");
    }

    [Test]
    public async Task FormatIban_WithNull_ReturnsEmpty()
    {
        var result = IbanValidationHelper.FormatIban(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region GetCountryCode Tests

    [Test]
    public async Task GetCountryCode_WithValidIban_ReturnsCountryCode()
    {
        var result = IbanValidationHelper.GetCountryCode("DE89370400440532013000");
        await Assert.That(result).IsEqualTo("DE");
    }

    [Test]
    public async Task GetCountryCode_WithNull_ReturnsEmpty()
    {
        var result = IbanValidationHelper.GetCountryCode(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task GetCountryCode_WithTooShort_ReturnsEmpty()
    {
        var result = IbanValidationHelper.GetCountryCode("D");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Normalize Tests

    [Test]
    public async Task Normalize_RemovesSpaces()
    {
        var result = IbanValidationHelper.Normalize("DE89 3704 0044 0532 0130 00");
        await Assert.That(result).IsEqualTo("DE89370400440532013000");
    }

    [Test]
    public async Task Normalize_ConvertsToUppercase()
    {
        var result = IbanValidationHelper.Normalize("de89370400440532013000");
        await Assert.That(result).IsEqualTo("DE89370400440532013000");
    }

    [Test]
    public async Task Normalize_WithNull_ReturnsEmpty()
    {
        var result = IbanValidationHelper.Normalize(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion
}
