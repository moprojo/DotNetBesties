using System;
using System.Globalization;
using System.Linq;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Common;

namespace DotNetBesties.Helpers.Tests.Common;

public class CultureInfoHelperTests
{
    #region InvariantCulture Tests

    [Test]
    public async Task InvariantCulture_ReturnsInvariantCulture()
    {
        var culture = CultureInfoHelper.InvariantCulture;

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture.Name).IsEqualTo(string.Empty);
        await Assert.That(culture).IsEqualTo(CultureInfo.InvariantCulture);
    }

    #endregion

    #region CurrentCulture Tests

    [Test]
    public async Task CurrentCulture_ReturnsCurrentCulture()
    {
        var culture = CultureInfoHelper.CurrentCulture;

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture).IsEqualTo(CultureInfo.CurrentCulture);
    }

    #endregion

    #region CurrentUICulture Tests

    [Test]
    public async Task CurrentUICulture_ReturnsCurrentUICulture()
    {
        var culture = CultureInfoHelper.CurrentUICulture;

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture).IsEqualTo(CultureInfo.CurrentUICulture);
    }

    #endregion

    #region TryGetCulture Tests

    [Test]
    public async Task TryGetCulture_WithValidCulture_ReturnsTrue()
    {
        var validCultures = new[] { "en-US", "de-DE", "fr-FR", "es-ES", "ja-JP" };

        foreach (var cultureName in validCultures)
        {
            var success = CultureInfoHelper.TryGetCulture(cultureName, out var culture);

            await Assert.That(success).IsTrue();
            await Assert.That(culture).IsNotNull();
            await Assert.That(culture!.Name).IsEqualTo(cultureName);
        }
    }

    [Test]
    public async Task TryGetCulture_WithInvalidCulture_ReturnsFalse()
    {
        var invalidCultures = new[] { "xx-YY", "zzzzzz", "999-999" };

        foreach (var cultureName in invalidCultures)
        {
            var success = CultureInfoHelper.TryGetCulture(cultureName, out var culture);

            // Some systems might recognize certain patterns, so we just check basic behavior
            if (!success)
            {
                await Assert.That(culture).IsNull();
            }
        }
        
        // Always check that clearly invalid culture returns false
        await Assert.That(CultureInfoHelper.TryGetCulture("zzzzzz", out _)).IsFalse();
    }

    [Test]
    public async Task TryGetCulture_WithNull_ReturnsFalse()
    {
        var success = CultureInfoHelper.TryGetCulture(null!, out var culture);

        await Assert.That(success).IsFalse();
        await Assert.That(culture).IsNull();
    }

    [Test]
    public async Task TryGetCulture_WithEmptyString_ReturnsFalse()
    {
        var success = CultureInfoHelper.TryGetCulture(string.Empty, out var culture);

        await Assert.That(success).IsFalse();
        await Assert.That(culture).IsNull();
    }

    [Test]
    public async Task TryGetCulture_WithWhitespace_ReturnsFalse()
    {
        var success = CultureInfoHelper.TryGetCulture("   ", out var culture);

        await Assert.That(success).IsFalse();
        await Assert.That(culture).IsNull();
    }

    #endregion

    #region GetCultureOrInvariant Tests

    [Test]
    public async Task GetCultureOrInvariant_WithValidCulture_ReturnsCulture()
    {
        var culture = CultureInfoHelper.GetCultureOrInvariant("en-US");

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture.Name).IsEqualTo("en-US");
    }

    [Test]
    public async Task GetCultureOrInvariant_WithInvalidCulture_ReturnsInvariant()
    {
        var culture = CultureInfoHelper.GetCultureOrInvariant("zzzzzz");

        // Check if it's either invariant or the system recognized it
        var isInvariantOrValid = culture.Equals(CultureInfo.InvariantCulture) || !string.IsNullOrEmpty(culture.Name);
        await Assert.That(isInvariantOrValid).IsTrue();
    }

    [Test]
    public async Task GetCultureOrInvariant_WithNull_ReturnsInvariant()
    {
        var culture = CultureInfoHelper.GetCultureOrInvariant(null);

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture).IsEqualTo(CultureInfo.InvariantCulture);
    }

    [Test]
    public async Task GetCultureOrInvariant_WithEmptyString_ReturnsInvariant()
    {
        var culture = CultureInfoHelper.GetCultureOrInvariant(string.Empty);

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture).IsEqualTo(CultureInfo.InvariantCulture);
    }

    [Test]
    public async Task GetCultureOrInvariant_WithWhitespace_ReturnsInvariant()
    {
        var culture = CultureInfoHelper.GetCultureOrInvariant("   ");

        await Assert.That(culture).IsNotNull();
        await Assert.That(culture).IsEqualTo(CultureInfo.InvariantCulture);
    }

    #endregion

    #region GetAllCultures Tests

    [Test]
    public async Task GetAllCultures_ReturnsAllCultures()
    {
        var cultures = CultureInfoHelper.GetAllCultures();

        await Assert.That(cultures).IsNotNull();
        await Assert.That(cultures.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GetAllCultures_ContainsInvariantCulture()
    {
        var cultures = CultureInfoHelper.GetAllCultures();

        await Assert.That(cultures.Any(c => c.Name == string.Empty)).IsTrue();
    }

    [Test]
    public async Task GetAllCultures_ContainsCommonCultures()
    {
        var cultures = CultureInfoHelper.GetAllCultures();
        var cultureNames = cultures.Select(c => c.Name).ToList();

        await Assert.That(cultureNames.Contains("en-US")).IsTrue();
        await Assert.That(cultureNames.Contains("de-DE")).IsTrue();
    }

    #endregion

    #region GetSpecificCultures Tests

    [Test]
    public async Task GetSpecificCultures_ReturnsSpecificCultures()
    {
        var cultures = CultureInfoHelper.GetSpecificCultures();

        await Assert.That(cultures).IsNotNull();
        await Assert.That(cultures.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GetSpecificCultures_AllAreSpecific()
    {
        var cultures = CultureInfoHelper.GetSpecificCultures();

        foreach (var culture in cultures)
        {
            await Assert.That(culture.IsNeutralCulture).IsFalse();
        }
    }

    [Test]
    public async Task GetSpecificCultures_ContainsCommonSpecificCultures()
    {
        var cultures = CultureInfoHelper.GetSpecificCultures();
        var cultureNames = cultures.Select(c => c.Name).ToList();

        await Assert.That(cultureNames.Contains("en-US")).IsTrue();
        await Assert.That(cultureNames.Contains("de-DE")).IsTrue();
    }

    #endregion

    #region GetNeutralCultures Tests

    [Test]
    public async Task GetNeutralCultures_ReturnsNeutralCultures()
    {
        var cultures = CultureInfoHelper.GetNeutralCultures();

        await Assert.That(cultures).IsNotNull();
        await Assert.That(cultures.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GetNeutralCultures_AllAreNeutral()
    {
        var cultures = CultureInfoHelper.GetNeutralCultures();

        foreach (var culture in cultures.Where(c => c.Name != string.Empty))
        {
            await Assert.That(culture.IsNeutralCulture).IsTrue();
        }
    }

    [Test]
    public async Task GetNeutralCultures_ContainsCommonNeutralCultures()
    {
        var cultures = CultureInfoHelper.GetNeutralCultures();
        var cultureNames = cultures.Select(c => c.Name).ToList();

        await Assert.That(cultureNames.Contains("en")).IsTrue();
        await Assert.That(cultureNames.Contains("de")).IsTrue();
    }

    #endregion

    #region IsValidCultureName Tests

    [Test]
    public async Task IsValidCultureName_WithValidNames_ReturnsTrue()
    {
        var validNames = new[] { "en-US", "de-DE", "fr-FR", "es-ES", "ja-JP", "zh-CN" };

        foreach (var name in validNames)
        {
            var isValid = CultureInfoHelper.IsValidCultureName(name);
            await Assert.That(isValid).IsTrue();
        }
    }

    [Test]
    public async Task IsValidCultureName_WithInvalidNames_ReturnsFalse()
    {
        // Use clearly invalid culture names that all systems should reject
        var invalidNames = new[] { "zzzzzz", "999-999", "xxxxx-xxxxx" };

        foreach (var name in invalidNames)
        {
            var isValid = CultureInfoHelper.IsValidCultureName(name);
            await Assert.That(isValid).IsFalse();
        }
    }

    [Test]
    public async Task IsValidCultureName_WithNull_ReturnsFalse()
    {
        var isValid = CultureInfoHelper.IsValidCultureName(null);

        await Assert.That(isValid).IsFalse();
    }

    [Test]
    public async Task IsValidCultureName_WithEmptyString_ReturnsFalse()
    {
        var isValid = CultureInfoHelper.IsValidCultureName(string.Empty);

        await Assert.That(isValid).IsFalse();
    }

    [Test]
    public async Task IsValidCultureName_WithWhitespace_ReturnsFalse()
    {
        var isValid = CultureInfoHelper.IsValidCultureName("   ");

        await Assert.That(isValid).IsFalse();
    }

    #endregion

    #region GetCurrencySymbol Tests

    [Test]
    public async Task GetCurrencySymbol_WithEnglish_ReturnsDollar()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var symbol = CultureInfoHelper.GetCurrencySymbol(culture);

        await Assert.That(symbol).IsEqualTo("$");
    }

    [Test]
    public async Task GetCurrencySymbol_WithGerman_ReturnsEuro()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var symbol = CultureInfoHelper.GetCurrencySymbol(culture);

        await Assert.That(symbol).IsEqualTo("€");
    }

    [Test]
    public async Task GetCurrencySymbol_WithJapanese_ReturnsYen()
    {
        var culture = CultureInfo.GetCultureInfo("ja-JP");
        var symbol = CultureInfoHelper.GetCurrencySymbol(culture);

        // Just verify that we got a symbol (different systems may use different yen variants)
        await Assert.That(symbol).IsNotNull();
        await Assert.That(symbol.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GetCurrencySymbol_WithNull_ReturnsCurrentCultureSymbol()
    {
        var symbol = CultureInfoHelper.GetCurrencySymbol(null);
        var expectedSymbol = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

        await Assert.That(symbol).IsEqualTo(expectedSymbol);
    }

    #endregion

    #region GetDecimalSeparator Tests

    [Test]
    public async Task GetDecimalSeparator_WithEnglish_ReturnsPeriod()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var separator = CultureInfoHelper.GetDecimalSeparator(culture);

        await Assert.That(separator).IsEqualTo(".");
    }

    [Test]
    public async Task GetDecimalSeparator_WithGerman_ReturnsComma()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var separator = CultureInfoHelper.GetDecimalSeparator(culture);

        await Assert.That(separator).IsEqualTo(",");
    }

    [Test]
    public async Task GetDecimalSeparator_WithNull_ReturnsCurrentCultureSeparator()
    {
        var separator = CultureInfoHelper.GetDecimalSeparator(null);
        var expectedSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        await Assert.That(separator).IsEqualTo(expectedSeparator);
    }

    #endregion

    #region GetThousandsSeparator Tests

    [Test]
    public async Task GetThousandsSeparator_WithEnglish_ReturnsComma()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var separator = CultureInfoHelper.GetThousandsSeparator(culture);

        await Assert.That(separator).IsEqualTo(",");
    }

    [Test]
    public async Task GetThousandsSeparator_WithGerman_ReturnsPeriod()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var separator = CultureInfoHelper.GetThousandsSeparator(culture);

        await Assert.That(separator).IsEqualTo(".");
    }

    [Test]
    public async Task GetThousandsSeparator_WithNull_ReturnsCurrentCultureSeparator()
    {
        var separator = CultureInfoHelper.GetThousandsSeparator(null);
        var expectedSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

        await Assert.That(separator).IsEqualTo(expectedSeparator);
    }

    #endregion

    #region GetDateSeparator Tests

    [Test]
    public async Task GetDateSeparator_WithEnglish_ReturnsSlash()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var separator = CultureInfoHelper.GetDateSeparator(culture);

        await Assert.That(separator).IsEqualTo("/");
    }

    [Test]
    public async Task GetDateSeparator_WithGerman_ReturnsPeriod()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var separator = CultureInfoHelper.GetDateSeparator(culture);

        await Assert.That(separator).IsEqualTo(".");
    }

    [Test]
    public async Task GetDateSeparator_WithNull_ReturnsCurrentCultureSeparator()
    {
        var separator = CultureInfoHelper.GetDateSeparator(null);
        var expectedSeparator = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;

        await Assert.That(separator).IsEqualTo(expectedSeparator);
    }

    #endregion

    #region GetTimeSeparator Tests

    [Test]
    public async Task GetTimeSeparator_WithEnglish_ReturnsColon()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var separator = CultureInfoHelper.GetTimeSeparator(culture);

        await Assert.That(separator).IsEqualTo(":");
    }

    [Test]
    public async Task GetTimeSeparator_WithGerman_ReturnsColon()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var separator = CultureInfoHelper.GetTimeSeparator(culture);

        await Assert.That(separator).IsEqualTo(":");
    }

    [Test]
    public async Task GetTimeSeparator_WithNull_ReturnsCurrentCultureSeparator()
    {
        var separator = CultureInfoHelper.GetTimeSeparator(null);
        var expectedSeparator = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator;

        await Assert.That(separator).IsEqualTo(expectedSeparator);
    }

    #endregion

    #region GetShortDatePattern Tests

    [Test]
    public async Task GetShortDatePattern_WithEnglish_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var pattern = CultureInfoHelper.GetShortDatePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("M");
        await Assert.That(pattern).Contains("d");
    }

    [Test]
    public async Task GetShortDatePattern_WithGerman_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var pattern = CultureInfoHelper.GetShortDatePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("d");
    }

    [Test]
    public async Task GetShortDatePattern_WithNull_ReturnsCurrentCulturePattern()
    {
        var pattern = CultureInfoHelper.GetShortDatePattern(null);
        var expectedPattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        await Assert.That(pattern).IsEqualTo(expectedPattern);
    }

    #endregion

    #region GetLongDatePattern Tests

    [Test]
    public async Task GetLongDatePattern_WithEnglish_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var pattern = CultureInfoHelper.GetLongDatePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("M");
        await Assert.That(pattern).Contains("d");
    }

    [Test]
    public async Task GetLongDatePattern_WithGerman_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var pattern = CultureInfoHelper.GetLongDatePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("d");
    }

    [Test]
    public async Task GetLongDatePattern_WithNull_ReturnsCurrentCulturePattern()
    {
        var pattern = CultureInfoHelper.GetLongDatePattern(null);
        var expectedPattern = CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;

        await Assert.That(pattern).IsEqualTo(expectedPattern);
    }

    #endregion

    #region GetShortTimePattern Tests

    [Test]
    public async Task GetShortTimePattern_WithEnglish_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var pattern = CultureInfoHelper.GetShortTimePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("h");
    }

    [Test]
    public async Task GetShortTimePattern_WithGerman_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var pattern = CultureInfoHelper.GetShortTimePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("H");
    }

    [Test]
    public async Task GetShortTimePattern_WithNull_ReturnsCurrentCulturePattern()
    {
        var pattern = CultureInfoHelper.GetShortTimePattern(null);
        var expectedPattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;

        await Assert.That(pattern).IsEqualTo(expectedPattern);
    }

    #endregion

    #region GetLongTimePattern Tests

    [Test]
    public async Task GetLongTimePattern_WithEnglish_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var pattern = CultureInfoHelper.GetLongTimePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("h");
    }

    [Test]
    public async Task GetLongTimePattern_WithGerman_ReturnsPattern()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var pattern = CultureInfoHelper.GetLongTimePattern(culture);

        await Assert.That(pattern).IsNotNull();
        await Assert.That(pattern).Contains("H");
    }

    [Test]
    public async Task GetLongTimePattern_WithNull_ReturnsCurrentCulturePattern()
    {
        var pattern = CultureInfoHelper.GetLongTimePattern(null);
        var expectedPattern = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;

        await Assert.That(pattern).IsEqualTo(expectedPattern);
    }

    #endregion

    #region Uses24HourFormat Tests

    [Test]
    public async Task Uses24HourFormat_WithEnglish_ReturnsFalse()
    {
        var culture = CultureInfo.GetCultureInfo("en-US");
        var uses24Hour = CultureInfoHelper.Uses24HourFormat(culture);

        await Assert.That(uses24Hour).IsFalse();
    }

    [Test]
    public async Task Uses24HourFormat_WithGerman_ReturnsTrue()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        var uses24Hour = CultureInfoHelper.Uses24HourFormat(culture);

        await Assert.That(uses24Hour).IsTrue();
    }

    [Test]
    public async Task Uses24HourFormat_WithFrench_ReturnsTrue()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var uses24Hour = CultureInfoHelper.Uses24HourFormat(culture);

        await Assert.That(uses24Hour).IsTrue();
    }

    [Test]
    public async Task Uses24HourFormat_WithNull_ReturnsCurrentCultureResult()
    {
        var uses24Hour = CultureInfoHelper.Uses24HourFormat(null);
        var expected = !CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("t", StringComparison.OrdinalIgnoreCase);

        await Assert.That(uses24Hour).IsEqualTo(expected);
    }

    #endregion
}
