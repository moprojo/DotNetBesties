using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Validation;

/// <summary>
/// Provides validation methods for internet-related data such as URLs, emails, IP addresses, and domain names.
/// </summary>
public static class InternetValidationHelper
{
    /// <summary>
    /// Compiled regex pattern for validating domain names.
    /// </summary>
    private static readonly Regex DomainNameRegex = new(
        @"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,63}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Compiled regex pattern for validating MAC addresses.
    /// Supports formats: XX:XX:XX:XX:XX:XX, XX-XX-XX-XX-XX-XX, XXXXXXXXXXXX
    /// </summary>
    private static readonly Regex MacAddressRegex = new(
        @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$|^([0-9A-Fa-f]{12})$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Validates whether the specified string is a valid URI (HTTP or HTTPS).
    /// Also accepts domain-style URLs without a scheme prefix.
    /// </summary>
    /// <param name="uri">The URI string to validate.</param>
    /// <returns><c>true</c> if the URI is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidUri(string? uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            return false;

        if (Uri.TryCreate(uri, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            return true;
        }

        // Try prepending http:// to see if it's a valid domain-style url
        return Uri.TryCreate("http://" + uri, UriKind.Absolute, out uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && uriResult.Host.Contains('.');
    }

    /// <summary>
    /// Validates whether the specified string is a valid URI with specific allowed schemes.
    /// </summary>
    /// <param name="uri">The URI string to validate.</param>
    /// <param name="allowedSchemes">The allowed URI schemes (e.g., "http", "https", "ftp").</param>
    /// <returns><c>true</c> if the URI is valid and uses an allowed scheme; otherwise, <c>false</c>.</returns>
    public static bool IsValidUriWithScheme(string? uri, params string[] allowedSchemes)
    {
        if (string.IsNullOrWhiteSpace(uri))
            return false;

        if (allowedSchemes == null || allowedSchemes.Length == 0)
            return false;

        if (!Uri.TryCreate(uri, UriKind.Absolute, out var uriResult))
            return false;

        foreach (var scheme in allowedSchemes)
        {
            if (string.Equals(uriResult.Scheme, scheme, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Validates whether the specified string is a valid email address.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns><c>true</c> if the email address is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return MailAddress.TryCreate(email, out var address)
               && address.Host.Contains('.');
    }

    /// <summary>
    /// Validates whether the specified string is a valid IPv4 or IPv6 address.
    /// </summary>
    /// <param name="ipAddress">The IP address string to validate.</param>
    /// <returns><c>true</c> if the IP address is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidIpAddress(string? ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            return false;

        return IPAddress.TryParse(ipAddress, out _);
    }

    /// <summary>
    /// Validates whether the specified string is a valid IPv4 address.
    /// </summary>
    /// <param name="ipAddress">The IP address string to validate.</param>
    /// <returns><c>true</c> if the IP address is a valid IPv4 address; otherwise, <c>false</c>.</returns>
    public static bool IsValidIPv4Address(string? ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            return false;

        return IPAddress.TryParse(ipAddress, out var address)
               && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// Validates whether the specified string is a valid IPv6 address.
    /// </summary>
    /// <param name="ipAddress">The IP address string to validate.</param>
    /// <returns><c>true</c> if the IP address is a valid IPv6 address; otherwise, <c>false</c>.</returns>
    public static bool IsValidIPv6Address(string? ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            return false;

        return IPAddress.TryParse(ipAddress, out var address)
               && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
    }

    /// <summary>
    /// Validates whether the specified string is a valid domain name.
    /// </summary>
    /// <param name="domainName">The domain name to validate.</param>
    /// <returns><c>true</c> if the domain name is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidDomainName(string? domainName)
    {
        if (string.IsNullOrWhiteSpace(domainName))
            return false;

        return DomainNameRegex.IsMatch(domainName);
    }

    /// <summary>
    /// Validates whether the specified string is a valid MAC address.
    /// Supports formats: XX:XX:XX:XX:XX:XX, XX-XX-XX-XX-XX-XX, and XXXXXXXXXXXX.
    /// </summary>
    /// <param name="macAddress">The MAC address string to validate.</param>
    /// <returns><c>true</c> if the MAC address is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidMacAddress(string? macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return false;

        return MacAddressRegex.IsMatch(macAddress);
    }

    /// <summary>
    /// Validates whether the specified port number is within the valid range (0-65535).
    /// </summary>
    /// <param name="port">The port number to validate.</param>
    /// <returns><c>true</c> if the port number is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidPortNumber(int port)
    {
        return port >= 0 && port <= 65535;
    }

    /// <summary>
    /// Validates whether the specified port number string is a valid port.
    /// </summary>
    /// <param name="port">The port number string to validate.</param>
    /// <returns><c>true</c> if the port number is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidPortNumber(string? port)
    {
        if (string.IsNullOrWhiteSpace(port))
            return false;

        return int.TryParse(port, out var portNumber) && IsValidPortNumber(portNumber);
    }

    /// <summary>
    /// Checks whether the specified domain name can be resolved via DNS.
    /// </summary>
    /// <param name="domainName">The domain name to check.</param>
    /// <returns><c>true</c> if the domain name can be resolved; otherwise, <c>false</c>.</returns>
    /// <remarks>This method performs a network operation and may be slow or fail due to network issues.</remarks>
    public static bool IsDnsResolvable(string? domainName)
    {
        if (string.IsNullOrWhiteSpace(domainName))
            return false;

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