using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation;

public class InternetValidationHelperTests
{
    #region IsValidUri Tests

    [Test]
    public async Task IsValidUri_WithValidUrls_ReturnsTrue()
    {
        var validUrls = new[]
        {
            "http://example.com",
            "https://example.com",
            "https://example.com/path",
            "http://example.com:8080",
            "example.com"
        };

        foreach (var url in validUrls)
        {
            await Assert.That(InternetValidationHelper.IsValidUri(url)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidUri_WithInvalidUrls_ReturnsFalse()
    {
        var invalidUrls = new[]
        {
            "htp://example.com",
            "http:/example.com",
            "example",
            "http://",
            ""
        };

        foreach (var url in invalidUrls)
        {
            await Assert.That(InternetValidationHelper.IsValidUri(url)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidUri_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidUri(null)).IsFalse();
    }

    [Test]
    public async Task IsValidUri_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidUri("   ")).IsFalse();
    }

    #endregion

    #region IsValidUriWithScheme Tests

    [Test]
    public async Task IsValidUriWithScheme_WithValidHttps_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidUriWithScheme("https://example.com", "https")).IsTrue();
    }

    [Test]
    public async Task IsValidUriWithScheme_WithValidFtp_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidUriWithScheme("ftp://example.com", "ftp", "ftps")).IsTrue();
    }

    [Test]
    public async Task IsValidUriWithScheme_WithWrongScheme_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidUriWithScheme("http://example.com", "https")).IsFalse();
    }

    [Test]
    public async Task IsValidUriWithScheme_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidUriWithScheme(null, "https")).IsFalse();
    }

    [Test]
    public async Task IsValidUriWithScheme_WithEmptySchemes_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidUriWithScheme("https://example.com")).IsFalse();
    }

    #endregion

    #region IsValidEmail Tests

    [Test]
    public async Task IsValidEmail_WithValidEmails_ReturnsTrue()
    {
        var validEmails = new[]
        {
            "user@example.com",
            "user.name+tag@example.co.uk",
            "user_name@example.org",
            "user-name@example.io"
        };

        foreach (var email in validEmails)
        {
            await Assert.That(InternetValidationHelper.IsValidEmail(email)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidEmail_WithInvalidEmails_ReturnsFalse()
    {
        var invalidEmails = new[]
        {
            "user@.com",
            "user@com",
            "@example.com",
            "user@",
            "user@com@com",
            ""
        };

        foreach (var email in invalidEmails)
        {
            await Assert.That(InternetValidationHelper.IsValidEmail(email)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidEmail_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidEmail(null)).IsFalse();
    }

    [Test]
    public async Task IsValidEmail_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidEmail("   ")).IsFalse();
    }

    #endregion

    #region IsValidIpAddress Tests

    [Test]
    public async Task IsValidIpAddress_WithValidIPv4_ReturnsTrue()
    {
        var validIps = new[]
        {
            "192.168.1.1",
            "255.255.255.255",
            "0.0.0.0",
            "127.0.0.1"
        };

        foreach (var ip in validIps)
        {
            await Assert.That(InternetValidationHelper.IsValidIpAddress(ip)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidIpAddress_WithValidIPv6_ReturnsTrue()
    {
        var validIps = new[]
        {
            "::1",
            "2001:db8::ff00:42:8329",
            "fe80::1"
        };

        foreach (var ip in validIps)
        {
            await Assert.That(InternetValidationHelper.IsValidIpAddress(ip)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidIpAddress_WithInvalidAddresses_ReturnsFalse()
    {
        var invalidIps = new[]
        {
            "999.999.999.999",
            "256.256.256.256",
            "g::1",
            "",
            "not-an-ip"
        };

        foreach (var ip in invalidIps)
        {
            await Assert.That(InternetValidationHelper.IsValidIpAddress(ip)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidIpAddress_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIpAddress(null)).IsFalse();
    }

    [Test]
    public async Task IsValidIpAddress_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIpAddress("   ")).IsFalse();
    }

    #endregion

    #region IsValidIPv4Address Tests

    [Test]
    public async Task IsValidIPv4Address_WithValidIPv4_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv4Address("192.168.1.1")).IsTrue();
    }

    [Test]
    public async Task IsValidIPv4Address_WithIPv6_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv4Address("::1")).IsFalse();
    }

    [Test]
    public async Task IsValidIPv4Address_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv4Address(null)).IsFalse();
    }

    #endregion

    #region IsValidIPv6Address Tests

    [Test]
    public async Task IsValidIPv6Address_WithValidIPv6_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv6Address("::1")).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidIPv6Address("2001:db8::ff00:42:8329")).IsTrue();
    }

    [Test]
    public async Task IsValidIPv6Address_WithIPv4_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv6Address("192.168.1.1")).IsFalse();
    }

    [Test]
    public async Task IsValidIPv6Address_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidIPv6Address(null)).IsFalse();
    }

    #endregion

    #region IsValidDomainName Tests

    [Test]
    public async Task IsValidDomainName_WithValidDomains_ReturnsTrue()
    {
        var validDomains = new[]
        {
            "example.com",
            "sub.example.com",
            "example.co.uk",
            "example.io"
        };

        foreach (var domain in validDomains)
        {
            await Assert.That(InternetValidationHelper.IsValidDomainName(domain)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidDomainName_WithInvalidDomains_ReturnsFalse()
    {
        var invalidDomains = new[]
        {
            "-example.com",
            "example-.com",
            "example",
            "",
            "com"
        };

        foreach (var domain in invalidDomains)
        {
            await Assert.That(InternetValidationHelper.IsValidDomainName(domain)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidDomainName_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidDomainName(null)).IsFalse();
    }

    [Test]
    public async Task IsValidDomainName_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidDomainName("   ")).IsFalse();
    }

    #endregion

    #region IsValidMacAddress Tests

    [Test]
    public async Task IsValidMacAddress_WithColonFormat_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidMacAddress("00:1A:2B:3C:4D:5E")).IsTrue();
    }

    [Test]
    public async Task IsValidMacAddress_WithDashFormat_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidMacAddress("00-1A-2B-3C-4D-5E")).IsTrue();
    }

    [Test]
    public async Task IsValidMacAddress_WithNoSeparator_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidMacAddress("001A2B3C4D5E")).IsTrue();
    }

    [Test]
    public async Task IsValidMacAddress_WithLowercase_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidMacAddress("00:1a:2b:3c:4d:5e")).IsTrue();
    }

    [Test]
    public async Task IsValidMacAddress_WithInvalidFormats_ReturnsFalse()
    {
        var invalidMacs = new[]
        {
            "00:1A:2B:3C:4D",      // Too short
            "00:1A:2B:3C:4D:5E:6F", // Too long
            "00:1G:2B:3C:4D:5E",    // Invalid hex character
            "001A2B3C4D5",          // Too short without separator
            "",
            "not-a-mac"
        };

        foreach (var mac in invalidMacs)
        {
            await Assert.That(InternetValidationHelper.IsValidMacAddress(mac)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidMacAddress_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidMacAddress(null)).IsFalse();
    }

    #endregion

    #region IsValidPortNumber Tests

    [Test]
    public async Task IsValidPortNumber_WithValidPorts_ReturnsTrue()
    {
        var validPorts = new[] { 0, 80, 443, 8080, 65535 };

        foreach (var port in validPorts)
        {
            await Assert.That(InternetValidationHelper.IsValidPortNumber(port)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidPortNumber_WithInvalidPorts_ReturnsFalse()
    {
        var invalidPorts = new[] { -1, 65536, 99999, int.MinValue, int.MaxValue };

        foreach (var port in invalidPorts)
        {
            await Assert.That(InternetValidationHelper.IsValidPortNumber(port)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidPortNumber_String_WithValidPort_ReturnsTrue()
    {
        await Assert.That(InternetValidationHelper.IsValidPortNumber("8080")).IsTrue();
    }

    [Test]
    public async Task IsValidPortNumber_String_WithInvalidPort_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidPortNumber("70000")).IsFalse();
        await Assert.That(InternetValidationHelper.IsValidPortNumber("abc")).IsFalse();
    }

    [Test]
    public async Task IsValidPortNumber_String_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidPortNumber(null as string)).IsFalse();
    }

    [Test]
    public async Task IsValidPortNumber_String_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsValidPortNumber("   ")).IsFalse();
    }

    #endregion

    #region IsDnsResolvable Tests

    [Test]
    public async Task IsDnsResolvable_WithResolvableDomains_ReturnsTrue()
    {
        var resolvableDomains = new[] { "example.com", "google.com" };

        foreach (var domain in resolvableDomains)
        {
            await Assert.That(InternetValidationHelper.IsDnsResolvable(domain)).IsTrue();
        }
    }

    [Test]
    public async Task IsDnsResolvable_WithUnresolvableDomains_ReturnsFalse()
    {
        var unresolvableDomains = new[] { "nonexistent.invalid.domain.test", "this-domain-does-not-exist-12345.com" };

        foreach (var domain in unresolvableDomains)
        {
            await Assert.That(InternetValidationHelper.IsDnsResolvable(domain)).IsFalse();
        }
    }

    [Test]
    public async Task IsDnsResolvable_WithNull_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsDnsResolvable(null)).IsFalse();
    }

    [Test]
    public async Task IsDnsResolvable_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(InternetValidationHelper.IsDnsResolvable("   ")).IsFalse();
    }

    #endregion
}