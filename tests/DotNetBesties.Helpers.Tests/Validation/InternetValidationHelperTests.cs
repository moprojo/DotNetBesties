using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Validation;

namespace DotNetBesties.Helpers.Tests.Validation
{
    public class InternetValidationHelperTests
    {
        [Test]
        public async Task IsValidUrl_ShouldReturnTrueForValidUrls()
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
        public async Task IsValidUrl_ShouldReturnFalseForInvalidUrls()
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
        public async Task IsValidEmail_ShouldReturnTrueForValidEmails()
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
        public async Task IsValidEmail_ShouldReturnFalseForInvalidEmails()
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
        public async Task IsValidIpAddress_ShouldReturnTrueForValidIpAddresses()
        {
            var validIps = new[]
            {
                "192.168.1.1",
                "255.255.255.255",
                "0.0.0.0",
                "::1",
                "2001:db8::ff00:42:8329"
            };

            foreach (var ip in validIps)
            {
                await Assert.That(InternetValidationHelper.IsValidIpAddress(ip)).IsTrue();
            }
        }

        [Test]
        public async Task IsValidIpAddress_ShouldReturnFalseForInvalidIpAddresses()
        {
            var invalidIps = new[]
            {
                "999.999.999.999",
                "256.256.256.256",
                "abcd::1234",
                "",
                "not-an-ip"
            };

            foreach (var ip in invalidIps)
            {
                await Assert.That(InternetValidationHelper.IsValidIpAddress(ip)).IsFalse();
            }
        }

        [Test]
        public async Task IsValidDomainName_ShouldReturnTrueForValidDomainNames()
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
        public async Task IsValidDomainName_ShouldReturnFalseForInvalidDomainNames()
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
        public async Task IsValidPortNumber_ShouldReturnTrueForValidPorts()
        {
            var validPorts = new[] { 0, 80, 443, 65535 };

            foreach (var port in validPorts)
            {
                await Assert.That(InternetValidationHelper.IsValidPortNumber(port)).IsTrue();
            }
        }

        [Test]
        public async Task IsValidPortNumber_ShouldReturnFalseForInvalidPorts()
        {
            var invalidPorts = new[] { -1, 65536, 99999 };

            foreach (var port in invalidPorts)
            {
                await Assert.That(InternetValidationHelper.IsValidPortNumber(port)).IsFalse();
            }
        }

        [Test]
        public async Task IsDnsResolvable_ShouldReturnTrueForResolvableDomains()
        {
            var resolvableDomains = new[] { "example.com", "google.com" };

            foreach (var domain in resolvableDomains)
            {
                await Assert.That(InternetValidationHelper.IsDnsResolvable(domain)).IsTrue();
            }
        }

        [Test]
        public async Task IsDnsResolvable_ShouldReturnFalseForUnresolvableDomains()
        {
            var unresolvableDomains = new[] { "nonexistent.domain", "invalid" };

            foreach (var domain in unresolvableDomains)
            {
                await Assert.That(InternetValidationHelper.IsDnsResolvable(domain)).IsFalse();
            }
        }
    }
}