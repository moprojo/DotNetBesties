using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests;

public class FloatHelperTests
{
    [Test]
    public async Task TotalHours_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromHours(2);
        var result = FloatHelper.TotalHours(ts);
        await Assert.That(result).IsEqualTo(2f);
    }
}
