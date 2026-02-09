using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

public class StringExtensionsTests
{
    #region Null/Empty Tests

    [Test]
    public async Task IsNullOrEmpty_WithNull_ReturnsTrue()
    {
        string? value = null;
        await Assert.That(value.IsNullOrEmpty()).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithEmpty_ReturnsTrue()
    {
        await Assert.That("".IsNullOrEmpty()).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithValue_ReturnsFalse()
    {
        await Assert.That("test".IsNullOrEmpty()).IsFalse();
    }

    [Test]
    public async Task IsNullOrWhiteSpace_WithWhitespace_ReturnsTrue()
    {
        await Assert.That("   ".IsNullOrWhiteSpace()).IsTrue();
    }

    [Test]
    public async Task HasValue_WithValue_ReturnsTrue()
    {
        await Assert.That("test".HasValue()).IsTrue();
    }

    [Test]
    public async Task DefaultIfEmpty_WithEmpty_ReturnsDefault()
    {
        var result = "".DefaultIfEmpty("default");
        await Assert.That(result).IsEqualTo("default");
    }

    #endregion

    #region Truncate Tests

    [Test]
    public async Task Truncate_WhenShorterThanMax_ReturnsOriginal()
    {
        var result = "test".Truncate(10);
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task Truncate_WhenLongerThanMax_TruncatesWithSuffix()
    {
        var result = "hello world".Truncate(8);
        await Assert.That(result).IsEqualTo("hello...");
    }

    [Test]
    public async Task Truncate_WithCustomSuffix_UsesCustomSuffix()
    {
        var result = "hello world".Truncate(8, "!");
        await Assert.That(result).IsEqualTo("hello w!");
    }

    [Test]
    public async Task Truncate_WithNegativeMaxLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => "test".Truncate(-1)));
    }

    [Test]
    public async Task Truncate_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Truncate(10);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Truncate_WithZeroLength_ReturnsSuffix()
    {
        var result = "hello".Truncate(0);
        await Assert.That(result).IsEqualTo("...");
    }

    [Test]
    public async Task Truncate_WhenMaxLengthLessThanSuffix_ReturnsEmptyPlusSuffix()
    {
        var result = "hello world".Truncate(2);
        await Assert.That(result).IsEqualTo("...");
    }

    #endregion

    #region Contains/Comparison Tests

    [Test]
    public async Task Contains_IgnoreCase_FindsValue()
    {
        await Assert.That("Hello World".Contains("WORLD", StringComparison.OrdinalIgnoreCase)).IsTrue();
    }

    [Test]
    public async Task Contains_WithNullSource_ReturnsFalse()
    {
        string? value = null;
        var result = DotNetBesties.Helpers.Extensions.StringExtensions.Contains(value, "test", StringComparison.Ordinal);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsIgnoreCase_WithDifferentCase_ReturnsTrue()
    {
        await Assert.That("Hello".EqualsIgnoreCase("HELLO")).IsTrue();
    }

    [Test]
    public async Task EqualsIgnoreCase_WithNull_ReturnsFalse()
    {
        string? value = null;
        await Assert.That(value.EqualsIgnoreCase("test")).IsFalse();
    }

    [Test]
    public async Task EqualsIgnoreCase_BothNull_ReturnsTrue()
    {
        string? value = null;
        await Assert.That(value.EqualsIgnoreCase(null)).IsTrue();
    }

    [Test]
    public async Task StartsWithIgnoreCase_WithDifferentCase_ReturnsTrue()
    {
        await Assert.That("Hello World".StartsWithIgnoreCase("HELLO")).IsTrue();
    }

    [Test]
    public async Task StartsWithIgnoreCase_WithNull_ReturnsFalse()
    {
        string? value = null;
        var result = value.StartsWithIgnoreCase("test");
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWithIgnoreCase_WithDifferentCase_ReturnsTrue()
    {
        await Assert.That("Hello World".EndsWithIgnoreCase("WORLD")).IsTrue();
    }

    [Test]
    public async Task EndsWithIgnoreCase_WithNull_ReturnsFalse()
    {
        string? value = null;
        var result = value.EndsWithIgnoreCase("test");
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Whitespace Tests

    [Test]
    public async Task RemoveWhitespace_RemovesAllWhitespace()
    {
        var result = "hello world test".RemoveWhitespace();
        await Assert.That(result).IsEqualTo("helloworldtest");
    }

    [Test]
    public async Task RemoveWhitespace_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.RemoveWhitespace();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task RemoveWhitespace_WithTabs_RemovesTabs()
    {
        var result = "hello\tworld".RemoveWhitespace();
        await Assert.That(result).IsEqualTo("helloworld");
    }

    [Test]
    public async Task CollapseWhitespace_CollapsesMultipleSpaces()
    {
        var result = "hello    world   test".CollapseWhitespace();
        await Assert.That(result).IsEqualTo("hello world test");
    }

    [Test]
    public async Task CollapseWhitespace_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.CollapseWhitespace();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task CollapseWhitespace_TrimsLeadingAndTrailing()
    {
        var result = "  hello  world  ".CollapseWhitespace();
        await Assert.That(result).IsEqualTo("hello world");
    }

    #endregion

    #region Case Conversion Tests

    [Test]
    public async Task ToTitleCase_ConvertsCorrectly()
    {
        var result = "hello world".ToTitleCase();
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task ToTitleCase_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ToTitleCase();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToPascalCase_ConvertsCorrectly()
    {
        var result = "hello world test".ToPascalCase();
        await Assert.That(result).IsEqualTo("HelloWorldTest");
    }

    [Test]
    public async Task ToPascalCase_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ToPascalCase();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToPascalCase_WithUnderscores_ConvertsCorrectly()
    {
        var result = "hello_world_test".ToPascalCase();
        await Assert.That(result).IsEqualTo("HelloWorldTest");
    }

    [Test]
    public async Task ToCamelCase_ConvertsCorrectly()
    {
        var result = "hello world test".ToCamelCase();
        await Assert.That(result).IsEqualTo("helloWorldTest");
    }

    [Test]
    public async Task ToCamelCase_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ToCamelCase();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToKebabCase_ConvertsCorrectly()
    {
        var result = "HelloWorldTest".ToKebabCase();
        await Assert.That(result).IsEqualTo("hello-world-test");
    }

    [Test]
    public async Task ToKebabCase_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ToKebabCase();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToSnakeCase_ConvertsCorrectly()
    {
        var result = "HelloWorldTest".ToSnakeCase();
        await Assert.That(result).IsEqualTo("hello_world_test");
    }

    [Test]
    public async Task ToSnakeCase_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ToSnakeCase();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Replace Tests

    [Test]
    public async Task ReplaceFirst_ReplacesOnlyFirst()
    {
        var result = "hello world hello".ReplaceFirst("hello", "hi");
        await Assert.That(result).IsEqualTo("hi world hello");
    }

    [Test]
    public async Task ReplaceFirst_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ReplaceFirst("hello", "hi");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ReplaceFirst_WhenNotFound_ReturnsOriginal()
    {
        var result = "hello world".ReplaceFirst("xyz", "abc");
        await Assert.That(result).IsEqualTo("hello world");
    }

    [Test]
    public async Task ReplaceLast_ReplacesOnlyLast()
    {
        var result = "hello world hello".ReplaceLast("hello", "hi");
        await Assert.That(result).IsEqualTo("hello world hi");
    }

    [Test]
    public async Task ReplaceLast_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.ReplaceLast("hello", "hi");
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ReplaceLast_WhenNotFound_ReturnsOriginal()
    {
        var result = "hello world".ReplaceLast("xyz", "abc");
        await Assert.That(result).IsEqualTo("hello world");
    }

    #endregion

    #region Substring Helpers Tests

    [Test]
    public async Task Left_ReturnsLeftCharacters()
    {
        var result = "hello world".Left(5);
        await Assert.That(result).IsEqualTo("hello");
    }

    [Test]
    public async Task Left_WithNegativeLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => "test".Left(-1)));
    }

    [Test]
    public async Task Left_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Left(5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Left_WithLengthGreaterThanString_ReturnsFullString()
    {
        var result = "test".Left(100);
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task Right_ReturnsRightCharacters()
    {
        var result = "hello world".Right(5);
        await Assert.That(result).IsEqualTo("world");
    }

    [Test]
    public async Task Right_WithNegativeLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => "test".Right(-1)));
    }

    [Test]
    public async Task Right_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Right(5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Right_WithLengthGreaterThanString_ReturnsFullString()
    {
        var result = "test".Right(100);
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task Mid_ReturnsMiddleCharacters()
    {
        var result = "hello world".Mid(6, 5);
        await Assert.That(result).IsEqualTo("world");
    }

    [Test]
    public async Task Mid_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Mid(0, 5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Mid_WithIndexOutOfRange_ReturnsEmptyString()
    {
        var result = "test".Mid(10, 5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Mid_WithLengthExceedingString_ReturnsRemainder()
    {
        var result = "hello".Mid(2, 100);
        await Assert.That(result).IsEqualTo("llo");
    }

    #endregion

    #region Other Tests

    [Test]
    public async Task Reverse_ReversesString()
    {
        var result = "hello".Reverse();
        await Assert.That(result).IsEqualTo("olleh");
    }

    [Test]
    public async Task Reverse_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Reverse();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Reverse_WithEmptyString_ReturnsEmptyString()
    {
        var result = "".Reverse();
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Reverse_WithSingleChar_ReturnsSameChar()
    {
        var result = "a".Reverse();
        await Assert.That(result).IsEqualTo("a");
    }

    [Test]
    public async Task CountOccurrences_CountsCorrectly()
    {
        var count = "hello world hello".CountOccurrences("hello");
        await Assert.That(count).IsEqualTo(2);
    }

    [Test]
    public async Task CountOccurrences_WithNullSource_ReturnsZero()
    {
        string? value = null;
        var count = value.CountOccurrences("test");
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task CountOccurrences_WithNullValue_ReturnsZero()
    {
        var count = "test".CountOccurrences(null!);
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task CountOccurrences_WithEmptyString_ReturnsZero()
    {
        var count = "test".CountOccurrences("");
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task CountOccurrences_CaseSensitive_CountsCorrectly()
    {
        var count = "Hello hello HELLO".CountOccurrences("hello");
        await Assert.That(count).IsEqualTo(1);
    }

    [Test]
    public async Task CountOccurrences_CaseInsensitive_CountsCorrectly()
    {
        var count = "Hello hello HELLO".CountOccurrences("hello", StringComparison.OrdinalIgnoreCase);
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Repeat_RepeatsString()
    {
        var result = "ab".Repeat(3);
        await Assert.That(result).IsEqualTo("ababab");
    }

    [Test]
    public async Task Repeat_WithNegativeCount_ReturnsEmptyString()
    {
        var result = "test".Repeat(-5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Repeat_WithZeroCount_ReturnsEmptyString()
    {
        var result = "test".Repeat(0);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Repeat_WithNull_ReturnsEmptyString()
    {
        string? value = null;
        var result = value.Repeat(5);
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Repeat_WithOneCount_ReturnsOriginal()
    {
        var result = "test".Repeat(1);
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task EnsureEndsWith_AddsIfMissing()
    {
        var result = "test".EnsureEndsWith(".txt");
        await Assert.That(result).IsEqualTo("test.txt");
    }

    [Test]
    public async Task EnsureEndsWith_DoesNotAddIfPresent()
    {
        var result = "test.txt".EnsureEndsWith(".txt");
        await Assert.That(result).IsEqualTo("test.txt");
    }

    [Test]
    public async Task EnsureEndsWith_WithNull_ReturnsSuffix()
    {
        string? value = null;
        var result = value.EnsureEndsWith("suffix");
        await Assert.That(result).IsEqualTo("suffix");
    }

    [Test]
    public async Task EnsureStartsWith_AddsIfMissing()
    {
        var result = "world".EnsureStartsWith("hello ");
        await Assert.That(result).IsEqualTo("hello world");
    }

    [Test]
    public async Task EnsureStartsWith_DoesNotAddIfPresent()
    {
        var result = "hello world".EnsureStartsWith("hello");
        await Assert.That(result).IsEqualTo("hello world");
    }

    [Test]
    public async Task EnsureStartsWith_WithNull_ReturnsPrefix()
    {
        string? value = null;
        var result = value.EnsureStartsWith("prefix");
        await Assert.That(result).IsEqualTo("prefix");
    }

    #endregion

    #region Validation Tests

    [Test]
    public async Task IsNumeric_WithDigitsOnly_ReturnsTrue()
    {
        await Assert.That("12345".IsNumeric()).IsTrue();
    }

    [Test]
    public async Task IsNumeric_WithLetters_ReturnsFalse()
    {
        await Assert.That("123abc".IsNumeric()).IsFalse();
    }

    [Test]
    public async Task IsNumeric_WithSpecialChars_ReturnsFalse()
    {
        await Assert.That("123.45".IsNumeric()).IsFalse();
    }

    [Test]
    public async Task IsNumeric_WithNull_ReturnsFalse()
    {
        string? value = null;
        await Assert.That(value.IsNumeric()).IsFalse();
    }

    [Test]
    public async Task IsNumeric_WithEmptyString_ReturnsFalse()
    {
        await Assert.That("".IsNumeric()).IsFalse();
    }

    [Test]
    public async Task IsNumeric_WithWhitespace_ReturnsFalse()
    {
        await Assert.That("123 456".IsNumeric()).IsFalse();
    }

    [Test]
    public async Task IsNumeric_WithZero_ReturnsTrue()
    {
        await Assert.That("0".IsNumeric()).IsTrue();
    }

    [Test]
    public async Task IsAlphanumeric_WithLettersAndDigits_ReturnsTrue()
    {
        await Assert.That("abc123".IsAlphanumeric()).IsTrue();
    }

    [Test]
    public async Task IsAlphanumeric_WithLettersOnly_ReturnsTrue()
    {
        await Assert.That("abcdef".IsAlphanumeric()).IsTrue();
    }

    [Test]
    public async Task IsAlphanumeric_WithDigitsOnly_ReturnsTrue()
    {
        await Assert.That("123456".IsAlphanumeric()).IsTrue();
    }

    [Test]
    public async Task IsAlphanumeric_WithSpecialChars_ReturnsFalse()
    {
        await Assert.That("abc-123".IsAlphanumeric()).IsFalse();
    }

    [Test]
    public async Task IsAlphanumeric_WithNull_ReturnsFalse()
    {
        string? value = null;
        await Assert.That(value.IsAlphanumeric()).IsFalse();
    }

    [Test]
    public async Task IsAlphanumeric_WithEmptyString_ReturnsFalse()
    {
        await Assert.That("".IsAlphanumeric()).IsFalse();
    }

    [Test]
    public async Task IsAlphanumeric_WithWhitespace_ReturnsFalse()
    {
        await Assert.That("abc 123".IsAlphanumeric()).IsFalse();
    }

    #endregion

    #region Encoding Tests

    [Test]
    public async Task ToBase64_EncodesCorrectly()
    {
        var result = "Hello World".ToBase64();
        await Assert.That(result).IsEqualTo("SGVsbG8gV29ybGQ=");
    }

    [Test]
    public async Task ToBase64_WithEmptyString_ReturnsEmptyBase64()
    {
        var result = "".ToBase64();
        await Assert.That(result).IsEqualTo("");
    }

    [Test]
    public async Task ToBase64_WithSpecialChars_EncodesCorrectly()
    {
        var original = "Hello! @#$%^&*()";
        var encoded = original.ToBase64();
        var decoded = encoded.FromBase64();
        await Assert.That(decoded).IsEqualTo(original);
    }

    [Test]
    public async Task ToBase64_WithNull_ThrowsArgumentNullException()
    {
        string? value = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => value!.ToBase64()));
    }

    [Test]
    public async Task ToBase64_WithUnicode_EncodesCorrectly()
    {
        var original = "Hello ?? ??";
        var encoded = original.ToBase64();
        var decoded = encoded.FromBase64();
        await Assert.That(decoded).IsEqualTo(original);
    }

    [Test]
    public async Task FromBase64_DecodesCorrectly()
    {
        var result = "SGVsbG8gV29ybGQ=".FromBase64();
        await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task FromBase64_WithEmptyString_ReturnsEmpty()
    {
        var result = "".FromBase64();
        await Assert.That(result).IsEqualTo("");
    }

    [Test]
    public async Task FromBase64_WithNull_ThrowsArgumentNullException()
    {
        string? value = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => value!.FromBase64()));
    }

    [Test]
    public async Task FromBase64_WithInvalidBase64_ThrowsFormatException()
    {
        await Assert.ThrowsAsync<FormatException>(
            async () => await Task.Run(() => "Not-Valid-Base64!!!".FromBase64()));
    }

    [Test]
    public async Task Base64_RoundTrip_PreservesOriginal()
    {
        var original = "The quick brown fox jumps over the lazy dog";
        var encoded = original.ToBase64();
        var decoded = encoded.FromBase64();
        await Assert.That(decoded).IsEqualTo(original);
    }

    [Test]
    public async Task Base64_WithLongString_EncodesAndDecodesCorrectly()
    {
        var original = new string('A', 1000);
        var encoded = original.ToBase64();
        var decoded = encoded.FromBase64();
        await Assert.That(decoded).IsEqualTo(original);
    }

    #endregion
}
