using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation
{
    public static class InternetValidationHelper
    {
        public static bool IsValidUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out var uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsValidEmail(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }

        public static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        public static bool IsValidDomainName(string domainName)
        {
            var domainRegex = new Regex(
                "^(?!-)[A-Za-z0-9-]{1,63}(?<!-)\\.[A-Za-z]{2,6}$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase
            );
            return domainRegex.IsMatch(domainName);
        }

        public static bool IsValidPortNumber(int port)
        {
            return port >= 0 && port <= 65535;
        }

        public static bool IsDnsResolvable(string domainName)
        {
            try
            {
                var addresses = Dns.GetHostAddresses(domainName);
                return addresses.Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}