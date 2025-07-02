using DotNetBesties.Helpers.Format;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBesties.Helpers.Tests.Format
{
    public class StringHelperTests
    {
        #region Color        
        [Test]
        public async Task RGBToHex_ShouldReturnExpectedHex()
        {
            var hex = StringHelper.RGBToHex(255, 0, 0);
            await Assert.That(hex).IsEqualTo("#FF0000");
        }

        [Test]
        public async Task ColorToHexString_ShouldReturnExpectedHex()
        {
            var color = Color.FromArgb(255, 0, 0);
            var hex = StringHelper.ColorToHexString(color);
            await Assert.That(hex).IsEqualTo("#FF0000");
        }

        [Test]
        public async Task ColorToRgbString_ShouldReturnExpectedRgbString()
        {
            var color = Color.FromArgb(255, 0, 0);
            var rgbString = StringHelper.ColorToRgbString(color);
            await Assert.That(rgbString).IsEqualTo("rgb(255, 0, 0)");
        }

        [Test]
        public async Task ColorToARgbString_ShouldReturnExpectedArgbString()
        {
            var color = Color.FromArgb(128, 255, 0, 0);
            var argbString = StringHelper.ColorToARgbString(color);
            await Assert.That(argbString).IsEqualTo("rgba(255, 0, 0, 128)");
        }
        #endregion
        
        #region DateOnly
        [Test]
        public async Task FromDateOnly_Nullable_ShouldFormatCorrectly()
        {
            DateOnly? date = new DateOnly(2024, 5, 1);
            var formatted = StringHelper.FromDateOnly(date, "yyyy-MM-dd");
            await Assert.That(formatted).IsEqualTo("2024-05-01");
        }
        #endregion

        #region DateTimeOffset
        [Test]
        public async Task FromDateTimeOffset_ShouldFormatCorrectly()
        {
            var dto = new DateTimeOffset(2024, 5, 1, 0, 0, 0, TimeSpan.Zero);
            var formatted = StringHelper.FromDateTimeOffset(dto, "yyyy-MM-dd");
            await Assert.That(formatted).IsEqualTo("2024-05-01");
        }
        #endregion
        
        #region TimeSpan
        [Test]
        public async Task FromTimeSpan_ShouldFormatCorrectly()
        {
            var ts = TimeSpan.FromHours(1.5);
            var formatted = StringHelper.FromTimeSpan(ts, "c");
            await Assert.That(formatted).IsEqualTo("01:30:00");
        }
        #endregion
    }
}
