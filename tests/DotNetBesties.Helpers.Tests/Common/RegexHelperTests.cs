using System;
using System.Linq;
using System.Text.RegularExpressions;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Common;

namespace DotNetBesties.Helpers.Tests.Common;

public class RegexHelperTests
{
    #region EmailPattern Tests

    [Test]
    public async Task EmailPattern_WithValidEmails_Matches()
    {
        var validEmails = new[]
        {
            "test@example.com",
            "user.name@example.com",
            "user+tag@example.co.uk",
            "test123@test-domain.com",
            "UPPERCASE@EXAMPLE.COM"
        };

        foreach (var email in validEmails)
        {
            await Assert.That(RegexHelper.EmailPattern.IsMatch(email)).IsTrue();
        }
    }

    [Test]
    public async Task EmailPattern_WithInvalidEmails_DoesNotMatch()
    {
        var invalidEmails = new[]
        {
            "invalid",
            "@example.com",
            "user@",
            "user name@example.com",
            "user@.com",
            "user@domain"
        };

        foreach (var email in invalidEmails)
        {
            await Assert.That(RegexHelper.EmailPattern.IsMatch(email)).IsFalse();
        }
    }

    [Test]
    public async Task EmailPattern_IsCaseInsensitive()
    {
        await Assert.That(RegexHelper.EmailPattern.IsMatch("TEST@EXAMPLE.COM")).IsTrue();
        await Assert.That(RegexHelper.EmailPattern.IsMatch("test@example.com")).IsTrue();
    }

    #endregion

    #region UrlPattern Tests

    [Test]
    public async Task UrlPattern_WithValidUrls_Matches()
    {
        var validUrls = new[]
        {
            "https://example.com",
            "http://example.com",
            "https://www.example.com",
            "example.com",
            "https://example.com/path",
            "https://example.com/path/to/resource",
            "http://sub.example.co.uk"
        };

        foreach (var url in validUrls)
        {
            await Assert.That(RegexHelper.UrlPattern.IsMatch(url)).IsTrue();
        }
    }

    [Test]
    public async Task UrlPattern_WithInvalidUrls_DoesNotMatch()
    {
        var invalidUrls = new[]
        {
            "not a url",
            "ftp://example.com", // FTP not supported
            "://example.com",
            "http://"
        };

        foreach (var url in invalidUrls)
        {
            await Assert.That(RegexHelper.UrlPattern.IsMatch(url)).IsFalse();
        }
    }

    #endregion

    #region AlphanumericPattern Tests

    [Test]
    public async Task AlphanumericPattern_WithAlphanumeric_Matches()
    {
        var validInputs = new[] { "abc123", "ABC", "123", "Test123" };

        foreach (var input in validInputs)
        {
            await Assert.That(RegexHelper.AlphanumericPattern.IsMatch(input)).IsTrue();
        }
    }

    [Test]
    public async Task AlphanumericPattern_WithNonAlphanumeric_DoesNotMatch()
    {
        var invalidInputs = new[] { "abc-123", "test space", "test@123", "test.123", "" };

        foreach (var input in invalidInputs)
        {
            await Assert.That(RegexHelper.AlphanumericPattern.IsMatch(input)).IsFalse();
        }
    }

    #endregion

    #region NumericPattern Tests

    [Test]
    public async Task NumericPattern_WithNumbers_Matches()
    {
        var validInputs = new[] { "123", "0", "999999" };

        foreach (var input in validInputs)
        {
            await Assert.That(RegexHelper.NumericPattern.IsMatch(input)).IsTrue();
        }
    }

    [Test]
    public async Task NumericPattern_WithNonNumbers_DoesNotMatch()
    {
        var invalidInputs = new[] { "abc", "12.34", "-123", "12 34", "", "12a" };

        foreach (var input in invalidInputs)
        {
            await Assert.That(RegexHelper.NumericPattern.IsMatch(input)).IsFalse();
        }
    }

    #endregion

    #region HexPattern Tests

    [Test]
    public async Task HexPattern_WithHexadecimal_Matches()
    {
        var validInputs = new[] { "123ABC", "abc", "ABC", "123", "0", "DEADBEEF", "cafebabe" };

        foreach (var input in validInputs)
        {
            await Assert.That(RegexHelper.HexPattern.IsMatch(input)).IsTrue();
        }
    }

    [Test]
    public async Task HexPattern_WithNonHexadecimal_DoesNotMatch()
    {
        var invalidInputs = new[] { "XYZ", "12G", "test", "", "12 34" };

        foreach (var input in invalidInputs)
        {
            await Assert.That(RegexHelper.HexPattern.IsMatch(input)).IsFalse();
        }
    }

    #endregion

    #region GuidPattern Tests

    [Test]
    public async Task GuidPattern_WithValidGuids_Matches()
    {
        var validGuids = new[]
        {
            "12345678-1234-1234-1234-123456789012",
            "{12345678-1234-1234-1234-123456789012}",
            "AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE",
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString("B")
        };

        foreach (var guid in validGuids)
        {
            await Assert.That(RegexHelper.GuidPattern.IsMatch(guid)).IsTrue();
        }
    }

    [Test]
    public async Task GuidPattern_WithInvalidGuids_DoesNotMatch()
    {
        var invalidGuids = new[]
        {
            "invalid",
            "12345678-1234-1234-1234",
            "12345678-1234-1234-1234-12345678901", // too short
            "XXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
        };

        foreach (var guid in invalidGuids)
        {
            await Assert.That(RegexHelper.GuidPattern.IsMatch(guid)).IsFalse();
        }
    }

    #endregion

    #region WhitespacePattern Tests

    [Test]
    public async Task WhitespacePattern_WithWhitespace_Matches()
    {
        var inputs = new[] { " ", "  ", "\t", "\n", "\r\n", "   " };

        foreach (var input in inputs)
        {
            await Assert.That(RegexHelper.WhitespacePattern.IsMatch(input)).IsTrue();
        }
    }

    [Test]
    public async Task WhitespacePattern_ReplacesMultipleSpaces()
    {
        var input = "hello    world";
        var result = RegexHelper.WhitespacePattern.Replace(input, " ");

        await Assert.That(result).IsEqualTo("hello world");
    }

    #endregion

    #region MultipleWhitespacePattern Tests

    [Test]
    public async Task MultipleWhitespacePattern_WithMultipleWhitespace_Matches()
    {
        var inputs = new[] { "  ", "   ", "\t\t", "\n\n" };

        foreach (var input in inputs)
        {
            await Assert.That(RegexHelper.MultipleWhitespacePattern.IsMatch(input)).IsTrue();
        }
    }

    [Test]
    public async Task MultipleWhitespacePattern_WithSingleWhitespace_DoesNotMatch()
    {
        var inputs = new[] { " ", "\t", "\n" };

        foreach (var input in inputs)
        {
            await Assert.That(RegexHelper.MultipleWhitespacePattern.IsMatch(input)).IsFalse();
        }
    }

    [Test]
    public async Task MultipleWhitespacePattern_CollapsesMultipleSpaces()
    {
        var input = "hello    world    test";
        var result = RegexHelper.MultipleWhitespacePattern.Replace(input, " ");

        await Assert.That(result).IsEqualTo("hello world test");
    }

    #endregion

    #region GetOrAdd Tests

    [Test]
    public async Task GetOrAdd_CreatesNewRegex()
    {
        var testKey = $"test_{Guid.NewGuid()}";
        var regex = RegexHelper.GetOrAdd(testKey, @"^\d+$");

        await Assert.That(regex).IsNotNull();
        await Assert.That(regex.IsMatch("123")).IsTrue();
        await Assert.That(regex.IsMatch("abc")).IsFalse();
        
        RegexHelper.Remove(testKey);
    }

    [Test]
    public async Task GetOrAdd_ReturnsCachedRegex()
    {
        var testKey = $"test_{Guid.NewGuid()}";
        var regex1 = RegexHelper.GetOrAdd(testKey, @"^\d+$");
        var regex2 = RegexHelper.GetOrAdd(testKey, @"^\d+$");

        await Assert.That(ReferenceEquals(regex1, regex2)).IsTrue();
        
        RegexHelper.Remove(testKey);
    }

    [Test]
    public async Task GetOrAdd_WithDifferentKeys_CreatesDifferentRegexes()
    {
        var testKey1 = $"test1_{Guid.NewGuid()}";
        var testKey2 = $"test2_{Guid.NewGuid()}";
        var regex1 = RegexHelper.GetOrAdd(testKey1, @"^\d+$");
        var regex2 = RegexHelper.GetOrAdd(testKey2, @"^\d+$");

        await Assert.That(ReferenceEquals(regex1, regex2)).IsFalse();
        
        RegexHelper.Remove(testKey1);
        RegexHelper.Remove(testKey2);
    }

    [Test]
    public async Task GetOrAdd_WithOptions_UsesOptions()
    {
        var testKey = $"caseInsensitive_{Guid.NewGuid()}";
        var regex = RegexHelper.GetOrAdd(testKey, @"^test$", RegexOptions.IgnoreCase);

        await Assert.That(regex.IsMatch("TEST")).IsTrue();
        await Assert.That(regex.IsMatch("test")).IsTrue();
        
        RegexHelper.Remove(testKey);
    }

    #endregion

    #region TryGet Tests

    [Test]
    public async Task TryGet_WithExistingKey_ReturnsTrue()
    {
        var testKey = $"test_{Guid.NewGuid()}";
        RegexHelper.GetOrAdd(testKey, @"^\d+$");
        
        var success = RegexHelper.TryGet(testKey, out var regex);

        await Assert.That(success).IsTrue();
        await Assert.That(regex).IsNotNull();
        
        RegexHelper.Remove(testKey);
    }

    [Test]
    public async Task TryGet_WithNonExistingKey_ReturnsFalse()
    {
        var nonExistentKey = $"nonexistent_{Guid.NewGuid()}";
        var success = RegexHelper.TryGet(nonExistentKey, out var regex);

        await Assert.That(success).IsFalse();
        await Assert.That(regex).IsNull();
    }

    #endregion

    #region Remove Tests

    [Test]
    public async Task Remove_WithExistingKey_ReturnsTrue()
    {
        var testKey = $"test_{Guid.NewGuid()}";
        RegexHelper.GetOrAdd(testKey, @"^\d+$");
        
        var removed = RegexHelper.Remove(testKey);

        await Assert.That(removed).IsTrue();
        await Assert.That(RegexHelper.TryGet(testKey, out _)).IsFalse();
    }

    [Test]
    public async Task Remove_WithNonExistingKey_ReturnsFalse()
    {
        var nonExistentKey = $"nonexistent_{Guid.NewGuid()}";
        var removed = RegexHelper.Remove(nonExistentKey);

        await Assert.That(removed).IsFalse();
    }

    #endregion

    #region ClearCache Tests

    [Test]
    public async Task ClearCache_RemovesAllCachedRegexes()
    {
        var testKey1 = $"cleartest1_{Guid.NewGuid()}";
        var testKey2 = $"cleartest2_{Guid.NewGuid()}";
        
        RegexHelper.GetOrAdd(testKey1, @"^\d+$");
        RegexHelper.GetOrAdd(testKey2, @"^[a-z]+$");
        
        // Verify they exist
        await Assert.That(RegexHelper.TryGet(testKey1, out _)).IsTrue();
        await Assert.That(RegexHelper.TryGet(testKey2, out _)).IsTrue();
        
        RegexHelper.ClearCache();

        // After clearing, our test keys should not exist
        await Assert.That(RegexHelper.TryGet(testKey1, out _)).IsFalse();
        await Assert.That(RegexHelper.TryGet(testKey2, out _)).IsFalse();
    }

    #endregion

    #region CacheCount Tests

    [Test]
    public async Task CacheCount_ReturnsCorrectCount()
    {
        var testKey1 = $"counttest1_{Guid.NewGuid()}";
        var testKey2 = $"counttest2_{Guid.NewGuid()}";
        try
        {
            // Test 1: Adding a key increases count
            var countBeforeAdd = RegexHelper.CacheCount;
            RegexHelper.GetOrAdd(testKey1, @"^\d+$");
            await Assert.That(RegexHelper.TryGet(testKey1, out _)).IsTrue();
            var countAfterAdd = RegexHelper.CacheCount;
            // Count should increase by at least 1 (may be more if other tests are running)
            await Assert.That(countAfterAdd).IsGreaterThanOrEqualTo(countBeforeAdd + 1);

            // Test 2: Adding another unique key increases count
            var countBeforeSecondAdd = RegexHelper.CacheCount;
            RegexHelper.GetOrAdd(testKey2, @"^[a-z]+$");
            await Assert.That(RegexHelper.TryGet(testKey2, out _)).IsTrue();
            var countAfterSecondAdd = RegexHelper.CacheCount;
            await Assert.That(countAfterSecondAdd).IsGreaterThanOrEqualTo(countBeforeSecondAdd + 1);

            // Test 3: Removing a key decreases count (only if it still exists)
            if (RegexHelper.TryGet(testKey1, out _))
            {
                var countBeforeRemove = RegexHelper.CacheCount;
                var removed = RegexHelper.Remove(testKey1);
                if (removed)
                {
                    await Assert.That(RegexHelper.TryGet(testKey1, out _)).IsFalse();
                    var countAfterRemove = RegexHelper.CacheCount;
                    await Assert.That(countAfterRemove).IsEqualTo(countBeforeRemove - 1);
                }
            }
        }
        finally
        {
            // Cleanup - remove keys if they still exist
            RegexHelper.Remove(testKey1);
            RegexHelper.Remove(testKey2);
        }
    }

    #endregion

    #region IsMatch Tests

    [Test]
    public async Task IsMatch_WithMatchingInput_ReturnsTrue()
    {
        var isMatch = RegexHelper.IsMatch("123", @"^\d+$");

        await Assert.That(isMatch).IsTrue();
    }

    [Test]
    public async Task IsMatch_WithNonMatchingInput_ReturnsFalse()
    {
        var isMatch = RegexHelper.IsMatch("abc", @"^\d+$");

        await Assert.That(isMatch).IsFalse();
    }

    [Test]
    public async Task IsMatch_WithNull_ReturnsFalse()
    {
        var isMatch = RegexHelper.IsMatch(null, @"^\d+$");

        await Assert.That(isMatch).IsFalse();
    }

    [Test]
    public async Task IsMatch_WithEmptyString_ReturnsFalse()
    {
        var isMatch = RegexHelper.IsMatch(string.Empty, @"^\d+$");

        await Assert.That(isMatch).IsFalse();
    }

    [Test]
    public async Task IsMatch_WithOptions_UsesOptions()
    {
        var isMatch = RegexHelper.IsMatch("TEST", @"^test$", RegexOptions.IgnoreCase);

        await Assert.That(isMatch).IsTrue();
    }

    #endregion

    #region Matches Tests

    [Test]
    public async Task Matches_ReturnsAllMatches()
    {
        var matches = RegexHelper.Matches("123 abc 456", @"\d+");

        await Assert.That(matches.Count).IsEqualTo(2);
        await Assert.That(matches[0].Value).IsEqualTo("123");
        await Assert.That(matches[1].Value).IsEqualTo("456");
    }

    [Test]
    public async Task Matches_WithNoMatches_ReturnsEmptyCollection()
    {
        var matches = RegexHelper.Matches("abc", @"\d+");

        await Assert.That(matches.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Matches_WithNull_ReturnsEmptyCollection()
    {
        var matches = RegexHelper.Matches(null!, @"\d+");

        await Assert.That(matches.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Matches_WithEmptyString_ReturnsEmptyCollection()
    {
        var matches = RegexHelper.Matches(string.Empty, @"\d+");

        await Assert.That(matches.Count).IsEqualTo(0);
    }

    #endregion

    #region Replace Tests

    [Test]
    public async Task Replace_ReplacesMatches()
    {
        var result = RegexHelper.Replace("123 abc 456", @"\d+", "X");

        await Assert.That(result).IsEqualTo("X abc X");
    }

    [Test]
    public async Task Replace_WithNoMatches_ReturnsOriginal()
    {
        var input = "abc def";
        var result = RegexHelper.Replace(input, @"\d+", "X");

        await Assert.That(result).IsEqualTo(input);
    }

    [Test]
    public async Task Replace_WithNull_ReturnsEmptyString()
    {
        var result = RegexHelper.Replace(null, @"\d+", "X");

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Replace_WithEmptyString_ReturnsEmptyString()
    {
        var result = RegexHelper.Replace(string.Empty, @"\d+", "X");

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Replace_WithOptions_UsesOptions()
    {
        var result = RegexHelper.Replace("TEST test", @"test", "X", RegexOptions.IgnoreCase);

        await Assert.That(result).IsEqualTo("X X");
    }

    #endregion

    #region Split Tests

    [Test]
    public async Task Split_SplitsString()
    {
        var result = RegexHelper.Split("one,two,three", @",");

        await Assert.That(result.Length).IsEqualTo(3);
        await Assert.That(result[0]).IsEqualTo("one");
        await Assert.That(result[1]).IsEqualTo("two");
        await Assert.That(result[2]).IsEqualTo("three");
    }

    [Test]
    public async Task Split_WithNoMatches_ReturnsOriginalArray()
    {
        var result = RegexHelper.Split("abc", @"\d+");

        await Assert.That(result.Length).IsEqualTo(1);
        await Assert.That(result[0]).IsEqualTo("abc");
    }

    [Test]
    public async Task Split_WithNull_ReturnsEmptyArray()
    {
        var result = RegexHelper.Split(null, @",");

        await Assert.That(result.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Split_WithEmptyString_ReturnsEmptyArray()
    {
        var result = RegexHelper.Split(string.Empty, @",");

        await Assert.That(result.Length).IsEqualTo(0);
    }

    #endregion

    #region Escape Tests

    [Test]
    public async Task Escape_EscapesSpecialCharacters()
    {
        var result = RegexHelper.Escape("test.com");

        await Assert.That(result).IsEqualTo(@"test\.com");
    }

    [Test]
    public async Task Escape_WithMultipleSpecialChars_EscapesAll()
    {
        var result = RegexHelper.Escape("a+b*c?d");

        await Assert.That(result).Contains(@"\+");
        await Assert.That(result).Contains(@"\*");
        await Assert.That(result).Contains(@"\?");
    }

    [Test]
    public async Task Escape_WithNull_ReturnsEmptyString()
    {
        var result = RegexHelper.Escape(null);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Escape_WithEmptyString_ReturnsEmptyString()
    {
        var result = RegexHelper.Escape(string.Empty);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Unescape Tests

    [Test]
    public async Task Unescape_UnescapesEscapedCharacters()
    {
        var result = RegexHelper.Unescape(@"test\.com");

        await Assert.That(result).IsEqualTo("test.com");
    }

    [Test]
    public async Task Unescape_WithMultipleEscapes_UnescapesAll()
    {
        var input = @"\t\n\r";
        var result = RegexHelper.Unescape(input);

        await Assert.That(result).IsEqualTo("\t\n\r");
    }

    [Test]
    public async Task Unescape_WithNull_ReturnsEmptyString()
    {
        var result = RegexHelper.Unescape(null);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Unescape_WithEmptyString_ReturnsEmptyString()
    {
        var result = RegexHelper.Unescape(string.Empty);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    #endregion

    #region Integration Tests

    [Test]
    public async Task EscapeAndUnescape_RoundTrip_PreservesInput()
    {
        var original = "test.com?query=1+2";
        var escaped = RegexHelper.Escape(original);
        var unescaped = RegexHelper.Unescape(escaped);

        await Assert.That(unescaped).IsEqualTo(original);
    }

    [Test]
    public async Task CachedPattern_CanBeRetrievedAndUsed()
    {
        var testKey = $"digits_{Guid.NewGuid()}";
        RegexHelper.GetOrAdd(testKey, @"^\d+$");
        
        var success = RegexHelper.TryGet(testKey, out var regex);

        await Assert.That(success).IsTrue();
        await Assert.That(regex!.IsMatch("123")).IsTrue();
        await Assert.That(regex.IsMatch("abc")).IsFalse();
        
        RegexHelper.Remove(testKey);
    }

    #endregion
}
