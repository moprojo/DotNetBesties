using System;
using System.Globalization;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class CharHelperTests
{
    #region Case Conversion Tests

    [Test]
    public async Task ToLower_WithCustomCulture_ShouldConvertCorrectly()
    {
        var culture = new CultureInfo("tr-TR");
        var lower = CharHelper.ToLower('I', culture);
        await Assert.That(lower).IsEqualTo('ı');
    }

    [Test]
    public async Task ToUpper_WithCustomCulture_ShouldConvertCorrectly()
    {
        var culture = new CultureInfo("tr-TR");
        var upper = CharHelper.ToUpper('i', culture);
        await Assert.That(upper).IsEqualTo('İ');
    }

    [Test]
    public async Task ToLower_ToUpper_ShouldUseInvariantCulture()
    {
        var lower = CharHelper.ToLower('A');
        var upper = CharHelper.ToUpper('ß');
        await Assert.That(lower).IsEqualTo('a');
        await Assert.That(upper).IsEqualTo(char.ToUpperInvariant('ß'));
    }

    #endregion

    #region Character Classification Tests

    [Test]
    public async Task IsVowel_WithVowels_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsVowel('a')).IsTrue();
        await Assert.That(CharHelper.IsVowel('e')).IsTrue();
        await Assert.That(CharHelper.IsVowel('i')).IsTrue();
        await Assert.That(CharHelper.IsVowel('o')).IsTrue();
        await Assert.That(CharHelper.IsVowel('u')).IsTrue();
    }

    [Test]
    public async Task IsVowel_WithUppercaseVowels_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsVowel('A')).IsTrue();
        await Assert.That(CharHelper.IsVowel('E')).IsTrue();
        await Assert.That(CharHelper.IsVowel('I')).IsTrue();
        await Assert.That(CharHelper.IsVowel('O')).IsTrue();
        await Assert.That(CharHelper.IsVowel('U')).IsTrue();
    }

    [Test]
    public async Task IsVowel_WithConsonants_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsVowel('b')).IsFalse();
        await Assert.That(CharHelper.IsVowel('c')).IsFalse();
        await Assert.That(CharHelper.IsVowel('d')).IsFalse();
    }

    [Test]
    public async Task IsVowel_WithNonLetters_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsVowel('1')).IsFalse();
        await Assert.That(CharHelper.IsVowel(' ')).IsFalse();
        await Assert.That(CharHelper.IsVowel('!')).IsFalse();
    }

    [Test]
    public async Task IsConsonant_WithConsonants_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsConsonant('b')).IsTrue();
        await Assert.That(CharHelper.IsConsonant('c')).IsTrue();
        await Assert.That(CharHelper.IsConsonant('d')).IsTrue();
        await Assert.That(CharHelper.IsConsonant('z')).IsTrue();
    }

    [Test]
    public async Task IsConsonant_WithVowels_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsConsonant('a')).IsFalse();
        await Assert.That(CharHelper.IsConsonant('e')).IsFalse();
    }

    [Test]
    public async Task IsConsonant_WithNonLetters_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsConsonant('1')).IsFalse();
        await Assert.That(CharHelper.IsConsonant(' ')).IsFalse();
        await Assert.That(CharHelper.IsConsonant('!')).IsFalse();
    }

    #endregion

    #region Repetition Tests

    [Test]
    public async Task Repeat_WithPositiveCount_RepeatsCharacter()
    {
        var result = CharHelper.Repeat('a', 5);
        await Assert.That(result).IsEqualTo("aaaaa");
    }

    [Test]
    public async Task Repeat_WithZeroCount_ReturnsEmptyString()
    {
        var result = CharHelper.Repeat('a', 0);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Repeat_WithNegativeCount_ThrowsArgumentOutOfRangeException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => CharHelper.Repeat('a', -1)));
    }

    [Test]
    public async Task Repeat_WithSpecialCharacters_Works()
    {
        var result = CharHelper.Repeat('*', 3);
        await Assert.That(result).IsEqualTo("***");
    }

    #endregion

    #region Range Tests

    [Test]
    public async Task IsInRange_WithinRange_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsInRange('b', 'a', 'c')).IsTrue();
        await Assert.That(CharHelper.IsInRange('5', '0', '9')).IsTrue();
    }

    [Test]
    public async Task IsInRange_AtBoundaries_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsInRange('a', 'a', 'z')).IsTrue();
        await Assert.That(CharHelper.IsInRange('z', 'a', 'z')).IsTrue();
    }

    [Test]
    public async Task IsInRange_OutsideRange_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsInRange('0', 'a', 'z')).IsFalse();
        await Assert.That(CharHelper.IsInRange('A', 'a', 'z')).IsFalse();
    }

    #endregion

    #region IsAnyOf Tests

    [Test]
    public async Task IsAnyOf_WithMatchingChar_ReturnsTrue()
    {
        await Assert.That(CharHelper.IsAnyOf('a', 'a', 'b', 'c')).IsTrue();
        await Assert.That(CharHelper.IsAnyOf('b', 'a', 'b', 'c')).IsTrue();
        await Assert.That(CharHelper.IsAnyOf('c', 'a', 'b', 'c')).IsTrue();
    }

    [Test]
    public async Task IsAnyOf_WithNonMatchingChar_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsAnyOf('d', 'a', 'b', 'c')).IsFalse();
    }

    [Test]
    public async Task IsAnyOf_WithEmptyArray_ReturnsFalse()
    {
        await Assert.That(CharHelper.IsAnyOf('a')).IsFalse();
    }

    [Test]
    public async Task IsAnyOf_WithNullArray_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CharHelper.IsAnyOf('a', null!)));
    }

    #endregion

    #region String Extraction Tests

    [Test]
    public async Task GetFirstOrDefault_ShouldReturnFirstChar()
    {
        var result = CharHelper.GetFirstOrDefault("abc");
        await Assert.That(result).IsEqualTo('a');
    }

    [Test]
    public async Task GetFirstOrDefault_Null_ReturnsDefault()
    {
        var result = CharHelper.GetFirstOrDefault(null, '?');
        await Assert.That(result).IsEqualTo('?');
    }

    [Test]
    public async Task GetFirstOrDefault_EmptyString_ReturnsDefault()
    {
        var result = CharHelper.GetFirstOrDefault(string.Empty, '?');
        await Assert.That(result).IsEqualTo('?');
    }

    [Test]
    public async Task GetLastOrDefault_ShouldReturnLastChar()
    {
        var result = CharHelper.GetLastOrDefault("xyz");
        await Assert.That(result).IsEqualTo('z');
    }

    [Test]
    public async Task GetLastOrDefault_Null_ReturnsDefault()
    {
        var result = CharHelper.GetLastOrDefault(null, '?');
        await Assert.That(result).IsEqualTo('?');
    }

    [Test]
    public async Task GetLastOrDefault_EmptyString_ReturnsDefault()
    {
        var result = CharHelper.GetLastOrDefault(string.Empty, '?');
        await Assert.That(result).IsEqualTo('?');
    }

    [Test]
    public async Task GetAtOrDefault_ValidIndex_ReturnsChar()
    {
        var result = CharHelper.GetAtOrDefault("abc", 1);
        await Assert.That(result).IsEqualTo('b');
    }

    [Test]
    public async Task GetAtOrDefault_OutOfRange_ReturnsDefault()
    {
        var result = CharHelper.GetAtOrDefault("abc", 5, '!');
        await Assert.That(result).IsEqualTo('!');
    }

    [Test]
    public async Task GetAtOrDefault_NegativeIndex_ReturnsDefault()
    {
        var result = CharHelper.GetAtOrDefault("abc", -1, '!');
        await Assert.That(result).IsEqualTo('!');
    }

    [Test]
    public async Task GetAtOrDefault_NullString_ReturnsDefault()
    {
        var result = CharHelper.GetAtOrDefault(null, 0, '!');
        await Assert.That(result).IsEqualTo('!');
    }

    #endregion
}
