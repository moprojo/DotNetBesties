using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class VinValidationHelperTests
{
    #region IsValidVin Tests

    [Test]
    public async Task IsValidVin_WithValidVin_ReturnsTrue()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN109186");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidVin_WithInvalidCheckDigit_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN109187"); // Changed last digit
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithTooShort_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN10918");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithTooLong_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN1091861");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithInvalidCharI_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN10918I");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithInvalidCharO_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN10918O");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithInvalidCharQ_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("1HGBH41JXMN10918Q");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithLowercase_ReturnsTrue()
    {
        var result = VinValidationHelper.IsValidVin("1hgbh41jxmn109186");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidVin_WithNull_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidVin_WithEmpty_ReturnsFalse()
    {
        var result = VinValidationHelper.IsValidVin("");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region TryParseVin Tests

    [Test]
    public async Task TryParseVin_WithValidVin_ParsesCorrectly()
    {
        var success = VinValidationHelper.TryParseVin("1HGBH41JXMN109186", out var wmi, out var year, out var plant);
        
        await Assert.That(success).IsTrue();
        await Assert.That(wmi).IsEqualTo("1HG");
        await Assert.That(year).IsEqualTo('M');
        await Assert.That(plant).IsEqualTo('N');
    }

    [Test]
    public async Task TryParseVin_WithInvalidVin_ReturnsFalse()
    {
        var success = VinValidationHelper.TryParseVin("INVALID", out var wmi, out var year, out var plant);
        
        await Assert.That(success).IsFalse();
        await Assert.That(wmi).IsEqualTo(string.Empty);
        await Assert.That(year).IsEqualTo('\0');
        await Assert.That(plant).IsEqualTo('\0');
    }

    [Test]
    public async Task TryParseVin_WithNull_ReturnsFalse()
    {
        var success = VinValidationHelper.TryParseVin(null, out var wmi, out var year, out var plant);
        await Assert.That(success).IsFalse();
    }

    #endregion

    #region GetWorldManufacturerId Tests

    [Test]
    public async Task GetWorldManufacturerId_WithValidVin_ReturnsWMI()
    {
        var result = VinValidationHelper.GetWorldManufacturerId("1HGBH41JXMN109186");
        await Assert.That(result).IsEqualTo("1HG");
    }

    [Test]
    public async Task GetWorldManufacturerId_WithNull_ReturnsEmpty()
    {
        var result = VinValidationHelper.GetWorldManufacturerId(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task GetWorldManufacturerId_WithTooShort_ReturnsEmpty()
    {
        var result = VinValidationHelper.GetWorldManufacturerId("1H");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region GetModelYearCode Tests

    [Test]
    public async Task GetModelYearCode_WithValidVin_ReturnsYearCode()
    {
        var result = VinValidationHelper.GetModelYearCode("1HGBH41JXMN109186");
        await Assert.That(result).IsEqualTo('M');
    }

    [Test]
    public async Task GetModelYearCode_WithNull_ReturnsNull()
    {
        var result = VinValidationHelper.GetModelYearCode(null);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task GetModelYearCode_WithTooShort_ReturnsNull()
    {
        var result = VinValidationHelper.GetModelYearCode("1HGBH41JX");
        await Assert.That(result).IsNull();
    }

    #endregion

    #region GetModelYear Tests

    [Test]
    public async Task GetModelYear_WithYearCodeA_ReturnsCorrectYear()
    {
        var result = VinValidationHelper.GetModelYear('A', 2023);
        await Assert.That(result).IsEqualTo(2010);
    }

    [Test]
    public async Task GetModelYear_WithYearCode1_ReturnsCorrectYear()
    {
        var result = VinValidationHelper.GetModelYear('1', 2023);
        await Assert.That(result).IsEqualTo(2001);
    }

    [Test]
    public async Task GetModelYear_WithYearCodeM_ReturnsCorrectYear()
    {
        var result = VinValidationHelper.GetModelYear('M', 2023);
        // M = 1991 or 2021
        await Assert.That(result).IsEqualTo(2021);
    }

    [Test]
    public async Task GetModelYear_WithInvalidCode_ReturnsNull()
    {
        var result = VinValidationHelper.GetModelYear('I'); // I is not used in VINs
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task GetModelYear_WithDefaultCurrentYear_Works()
    {
        var result = VinValidationHelper.GetModelYear('M');
        await Assert.That(result).IsNotNull();
    }

    #endregion

    #region Normalize Tests

    [Test]
    public async Task Normalize_RemovesSpaces()
    {
        var result = VinValidationHelper.Normalize("1HG BH41 JXMN 109186");
        await Assert.That(result).IsEqualTo("1HGBH41JXMN109186");
    }

    [Test]
    public async Task Normalize_ConvertsToUppercase()
    {
        var result = VinValidationHelper.Normalize("1hgbh41jxmn109186");
        await Assert.That(result).IsEqualTo("1HGBH41JXMN109186");
    }

    [Test]
    public async Task Normalize_WithNull_ReturnsEmpty()
    {
        var result = VinValidationHelper.Normalize(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion
}
