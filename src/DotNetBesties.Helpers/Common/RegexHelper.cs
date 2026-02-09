using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace DotNetBesties.Helpers.Common;

/// <summary>
/// Helper methods for working with compiled regular expressions.
/// Provides caching and common regex patterns.
/// </summary>
public static class RegexHelper
{
    private static readonly ConcurrentDictionary<string, Regex> CompiledCache = new();
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(2);

    #region Cached Compiled Patterns

    /// <summary>
    /// Gets a compiled regex for email validation.
    /// </summary>
    public static Regex EmailPattern => GetOrAdd(
        "email",
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Gets a compiled regex for URL validation.
    /// </summary>
    public static Regex UrlPattern => GetOrAdd(
        "url",
        @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Gets a compiled regex for alphanumeric validation.
    /// </summary>
    public static Regex AlphanumericPattern => GetOrAdd(
        "alphanumeric",
        @"^[a-zA-Z0-9]+$",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Gets a compiled regex for numeric validation.
    /// </summary>
    public static Regex NumericPattern => GetOrAdd(
        "numeric",
        @"^\d+$",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Gets a compiled regex for hexadecimal validation.
    /// </summary>
    public static Regex HexPattern => GetOrAdd(
        "hex",
        @"^[0-9A-Fa-f]+$",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Gets a compiled regex for GUID validation.
    /// </summary>
    public static Regex GuidPattern => GetOrAdd(
        "guid",
        @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Gets a compiled regex for whitespace matching.
    /// </summary>
    public static Regex WhitespacePattern => GetOrAdd(
        "whitespace",
        @"\s+",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Gets a compiled regex for multiple whitespace collapsing.
    /// </summary>
    public static Regex MultipleWhitespacePattern => GetOrAdd(
        "multipleWhitespace",
        @"\s{2,}",
        RegexOptions.Compiled
    );

    #endregion

    #region Cache Management

    /// <summary>
    /// Gets or adds a compiled regex to the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="options">The regex options.</param>
    /// <param name="timeout">The match timeout.</param>
    /// <returns>The compiled regex.</returns>
    public static Regex GetOrAdd(string key, string pattern, RegexOptions options = RegexOptions.Compiled, TimeSpan? timeout = null)
    {
        timeout ??= DefaultTimeout;

        return CompiledCache.GetOrAdd(key, _ => new Regex(pattern, options, timeout.Value));
    }

    /// <summary>
    /// Tries to get a cached regex.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="regex">The cached regex if found.</param>
    /// <returns><c>true</c> if the regex was found; otherwise, <c>false</c>.</returns>
    public static bool TryGet(string key, out Regex? regex)
        => CompiledCache.TryGetValue(key, out regex);

    /// <summary>
    /// Removes a regex from the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns><c>true</c> if the regex was removed; otherwise, <c>false</c>.</returns>
    public static bool Remove(string key)
        => CompiledCache.TryRemove(key, out _);

    /// <summary>
    /// Clears all cached regexes.
    /// </summary>
    public static void ClearCache()
        => CompiledCache.Clear();

    /// <summary>
    /// Gets the number of cached regexes.
    /// </summary>
    public static int CacheCount => CompiledCache.Count;

    #endregion

    #region Common Operations

    /// <summary>
    /// Validates if a string matches a pattern.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="options">The regex options.</param>
    /// <returns><c>true</c> if the input matches the pattern; otherwise, <c>false</c>.</returns>
    public static bool IsMatch(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        try
        {
            return Regex.IsMatch(input, pattern, options, DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    /// Extracts all matches from a string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="options">The regex options.</param>
    /// <returns>The collection of matches.</returns>
    public static MatchCollection Matches(string input, string pattern, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(input))
            return Regex.Matches(string.Empty, pattern);

        try
        {
            return Regex.Matches(input, pattern, options, DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return Regex.Matches(string.Empty, pattern);
        }
    }

    /// <summary>
    /// Replaces all matches in a string with a replacement value.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="replacement">The replacement value.</param>
    /// <param name="options">The regex options.</param>
    /// <returns>The string with replacements.</returns>
    public static string Replace(string? input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(input))
            return input ?? string.Empty;

        try
        {
            return Regex.Replace(input, pattern, replacement, options, DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return input;
        }
    }

    /// <summary>
    /// Splits a string using a regex pattern.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="options">The regex options.</param>
    /// <returns>An array of split strings.</returns>
    public static string[] Split(string? input, string pattern, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(input))
            return Array.Empty<string>();

        try
        {
            return Regex.Split(input, pattern, options, DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return new[] { input };
        }
    }

    /// <summary>
    /// Escapes special regex characters in a string.
    /// </summary>
    /// <param name="input">The string to escape.</param>
    /// <returns>The escaped string.</returns>
    public static string Escape(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input ?? string.Empty;

        return Regex.Escape(input);
    }

    /// <summary>
    /// Unescapes a regex-escaped string.
    /// </summary>
    /// <param name="input">The escaped string.</param>
    /// <returns>The unescaped string.</returns>
    public static string Unescape(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input ?? string.Empty;

        return Regex.Unescape(input);
    }

    #endregion
}
