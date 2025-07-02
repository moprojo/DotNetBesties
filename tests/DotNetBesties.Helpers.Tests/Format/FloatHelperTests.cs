using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class FloatHelperTests
{
    [Test]
    public async Task TotalHours_ShouldReturnExpected()
    {
        var ts = TimeSpan.FromHours(2);
        var result = FloatHelper.TotalHours(ts);
        await Assert.That(result).IsEqualTo(2f);
    }

    [Test]
    public async Task ToOADate_FromDateTimeOffset_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 3, 1, 0, 0, 0, TimeSpan.Zero);
        var oa = FloatHelper.ToOADate(dto);
        var back = new DateTimeOffset(DateTime.SpecifyKind(DateTime.FromOADate(oa), DateTimeKind.Utc));
        await Assert.That(back.UtcDateTime).IsEqualTo(dto.UtcDateTime);
    }
}
