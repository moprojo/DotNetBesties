using System;
using System.Drawing;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class ColorHelperTests
{
    #region RgbToColor Tests

    [Test]
    public async Task RgbToColor_WithValidValues_ReturnsExpectedColor()
    {
        var color = ColorHelper.RgbToColor(255, 0, 0);
        await Assert.That((int)color.R).IsEqualTo(255);
        await Assert.That((int)color.G).IsEqualTo(0);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task RgbToColor_WithBoundaryValues_ReturnsExpectedColor()
    {
        var colorMin = ColorHelper.RgbToColor(0, 0, 0);
        await Assert.That((int)colorMin.R).IsEqualTo(0);

        var colorMax = ColorHelper.RgbToColor(255, 255, 255);
        await Assert.That((int)colorMax.R).IsEqualTo(255);
    }

    [Test]
    public async Task RgbToColor_WithInvalidRedValue_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.RgbToColor(256, 0, 0)));
    }

    [Test]
    public async Task RgbToColor_WithNegativeValue_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.RgbToColor(-1, 0, 0)));
    }

    #endregion

    #region ARGBToColor Tests

    [Test]
    public async Task ARGBToColor_WithValidValues_ReturnsExpectedColor()
    {
        var color = ColorHelper.ARGBToColor(128, 255, 0, 0);
        await Assert.That((int)color.A).IsEqualTo(128);
        await Assert.That((int)color.R).IsEqualTo(255);
        await Assert.That((int)color.G).IsEqualTo(0);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task ARGBToColor_WithInvalidAlphaValue_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.ARGBToColor(300, 255, 0, 0)));
    }

    #endregion

    #region HexToColor Tests

    [Test]
    public async Task HexToColor_WithHashPrefix_ReturnsExpectedColor()
    {
        var color = ColorHelper.HexToColor("#FF0000");
        await Assert.That((int)color.R).IsEqualTo(255);
        await Assert.That((int)color.G).IsEqualTo(0);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task HexToColor_WithoutHashPrefix_ReturnsExpectedColor()
    {
        var color = ColorHelper.HexToColor("00FF00");
        await Assert.That((int)color.R).IsEqualTo(0);
        await Assert.That((int)color.G).IsEqualTo(255);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task HexToColor_WithShortFormat_ReturnsExpectedColor()
    {
        var color = ColorHelper.HexToColor("#F00");
        await Assert.That((int)color.R).IsEqualTo(255);
        await Assert.That((int)color.G).IsEqualTo(0);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task HexToColor_WithShortFormatNoHash_ReturnsExpectedColor()
    {
        var color = ColorHelper.HexToColor("0F0");
        await Assert.That((int)color.R).IsEqualTo(0);
        await Assert.That((int)color.G).IsEqualTo(255);
        await Assert.That((int)color.B).IsEqualTo(0);
    }

    [Test]
    public async Task HexToColor_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => ColorHelper.HexToColor(null!)));
    }

    [Test]
    public async Task HexToColor_WithInvalidFormat_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await Task.Run(() => ColorHelper.HexToColor("invalid")));
    }

    [Test]
    public async Task HexToColor_WithInvalidHexCharacters_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await Task.Run(() => ColorHelper.HexToColor("#GGGGGG")));
    }

    #endregion

    #region HexToRGB Tests

    [Test]
    public async Task HexToRGB_WithValidHex_ReturnsExpectedRGB()
    {
        var (r, g, b) = ColorHelper.HexToRGB("#FF0000");
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(0);
        await Assert.That(b).IsEqualTo(0);
    }

    [Test]
    public async Task HexToRGB_WithLowercaseHex_ReturnsExpectedRGB()
    {
        var (r, g, b) = ColorHelper.HexToRGB("#ff00ff");
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(0);
        await Assert.That(b).IsEqualTo(255);
    }

    #endregion

    #region ColorToARGB Tests

    [Test]
    public async Task ColorToARGB_ReturnsExpectedARGB()
    {
        var color = Color.FromArgb(128, 255, 0, 0);
        var (a, r, g, b) = ColorHelper.ColorToARGB(color);
        await Assert.That(a).IsEqualTo(128);
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(0);
        await Assert.That(b).IsEqualTo(0);
    }

    #endregion

    #region ColorToRGB Tests

    [Test]
    public async Task ColorToRGB_ReturnsExpectedRGB()
    {
        var color = Color.FromArgb(255, 128, 64);
        var (r, g, b) = ColorHelper.ColorToRGB(color);
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(128);
        await Assert.That(b).IsEqualTo(64);
    }

    #endregion

    #region Invert Tests

    [Test]
    public async Task Invert_WithBlack_ReturnsWhite()
    {
        var black = Color.FromArgb(0, 0, 0);
        var inverted = ColorHelper.Invert(black);
        await Assert.That((int)inverted.R).IsEqualTo(255);
        await Assert.That((int)inverted.G).IsEqualTo(255);
        await Assert.That((int)inverted.B).IsEqualTo(255);
    }

    [Test]
    public async Task Invert_WithWhite_ReturnsBlack()
    {
        var white = Color.FromArgb(255, 255, 255);
        var inverted = ColorHelper.Invert(white);
        await Assert.That((int)inverted.R).IsEqualTo(0);
        await Assert.That((int)inverted.G).IsEqualTo(0);
        await Assert.That((int)inverted.B).IsEqualTo(0);
    }

    [Test]
    public async Task Invert_PreservesAlpha()
    {
        var color = Color.FromArgb(128, 100, 150, 200);
        var inverted = ColorHelper.Invert(color);
        await Assert.That((int)inverted.A).IsEqualTo(128);
    }

    #endregion

    #region GetLuminance Tests

    [Test]
    public async Task GetLuminance_WithBlack_ReturnsZero()
    {
        var black = Color.FromArgb(0, 0, 0);
        var luminance = ColorHelper.GetLuminance(black);
        await Assert.That(luminance).IsEqualTo(0);
    }

    [Test]
    public async Task GetLuminance_WithWhite_ReturnsOne()
    {
        var white = Color.FromArgb(255, 255, 255);
        var luminance = ColorHelper.GetLuminance(white);
        await Assert.That(luminance).IsEqualTo(1);
    }

    #endregion

    #region IsDark/IsLight Tests

    [Test]
    public async Task IsDark_WithBlack_ReturnsTrue()
    {
        var black = Color.FromArgb(0, 0, 0);
        await Assert.That(ColorHelper.IsDark(black)).IsTrue();
    }

    [Test]
    public async Task IsDark_WithWhite_ReturnsFalse()
    {
        var white = Color.FromArgb(255, 255, 255);
        await Assert.That(ColorHelper.IsDark(white)).IsFalse();
    }

    [Test]
    public async Task IsLight_WithWhite_ReturnsTrue()
    {
        var white = Color.FromArgb(255, 255, 255);
        await Assert.That(ColorHelper.IsLight(white)).IsTrue();
    }

    [Test]
    public async Task IsLight_WithBlack_ReturnsFalse()
    {
        var black = Color.FromArgb(0, 0, 0);
        await Assert.That(ColorHelper.IsLight(black)).IsFalse();
    }

    #endregion

    #region WithAlpha Tests

    [Test]
    public async Task WithAlpha_WithValidAlpha_ReturnsColorWithNewAlpha()
    {
        var color = Color.FromArgb(255, 255, 100, 50);
        var result = ColorHelper.WithAlpha(color, 128);

        await Assert.That((int)result.A).IsEqualTo(128);
        await Assert.That((int)result.R).IsEqualTo(255);
        await Assert.That((int)result.G).IsEqualTo(100);
        await Assert.That((int)result.B).IsEqualTo(50);
    }

    [Test]
    public async Task WithAlpha_WithZeroAlpha_ReturnsTransparentColor()
    {
        var color = Color.Red;
        var result = ColorHelper.WithAlpha(color, 0);

        await Assert.That((int)result.A).IsEqualTo(0);
    }

    [Test]
    public async Task WithAlpha_WithNegativeAlpha_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.WithAlpha(color, -1)));
    }

    [Test]
    public async Task WithAlpha_WithAlphaAbove255_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.WithAlpha(color, 256)));
    }

    #endregion

    #region Lighten Tests

    [Test]
    public async Task Lighten_WithDefaultFactor_ReturnsLighterColor()
    {
        var color = Color.FromArgb(100, 100, 100);
        var result = ColorHelper.Lighten(color);

        await Assert.That((int)result.R).IsGreaterThan(100);
        await Assert.That((int)result.G).IsGreaterThan(100);
        await Assert.That((int)result.B).IsGreaterThan(100);
    }

    [Test]
    public async Task Lighten_WithCustomFactor_ReturnsLighterColor()
    {
        var color = Color.FromArgb(100, 100, 100);
        var result = ColorHelper.Lighten(color, 0.5);

        await Assert.That((int)result.R).IsEqualTo(177); // 100 + (255-100)*0.5 = 177.5 -> 177
        await Assert.That((int)result.G).IsEqualTo(177);
        await Assert.That((int)result.B).IsEqualTo(177);
    }

    [Test]
    public async Task Lighten_WithWhite_RemainsWhite()
    {
        var color = Color.White;
        var result = ColorHelper.Lighten(color);

        await Assert.That((int)result.R).IsEqualTo(255);
        await Assert.That((int)result.G).IsEqualTo(255);
        await Assert.That((int)result.B).IsEqualTo(255);
    }

    [Test]
    public async Task Lighten_WithNegativeFactor_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Lighten(color, -0.1)));
    }

    [Test]
    public async Task Lighten_WithFactorAboveOne_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Lighten(color, 1.1)));
    }

    #endregion

    #region Darken Tests

    [Test]
    public async Task Darken_WithDefaultFactor_ReturnsDarkerColor()
    {
        var color = Color.FromArgb(100, 100, 100);
        var result = ColorHelper.Darken(color);

        await Assert.That((int)result.R).IsLessThan(100);
        await Assert.That((int)result.G).IsLessThan(100);
        await Assert.That((int)result.B).IsLessThan(100);
    }

    [Test]
    public async Task Darken_WithCustomFactor_ReturnsDarkerColor()
    {
        var color = Color.FromArgb(100, 100, 100);
        var result = ColorHelper.Darken(color, 0.5);

        await Assert.That((int)result.R).IsEqualTo(50); // 100 * 0.5 = 50
        await Assert.That((int)result.G).IsEqualTo(50);
        await Assert.That((int)result.B).IsEqualTo(50);
    }

    [Test]
    public async Task Darken_WithBlack_RemainsBlack()
    {
        var color = Color.Black;
        var result = ColorHelper.Darken(color);

        await Assert.That((int)result.R).IsEqualTo(0);
        await Assert.That((int)result.G).IsEqualTo(0);
        await Assert.That((int)result.B).IsEqualTo(0);
    }

    [Test]
    public async Task Darken_WithNegativeFactor_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Darken(color, -0.1)));
    }

    [Test]
    public async Task Darken_WithFactorAboveOne_ThrowsArgumentOutOfRangeException()
    {
        var color = Color.Red;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Darken(color, 1.1)));
    }

    #endregion

    #region Blend Tests

    [Test]
    public async Task Blend_WithDefaultRatio_ReturnsMiddleColor()
    {
        var color1 = Color.FromArgb(255, 0, 0, 0);    // Black
        var color2 = Color.FromArgb(255, 255, 255, 255); // White
        var result = ColorHelper.Blend(color1, color2);

        await Assert.That((int)result.R).IsEqualTo(127); // (0 + 255) / 2
        await Assert.That((int)result.G).IsEqualTo(127);
        await Assert.That((int)result.B).IsEqualTo(127);
    }

    [Test]
    public async Task Blend_WithRatioZero_ReturnsFirstColor()
    {
        var color1 = Color.Red;
        var color2 = Color.Blue;
        var result = ColorHelper.Blend(color1, color2, 0.0);

        await Assert.That(result.R).IsEqualTo(color1.R);
        await Assert.That(result.G).IsEqualTo(color1.G);
        await Assert.That(result.B).IsEqualTo(color1.B);
    }

    [Test]
    public async Task Blend_WithRatioOne_ReturnsSecondColor()
    {
        var color1 = Color.Red;
        var color2 = Color.Blue;
        var result = ColorHelper.Blend(color1, color2, 1.0);

        await Assert.That(result.R).IsEqualTo(color2.R);
        await Assert.That(result.G).IsEqualTo(color2.G);
        await Assert.That(result.B).IsEqualTo(color2.B);
    }

    [Test]
    public async Task Blend_WithCustomRatio_ReturnsBlendedColor()
    {
        var color1 = Color.FromArgb(255, 100, 0, 0);
        var color2 = Color.FromArgb(255, 0, 200, 0);
        var result = ColorHelper.Blend(color1, color2, 0.25);

        await Assert.That((int)result.R).IsEqualTo(75);  // 100 * 0.75 + 0 * 0.25 = 75
        await Assert.That((int)result.G).IsEqualTo(50);  // 0 * 0.75 + 200 * 0.25 = 50
        await Assert.That((int)result.B).IsEqualTo(0);
    }

    [Test]
    public async Task Blend_WithAlphaValues_BlendsAlpha()
    {
        var color1 = Color.FromArgb(100, 255, 0, 0);
        var color2 = Color.FromArgb(200, 0, 255, 0);
        var result = ColorHelper.Blend(color1, color2, 0.5);

        await Assert.That((int)result.A).IsEqualTo(150); // (100 + 200) / 2
    }

    [Test]
    public async Task Blend_WithNegativeRatio_ThrowsArgumentOutOfRangeException()
    {
        var color1 = Color.Red;
        var color2 = Color.Blue;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Blend(color1, color2, -0.1)));
    }

    [Test]
    public async Task Blend_WithRatioAboveOne_ThrowsArgumentOutOfRangeException()
    {
        var color1 = Color.Red;
        var color2 = Color.Blue;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => ColorHelper.Blend(color1, color2, 1.1)));
    }

    #endregion

    #region GetContrastingTextColor Tests

    [Test]
    public async Task GetContrastingTextColor_WithWhiteBackground_ReturnsBlack()
    {
        var white = Color.White;
        var result = ColorHelper.GetContrastingTextColor(white);
        await Assert.That(result).IsEqualTo(Color.Black);
    }

    [Test]
    public async Task GetContrastingTextColor_WithBlackBackground_ReturnsWhite()
    {
        var black = Color.Black;
        var result = ColorHelper.GetContrastingTextColor(black);
        await Assert.That(result).IsEqualTo(Color.White);
    }

    [Test]
    public async Task GetContrastingTextColor_WithLightGray_ReturnsBlack()
    {
        var lightGray = Color.LightGray;
        var result = ColorHelper.GetContrastingTextColor(lightGray);
        await Assert.That(result).IsEqualTo(Color.Black);
    }

    [Test]
    public async Task GetContrastingTextColor_WithDarkGray_ReturnsWhite()
    {
        var darkGray = Color.DarkGray;
        var result = ColorHelper.GetContrastingTextColor(darkGray);
        await Assert.That(result).IsEqualTo(Color.White);
    }

    #endregion

    #region ToHexWithAlpha Tests

    [Test]
    public async Task ToHexWithAlpha_WithFullyOpaque_ReturnsHexWithFF()
    {
        var color = Color.FromArgb(255, 255, 0, 0);
        var result = ColorHelper.ToHexWithAlpha(color);
        await Assert.That(result).IsEqualTo("#FFFF0000");
    }

    [Test]
    public async Task ToHexWithAlpha_WithSemiTransparent_ReturnsHexWithAlpha()
    {
        var color = Color.FromArgb(128, 255, 0, 0);
        var result = ColorHelper.ToHexWithAlpha(color);
        await Assert.That(result).IsEqualTo("#80FF0000");
    }

    [Test]
    public async Task ToHexWithAlpha_WithTransparent_ReturnsHexWith00()
    {
        var color = Color.FromArgb(0, 255, 0, 0);
        var result = ColorHelper.ToHexWithAlpha(color);
        await Assert.That(result).IsEqualTo("#00FF0000");
    }

    #endregion
}