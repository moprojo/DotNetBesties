using System.Drawing;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

/// <summary>
/// Tests for <see cref="ColorExtensions"/>.
/// </summary>
public class ColorExtensionsTests
{
    #region Conversion

    [Test]
    public async Task ToARGB_WithColor_ReturnsCorrectTuple()
    {
        // Arrange
        var color = Color.FromArgb(128, 255, 100, 50);

        // Act
        var (a, r, g, b) = color.ToARGB();

        // Assert
        await Assert.That(a).IsEqualTo(128);
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(100);
        await Assert.That(b).IsEqualTo(50);
    }

    [Test]
    public async Task ToRGB_WithColor_ReturnsCorrectTuple()
    {
        // Arrange
        var color = Color.FromArgb(128, 255, 100, 50);

        // Act
        var (r, g, b) = color.ToRGB();

        // Assert
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(100);
        await Assert.That(b).IsEqualTo(50);
    }

    [Test]
    public async Task ToHex_WithColor_ReturnsCorrectHexString()
    {
        // Arrange
        var color = Color.FromArgb(255, 100, 50);

        // Act
        var result = color.ToHex();

        // Assert
        await Assert.That(result).IsEqualTo("#FF6432");
    }

    [Test]
    public async Task ToHex_WithBlackColor_ReturnsBlackHex()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.ToHex();

        // Assert
        await Assert.That(result).IsEqualTo("#000000");
    }

    [Test]
    public async Task ToHex_WithWhiteColor_ReturnsWhiteHex()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.ToHex();

        // Assert
        await Assert.That(result).IsEqualTo("#FFFFFF");
    }

    [Test]
    public async Task ToHexWithAlpha_WithColor_ReturnsCorrectHexString()
    {
        // Arrange
        var color = Color.FromArgb(128, 255, 100, 50);

        // Act
        var result = color.ToHexWithAlpha();

        // Assert
        await Assert.That(result).IsEqualTo("#80FF6432");
    }

    [Test]
    public async Task ToHexWithAlpha_WithFullyOpaqueColor_ReturnsFFPrefix()
    {
        // Arrange
        var color = Color.FromArgb(255, 255, 100, 50);

        // Act
        var result = color.ToHexWithAlpha();

        // Assert
        await Assert.That(result).IsEqualTo("#FFFF6432");
    }

    #endregion

    #region Manipulation

    [Test]
    public async Task Invert_WithColor_ReturnsInvertedColor()
    {
        // Arrange
        var color = Color.FromArgb(255, 100, 50);

        // Act
        var result = color.Invert();

        // Assert
        await Assert.That((int)result.A).IsEqualTo(255);
        await Assert.That((int)result.R).IsEqualTo(0);    // 255 - 255 = 0
        await Assert.That((int)result.G).IsEqualTo(155);  // 255 - 100 = 155
        await Assert.That((int)result.B).IsEqualTo(205);  // 255 - 50 = 205
    }

    [Test]
    public async Task Invert_WithBlack_ReturnsWhite()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.Invert();

        // Assert
        await Assert.That((int)result.R).IsEqualTo(255);
        await Assert.That((int)result.G).IsEqualTo(255);
        await Assert.That((int)result.B).IsEqualTo(255);
    }

    [Test]
    public async Task Invert_WithWhite_ReturnsBlack()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.Invert();

        // Assert
        await Assert.That((int)result.R).IsEqualTo(0);
        await Assert.That((int)result.G).IsEqualTo(0);
        await Assert.That((int)result.B).IsEqualTo(0);
    }

    [Test]
    public async Task WithAlpha_WithValidAlpha_ReturnsColorWithNewAlpha()
    {
        // Arrange
        var color = Color.FromArgb(255, 255, 100, 50);

        // Act
        var result = color.WithAlpha(128);

        // Assert
        await Assert.That((int)result.A).IsEqualTo(128);
        await Assert.That((int)result.R).IsEqualTo(255);
        await Assert.That((int)result.G).IsEqualTo(100);
        await Assert.That((int)result.B).IsEqualTo(50);
    }

    [Test]
    public async Task WithAlpha_WithZeroAlpha_ReturnsTransparentColor()
    {
        // Arrange
        var color = Color.Red;

        // Act
        var result = color.WithAlpha(0);

        // Assert
        await Assert.That((int)result.A).IsEqualTo(0);
    }

    [Test]
    public async Task WithAlpha_WithNegativeAlpha_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.WithAlpha(-1)));
    }

    [Test]
    public async Task WithAlpha_WithAlphaAbove255_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.WithAlpha(256)));
    }

    [Test]
    public async Task Lighten_WithDefaultFactor_ReturnsLighterColor()
    {
        // Arrange
        var color = Color.FromArgb(100, 100, 100);

        // Act
        var result = color.Lighten();

        // Assert
        await Assert.That((int)result.R).IsGreaterThan(100);
        await Assert.That((int)result.G).IsGreaterThan(100);
        await Assert.That((int)result.B).IsGreaterThan(100);
    }

    [Test]
    public async Task Lighten_WithCustomFactor_ReturnsLighterColor()
    {
        // Arrange
        var color = Color.FromArgb(100, 100, 100);

        // Act
        var result = color.Lighten(0.5);

        // Assert
        await Assert.That((int)result.R).IsEqualTo(177); // 100 + (255-100)*0.5 = 177.5 -> 177
        await Assert.That((int)result.G).IsEqualTo(177);
        await Assert.That((int)result.B).IsEqualTo(177);
    }

    [Test]
    public async Task Lighten_WithWhite_RemainsWhite()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.Lighten();

        // Assert
        await Assert.That((int)result.R).IsEqualTo(255);
        await Assert.That((int)result.G).IsEqualTo(255);
        await Assert.That((int)result.B).IsEqualTo(255);
    }

    [Test]
    public async Task Lighten_WithNegativeFactor_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.Lighten(-0.1)));
    }

    [Test]
    public async Task Lighten_WithFactorAboveOne_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.Lighten(1.1)));
    }

    [Test]
    public async Task Darken_WithDefaultFactor_ReturnsDarkerColor()
    {
        // Arrange
        var color = Color.FromArgb(100, 100, 100);

        // Act
        var result = color.Darken();

        // Assert
        await Assert.That((int)result.R).IsLessThan(100);
        await Assert.That((int)result.G).IsLessThan(100);
        await Assert.That((int)result.B).IsLessThan(100);
    }

    [Test]
    public async Task Darken_WithCustomFactor_ReturnsDarkerColor()
    {
        // Arrange
        var color = Color.FromArgb(100, 100, 100);

        // Act
        var result = color.Darken(0.5);

        // Assert
        await Assert.That((int)result.R).IsEqualTo(50); // 100 * 0.5 = 50
        await Assert.That((int)result.G).IsEqualTo(50);
        await Assert.That((int)result.B).IsEqualTo(50);
    }

    [Test]
    public async Task Darken_WithBlack_RemainsBlack()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.Darken();

        // Assert
        await Assert.That((int)result.R).IsEqualTo(0);
        await Assert.That((int)result.G).IsEqualTo(0);
        await Assert.That((int)result.B).IsEqualTo(0);
    }

    [Test]
    public async Task Darken_WithNegativeFactor_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.Darken(-0.1)));
    }

    [Test]
    public async Task Darken_WithFactorAboveOne_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color = Color.Red;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color.Darken(1.1)));
    }

    #endregion

    #region Analysis

    [Test]
    public async Task GetLuminance_WithBlack_ReturnsZero()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.GetLuminance();

        // Assert
        await Assert.That(Math.Abs(result - 0.0)).IsLessThan(0.001);
    }

    [Test]
    public async Task GetLuminance_WithWhite_ReturnsOne()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.GetLuminance();

        // Assert
        await Assert.That(Math.Abs(result - 1.0)).IsLessThan(0.001);
    }

    [Test]
    public async Task GetLuminance_WithGray_ReturnsMiddleValue()
    {
        // Arrange
        var color = Color.FromArgb(128, 128, 128);

        // Act
        var result = color.GetLuminance();

        // Assert
        await Assert.That(result).IsGreaterThan(0.0);
        await Assert.That(result).IsLessThan(1.0);
    }

    [Test]
    public async Task IsDark_WithBlack_ReturnsTrue()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.IsDark();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsDark_WithWhite_ReturnsFalse()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.IsDark();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsDark_WithDarkGray_ReturnsTrue()
    {
        // Arrange
        var color = Color.FromArgb(50, 50, 50);

        // Act
        var result = color.IsDark();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsLight_WithWhite_ReturnsTrue()
    {
        // Arrange
        var color = Color.White;

        // Act
        var result = color.IsLight();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsLight_WithBlack_ReturnsFalse()
    {
        // Arrange
        var color = Color.Black;

        // Act
        var result = color.IsLight();

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsLight_WithLightGray_ReturnsTrue()
    {
        // Arrange
        var color = Color.FromArgb(200, 200, 200);

        // Act
        var result = color.IsLight();

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task GetContrastingTextColor_WithWhiteBackground_ReturnsBlack()
    {
        // Arrange
        var backgroundColor = Color.White;

        // Act
        var result = backgroundColor.GetContrastingTextColor();

        // Assert
        await Assert.That(result).IsEqualTo(Color.Black);
    }

    [Test]
    public async Task GetContrastingTextColor_WithBlackBackground_ReturnsWhite()
    {
        // Arrange
        var backgroundColor = Color.Black;

        // Act
        var result = backgroundColor.GetContrastingTextColor();

        // Assert
        await Assert.That(result).IsEqualTo(Color.White);
    }

    [Test]
    public async Task GetContrastingTextColor_WithLightBackground_ReturnsBlack()
    {
        // Arrange
        var backgroundColor = Color.FromArgb(200, 200, 200);

        // Act
        var result = backgroundColor.GetContrastingTextColor();

        // Assert
        await Assert.That(result).IsEqualTo(Color.Black);
    }

    [Test]
    public async Task GetContrastingTextColor_WithDarkBackground_ReturnsWhite()
    {
        // Arrange
        var backgroundColor = Color.FromArgb(50, 50, 50);

        // Act
        var result = backgroundColor.GetContrastingTextColor();

        // Assert
        await Assert.That(result).IsEqualTo(Color.White);
    }

    #endregion

    #region Blending

    [Test]
    public async Task Blend_WithDefaultRatio_ReturnsMiddleColor()
    {
        // Arrange
        var color1 = Color.FromArgb(255, 0, 0, 0);    // Black
        var color2 = Color.FromArgb(255, 255, 255, 255); // White

        // Act
        var result = color1.Blend(color2);

        // Assert
        await Assert.That((int)result.R).IsEqualTo(127); // (0 + 255) / 2
        await Assert.That((int)result.G).IsEqualTo(127);
        await Assert.That((int)result.B).IsEqualTo(127);
    }

    [Test]
    public async Task Blend_WithRatioZero_ReturnsFirstColor()
    {
        // Arrange
        var color1 = Color.Red;
        var color2 = Color.Blue;

        // Act
        var result = color1.Blend(color2, 0.0);

        // Assert
        await Assert.That(result.R).IsEqualTo(color1.R);
        await Assert.That(result.G).IsEqualTo(color1.G);
        await Assert.That(result.B).IsEqualTo(color1.B);
    }

    [Test]
    public async Task Blend_WithRatioOne_ReturnsSecondColor()
    {
        // Arrange
        var color1 = Color.Red;
        var color2 = Color.Blue;

        // Act
        var result = color1.Blend(color2, 1.0);

        // Assert
        await Assert.That(result.R).IsEqualTo(color2.R);
        await Assert.That(result.G).IsEqualTo(color2.G);
        await Assert.That(result.B).IsEqualTo(color2.B);
    }

    [Test]
    public async Task Blend_WithCustomRatio_ReturnsBlendedColor()
    {
        // Arrange
        var color1 = Color.FromArgb(255, 100, 0, 0);
        var color2 = Color.FromArgb(255, 0, 200, 0);

        // Act
        var result = color1.Blend(color2, 0.25);

        // Assert
        await Assert.That((int)result.R).IsEqualTo(75);  // 100 * 0.75 + 0 * 0.25 = 75
        await Assert.That((int)result.G).IsEqualTo(50);  // 0 * 0.75 + 200 * 0.25 = 50
        await Assert.That((int)result.B).IsEqualTo(0);
    }

    [Test]
    public async Task Blend_WithAlphaValues_BlendsAlpha()
    {
        // Arrange
        var color1 = Color.FromArgb(100, 255, 0, 0);
        var color2 = Color.FromArgb(200, 0, 255, 0);

        // Act
        var result = color1.Blend(color2, 0.5);

        // Assert
        await Assert.That((int)result.A).IsEqualTo(150); // (100 + 200) / 2
    }

    [Test]
    public async Task Blend_WithNegativeRatio_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color1 = Color.Red;
        var color2 = Color.Blue;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color1.Blend(color2, -0.1)));
    }

    [Test]
    public async Task Blend_WithRatioAboveOne_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var color1 = Color.Red;
        var color2 = Color.Blue;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => color1.Blend(color2, 1.1)));
    }

    #endregion
}
