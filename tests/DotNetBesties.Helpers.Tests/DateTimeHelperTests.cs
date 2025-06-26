using System;
using System.Globalization;
using Xunit;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class DateTimeHelperTests
{
    [Fact]
    public void Format_RoundTrip_ShouldMatchParse()
    {
        var now = new DateTime(2024, 4, 5, 10, 20, 30, DateTimeKind.Utc);
        var formatted = DateTimeHelper.Format(now);
        var parsed = DateTimeHelper.ParseExactInvariant(formatted, "O", DateTimeStyles.RoundtripKind);
        Assert.Equal(now, parsed);
    }

    [Fact]
    public void TryParseExactInvariant_Invalid_ReturnsFalse()
    {
        var ok = DateTimeHelper.TryParseExactInvariant("invalid", "O", out var _);
        Assert.False(ok);
    }
}
