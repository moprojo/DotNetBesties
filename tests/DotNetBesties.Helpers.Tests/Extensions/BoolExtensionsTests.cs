using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

public class BoolExtensionsTests
{
    #region ToYesNo Tests

    [Test]
    public async Task ToYesNo_WithTrue_ReturnsYes()
    {
        var result = true.ToYesNo();
        await Assert.That(result).IsEqualTo("Yes");
    }

    [Test]
    public async Task ToYesNo_WithFalse_ReturnsNo()
    {
        var result = false.ToYesNo();
        await Assert.That(result).IsEqualTo("No");
    }

    #endregion

    #region ToInt Tests

    [Test]
    public async Task ToInt_WithTrue_ReturnsOne()
    {
        var result = true.ToInt();
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ToInt_WithFalse_ReturnsZero()
    {
        var result = false.ToInt();
        await Assert.That(result).IsEqualTo(0);
    }

    #endregion

    #region TryParse Tests

    [Test]
    public async Task TryParse_WithTrue_ParsesCorrectly()
    {
        var success = "true".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TryParse_WithFalse_ParsesCorrectly()
    {
        var success = "false".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task TryParse_WithOne_ParsesAsTrue()
    {
        var success = "1".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TryParse_WithZero_ParsesAsFalse()
    {
        var success = "0".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task TryParse_WithYes_ParsesAsTrue()
    {
        var success = "yes".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task TryParse_WithNo_ParsesAsFalse()
    {
        var success = "no".TryParse(out var result);
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task TryParse_CaseInsensitive_Works()
    {
        await Assert.That("TRUE".TryParse(out var result1) && result1).IsTrue();
        await Assert.That("FALSE".TryParse(out var result2) && !result2).IsTrue();
        await Assert.That("YES".TryParse(out var result3) && result3).IsTrue();
        await Assert.That("NO".TryParse(out var result4) && !result4).IsTrue();
    }

    [Test]
    public async Task TryParse_WithInvalidString_ReturnsFalse()
    {
        var success = "invalid".TryParse(out var result);
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task TryParse_WithNull_ReturnsFalse()
    {
        var success = ((string?)null).TryParse(out var result);
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task TryParse_WithWhitespace_ReturnsFalse()
    {
        var success = "   ".TryParse(out var result);
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region ParseBool Tests

    [Test]
    public async Task ParseBool_WithTrue_ReturnsTrue()
    {
        var result = "true".ParseBool();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task ParseBool_WithFalse_ReturnsFalse()
    {
        var result = "false".ParseBool();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ParseBool_WithOne_ReturnsTrue()
    {
        var result = "1".ParseBool();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task ParseBool_WithYes_ReturnsTrue()
    {
        var result = "yes".ParseBool();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task ParseBool_WithInvalidString_ThrowsFormatException()
    {
        await Assert.ThrowsAsync<FormatException>(async () => await Task.Run(() => "invalid".ParseBool()));
    }

    [Test]
    public async Task ParseBool_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Task.Run(() => ((string)null!).ParseBool()));
    }

    #endregion

    #region ParseBoolOrDefault Tests

    [Test]
    public async Task ParseBoolOrDefault_WithValidValue_ReturnsValue()
    {
        var result = "true".ParseBoolOrDefault();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task ParseBoolOrDefault_WithInvalidValue_ReturnsDefault()
    {
        var result = "invalid".ParseBoolOrDefault(true);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task ParseBoolOrDefault_WithNull_ReturnsDefault()
    {
        var result = ((string?)null).ParseBoolOrDefault(true);
        await Assert.That(result).IsTrue();
    }

    #endregion
}
