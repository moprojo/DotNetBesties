using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class IsbnValidationHelperTests
{
    #region IsValidIsbn Tests

    [Test]
    public async Task IsValidIsbn_WithValidIsbn10_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn("0306406152");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn_WithValidIsbn10WithX_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn("043942089X");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn_WithValidIsbn13_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn("9780306406157");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn_WithValidIsbn13WithHyphens_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn("978-0-306-40615-7");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn_WithInvalidLength_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn("12345");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIsbn_WithInvalidCheckDigit_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn("0306406153"); // Invalid check digit
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIsbn_WithNull_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn(null);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region IsValidIsbn10 Tests

    [Test]
    public async Task IsValidIsbn10_WithValidIsbn_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn10("0306406152");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn10_WithXCheckDigit_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn10("043942089X");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn10_WithHyphens_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn10("0-306-40615-2");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn10_WithInvalidCheckDigit_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn10("0306406153");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIsbn10_WithWrongLength_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn10("030640615");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region IsValidIsbn13 Tests

    [Test]
    public async Task IsValidIsbn13_WithValidIsbn_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn13("9780306406157");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn13_WithHyphens_ReturnsTrue()
    {
        var result = IsbnValidationHelper.IsValidIsbn13("978-0-306-40615-7");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsValidIsbn13_WithInvalidCheckDigit_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn13("9780306406158");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsValidIsbn13_WithWrongLength_ReturnsFalse()
    {
        var result = IsbnValidationHelper.IsValidIsbn13("978030640615");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region ConvertIsbn10ToIsbn13 Tests

    [Test]
    public async Task ConvertIsbn10ToIsbn13_WithValidIsbn10_ConvertsCorrectly()
    {
        var result = IsbnValidationHelper.ConvertIsbn10ToIsbn13("0306406152");
        await Assert.That(result).IsEqualTo("9780306406157");
    }

    [Test]
    public async Task ConvertIsbn10ToIsbn13_WithXCheckDigit_ConvertsCorrectly()
    {
        var result = IsbnValidationHelper.ConvertIsbn10ToIsbn13("043942089X");
        await Assert.That(result).IsEqualTo("9780439420891");
    }

    [Test]
    public async Task ConvertIsbn10ToIsbn13_WithInvalidIsbn_ReturnsEmpty()
    {
        var result = IsbnValidationHelper.ConvertIsbn10ToIsbn13("1234567890");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ConvertIsbn10ToIsbn13_WithNull_ReturnsEmpty()
    {
        var result = IsbnValidationHelper.ConvertIsbn10ToIsbn13(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region FormatIsbn Tests

    [Test]
    public async Task FormatIsbn_WithIsbn10_FormatsWithHyphens()
    {
        var result = IsbnValidationHelper.FormatIsbn("0306406152");
        await Assert.That(result).IsEqualTo("0-306-40615-2");
    }

    [Test]
    public async Task FormatIsbn_WithIsbn13_FormatsWithHyphens()
    {
        var result = IsbnValidationHelper.FormatIsbn("9780306406157");
        await Assert.That(result).IsEqualTo("978-0-306-40615-7");
    }

    [Test]
    public async Task FormatIsbn_WithNull_ReturnsEmpty()
    {
        var result = IsbnValidationHelper.FormatIsbn(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Normalize Tests

    [Test]
    public async Task Normalize_RemovesHyphens()
    {
        var result = IsbnValidationHelper.Normalize("978-0-306-40615-7");
        await Assert.That(result).IsEqualTo("9780306406157");
    }

    [Test]
    public async Task Normalize_RemovesSpaces()
    {
        var result = IsbnValidationHelper.Normalize("978 0 306 40615 7");
        await Assert.That(result).IsEqualTo("9780306406157");
    }

    [Test]
    public async Task Normalize_ConvertsToUppercase()
    {
        var result = IsbnValidationHelper.Normalize("043942089x");
        await Assert.That(result).IsEqualTo("043942089X");
    }

    [Test]
    public async Task Normalize_WithNull_ReturnsEmpty()
    {
        var result = IsbnValidationHelper.Normalize(null);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion
}
