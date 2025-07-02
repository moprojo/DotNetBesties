using System;
using System.Drawing;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers;

namespace DotNetBesties.Helpers.Tests
{
    public class ColorHelperTests
    {
        [Test]
        public async Task RgbToColor_ShouldReturnExpectedColor()
        {
            var color = ColorHelper.RgbToColor(255, 0, 0);
            await Assert.That((int)color.R).IsEqualTo(255);
            await Assert.That((int)color.G).IsEqualTo(0);
            await Assert.That((int)color.B).IsEqualTo(0);
        }

        [Test]
        public async Task ARGBToColor_ShouldReturnExpectedColor()
        {
            var color = ColorHelper.ARGBToColor(128, 255, 0, 0);
            await Assert.That((int)color.A).IsEqualTo(128);
            await Assert.That((int)color.R).IsEqualTo(255);
            await Assert.That((int)color.G).IsEqualTo(0);
            await Assert.That((int)color.B).IsEqualTo(0);
        }
        
        [Test]
        public async Task HexToColor_ShouldReturnExpectedColor()
        {
            var color = ColorHelper.HexToColor("#FF0000");
            await Assert.That((int)color.R).IsEqualTo(255);
            await Assert.That((int)color.G).IsEqualTo(0);
            await Assert.That((int)color.B).IsEqualTo(0);
        }

        [Test]
        public async Task HexToRGB_ShouldReturnExpectedRGB()
        {
            var (r, g, b) = ColorHelper.HexToRGB("#FF0000");
            await Assert.That((int)r).IsEqualTo(255);
            await Assert.That((int)g).IsEqualTo(0);
            await Assert.That((int)b).IsEqualTo(0);
        }

        [Test]
        public async Task ColorToARGB_ShouldReturnExpectedARGB()
        {
            var color = Color.FromArgb(128, 255, 0, 0);
            var (a, r, g, b) = ColorHelper.ColorToARGB(color);
            await Assert.That((int)a).IsEqualTo(128);
            await Assert.That((int)r).IsEqualTo(255);
            await Assert.That((int)g).IsEqualTo(0);
            await Assert.That((int)b).IsEqualTo(0);
        }
    }
}