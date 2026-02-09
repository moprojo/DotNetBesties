using DotNetBesties.Helpers.Extensions;
using DotNetBesties.Helpers.Format;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBesties.Helpers.Tests.Format
{
    public class StringHelperTests
    {
        #region Null/Empty Checks Tests

        [Test]
        public async Task IsNullOrEmpty_WithNull_ReturnsTrue()
        {
            await Assert.That(StringHelper.IsNullOrEmpty(null)).IsTrue();
        }

        [Test]
        public async Task IsNullOrEmpty_WithEmpty_ReturnsTrue()
        {
            await Assert.That(StringHelper.IsNullOrEmpty("")).IsTrue();
        }

        [Test]
        public async Task IsNullOrEmpty_WithValue_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsNullOrEmpty("test")).IsFalse();
        }

        [Test]
        public async Task IsNullOrWhiteSpace_WithWhitespace_ReturnsTrue()
        {
            await Assert.That(StringHelper.IsNullOrWhiteSpace("   ")).IsTrue();
        }

        [Test]
        public async Task IsNullOrWhiteSpace_WithValue_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsNullOrWhiteSpace("test")).IsFalse();
        }

        [Test]
        public async Task HasValue_WithValue_ReturnsTrue()
        {
            await Assert.That(StringHelper.HasValue("test")).IsTrue();
        }

        [Test]
        public async Task HasValue_WithEmpty_ReturnsFalse()
        {
            await Assert.That(StringHelper.HasValue("")).IsFalse();
        }

        [Test]
        public async Task DefaultIfEmpty_WithEmpty_ReturnsDefault()
        {
            var result = StringHelper.DefaultIfEmpty("", "default");
            await Assert.That(result).IsEqualTo("default");
        }

        [Test]
        public async Task DefaultIfEmpty_WithValue_ReturnsValue()
        {
            var result = StringHelper.DefaultIfEmpty("test", "default");
            await Assert.That(result).IsEqualTo("test");
        }

        #endregion

        #region String Comparison Tests - Extended

        [Test]
        public async Task EqualsIgnoreCase_WithSameStrings_ReturnsTrue()
        {
            await Assert.That(StringHelper.EqualsIgnoreCase("hello", "HELLO")).IsTrue();
        }

        [Test]
        public async Task EqualsIgnoreCase_WithDifferentStrings_ReturnsFalse()
        {
            await Assert.That(StringHelper.EqualsIgnoreCase("hello", "world")).IsFalse();
        }

        [Test]
        public async Task StartsWithIgnoreCase_WithMatch_ReturnsTrue()
        {
            await Assert.That(StringHelper.StartsWithIgnoreCase("HelloWorld", "hello")).IsTrue();
        }

        [Test]
        public async Task StartsWithIgnoreCase_WithNoMatch_ReturnsFalse()
        {
            await Assert.That(StringHelper.StartsWithIgnoreCase("HelloWorld", "world")).IsFalse();
        }

        [Test]
        public async Task StartsWithIgnoreCase_WithNull_ReturnsFalse()
        {
            await Assert.That(StringHelper.StartsWithIgnoreCase(null, "hello")).IsFalse();
        }

        [Test]
        public async Task EndsWithIgnoreCase_WithMatch_ReturnsTrue()
        {
            await Assert.That(StringHelper.EndsWithIgnoreCase("HelloWorld", "WORLD")).IsTrue();
        }

        [Test]
        public async Task EndsWithIgnoreCase_WithNoMatch_ReturnsFalse()
        {
            await Assert.That(StringHelper.EndsWithIgnoreCase("HelloWorld", "hello")).IsFalse();
        }

        [Test]
        public async Task EndsWithIgnoreCase_WithNull_ReturnsFalse()
        {
            await Assert.That(StringHelper.EndsWithIgnoreCase(null, "world")).IsFalse();
        }

        #endregion

        #region Color        
        [Test]
        public async Task RGBToHex_ShouldReturnExpectedHex()
        {
            var hex = StringHelper.RGBToHex(255, 0, 0);
            await Assert.That(hex).IsEqualTo("#FF0000");
        }

        [Test]
        public async Task RGBToHex_WithInvalidR_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await Task.Run(() => StringHelper.RGBToHex(256, 0, 0)));
        }

        [Test]
        public async Task RGBToHex_WithNegativeG_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await Task.Run(() => StringHelper.RGBToHex(0, -1, 0)));
        }

        [Test]
        public async Task RGBToHex_WithValidBoundary_ReturnsHex()
        {
            var hex = StringHelper.RGBToHex(0, 0, 0);
            await Assert.That(hex).IsEqualTo("#000000");
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

        [Test]
        public async Task FromDateOnly_NonNullable_ShouldFormatCorrectly()
        {
            var date = new DateOnly(2024, 5, 1);
            var formatted = StringHelper.FromDateOnly(date, "yyyy-MM-dd");
            await Assert.That(formatted).IsEqualTo("2024-05-01");
        }

        [Test]
        public async Task FromDateOnly_NullableNull_ReturnsNull()
        {
            DateOnly? date = null;
            var formatted = StringHelper.FromDateOnly(date);
            await Assert.That(formatted).IsNull();
        }
        #endregion

        #region DateTime
        [Test]
        public async Task FromDateTime_NonNullable_ShouldFormatCorrectly()
        {
            var dt = new DateTime(2024, 5, 1, 10, 30, 0, DateTimeKind.Utc);
            var formatted = StringHelper.FromDateTime(dt, "yyyy-MM-dd HH:mm:ss");
            await Assert.That(formatted).IsEqualTo("2024-05-01 10:30:00");
        }

        [Test]
        public async Task FromDateTime_NullableNull_ReturnsNull()
        {
            DateTime? dt = null;
            var formatted = StringHelper.FromDateTime(dt);
            await Assert.That(formatted).IsNull();
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

        [Test]
        public async Task FromDateTimeOffset_NullableNull_ReturnsNull()
        {
            DateTimeOffset? dto = null;
            var formatted = StringHelper.FromDateTimeOffset(dto);
            await Assert.That(formatted).IsNull();
        }
        #endregion

        #region TimeOnly
        [Test]
        public async Task FromTimeOnly_NonNullable_ShouldFormatCorrectly()
        {
            var time = new TimeOnly(14, 30, 45);
            var formatted = StringHelper.FromTimeOnly(time, "HH:mm:ss");
            await Assert.That(formatted).IsEqualTo("14:30:45");
        }

        [Test]
        public async Task FromTimeOnly_Nullable_ShouldFormatCorrectly()
        {
            TimeOnly? time = new TimeOnly(14, 30, 45);
            var formatted = StringHelper.FromTimeOnly(time, "HH:mm:ss");
            await Assert.That(formatted).IsEqualTo("14:30:45");
        }

        [Test]
        public async Task FromTimeOnly_NullableNull_ReturnsNull()
        {
            TimeOnly? time = null;
            var formatted = StringHelper.FromTimeOnly(time);
            await Assert.That(formatted).IsNull();
        }

        [Test]
        public async Task FromTimeOnly_CustomFormat_ShouldFormatCorrectly()
        {
            var time = new TimeOnly(14, 30, 0);
            var formatted = StringHelper.FromTimeOnly(time, "hh:mm tt");
            await Assert.That(formatted).IsEqualTo("02:30 PM");
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

        [Test]
        public async Task FromTimeSpan_NullableNull_ReturnsNull()
        {
            TimeSpan? ts = null;
            var formatted = StringHelper.FromTimeSpan(ts);
            await Assert.That(formatted).IsNull();
        }
        #endregion

        #region String Validation Tests

        [Test]
        public async Task IsNumeric_WithDigitsOnly_ReturnsTrue()
        {
            await Assert.That(StringHelper.IsNumeric("12345")).IsTrue();
        }

        [Test]
        public async Task IsNumeric_WithLetters_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsNumeric("123abc")).IsFalse();
        }

        [Test]
        public async Task IsNumeric_WithNull_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsNumeric(null)).IsFalse();
        }

        [Test]
        public async Task IsNumeric_WithEmpty_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsNumeric("")).IsFalse();
        }

        [Test]
        public async Task IsAlphanumeric_WithLettersAndDigits_ReturnsTrue()
        {
            await Assert.That(StringHelper.IsAlphanumeric("abc123")).IsTrue();
        }

        [Test]
        public async Task IsAlphanumeric_WithSpecialChars_ReturnsFalse()
        {
            await Assert.That(StringHelper.IsAlphanumeric("abc-123")).IsFalse();
        }

        #endregion

        #region String Manipulation Tests

        [Test]
        public async Task Truncate_LongerThanMax_TruncatesWithSuffix()
        {
            var result = StringHelper.Truncate("Hello World", 8, "...");
            await Assert.That(result).IsEqualTo("Hello...");
        }

        [Test]
        public async Task Truncate_ShorterThanMax_ReturnsOriginal()
        {
            var result = StringHelper.Truncate("Hello", 10);
            await Assert.That(result).IsEqualTo("Hello");
        }

        [Test]
        public async Task RemoveWhitespace_RemovesAllWhitespace()
        {
            var result = StringHelper.RemoveWhitespace("Hello World Test");
            await Assert.That(result).IsEqualTo("HelloWorldTest");
        }

        [Test]
        public async Task CollapseWhitespace_CollapsesMultipleSpaces()
        {
            var result = StringHelper.CollapseWhitespace("Hello    World   Test");
            await Assert.That(result).IsEqualTo("Hello World Test");
        }

        [Test]
        public async Task Reverse_ReversesString()
        {
            var result = StringHelper.Reverse("Hello");
            await Assert.That(result).IsEqualTo("olleH");
        }

        [Test]
        public async Task Repeat_RepeatsString()
        {
            var result = StringHelper.Repeat("ab", 3);
            await Assert.That(result).IsEqualTo("ababab");
        }

        #endregion

        #region String Comparison Tests

        [Test]
        public async Task Contains_WithStringComparison_FindsMatch()
        {
            await Assert.That(StringHelper.Contains("Hello World", "WORLD", StringComparison.OrdinalIgnoreCase)).IsTrue();
        }

        [Test]
        public async Task CountOccurrences_CountsCorrectly()
        {
            var count = StringHelper.CountOccurrences("banana", "na");
            await Assert.That(count).IsEqualTo(2);
        }

        #endregion

        #region String Replace Tests

        [Test]
        public async Task ReplaceFirst_ReplacesFirstOccurrence()
        {
            var result = StringHelper.ReplaceFirst("hello hello", "hello", "hi");
            await Assert.That(result).IsEqualTo("hi hello");
        }

        [Test]
        public async Task ReplaceLast_ReplacesLastOccurrence()
        {
            var result = StringHelper.ReplaceLast("hello hello", "hello", "hi");
            await Assert.That(result).IsEqualTo("hello hi");
        }

        #endregion

        #region Substring Helper Tests

        [Test]
        public async Task Left_ReturnsLeftCharacters()
        {
            var result = StringHelper.Left("Hello World", 5);
            await Assert.That(result).IsEqualTo("Hello");
        }

        [Test]
        public async Task Right_ReturnsRightCharacters()
        {
            var result = StringHelper.Right("Hello World", 5);
            await Assert.That(result).IsEqualTo("World");
        }

        [Test]
        public async Task Mid_ReturnsMiddleCharacters()
        {
            var result = StringHelper.Mid("Hello World", 6, 5);
            await Assert.That(result).IsEqualTo("World");
        }

        #endregion

        #region Case Conversion Tests

        [Test]
        public async Task ToTitleCase_ConvertsCorrectly()
        {
            var result = StringHelper.ToTitleCase("hello world");
            await Assert.That(result).IsEqualTo("Hello World");
        }

        [Test]
        public async Task ToPascalCase_ConvertsCorrectly()
        {
            var result = StringHelper.ToPascalCase("hello world");
            await Assert.That(result).IsEqualTo("HelloWorld");
        }

        [Test]
        public async Task ToCamelCase_ConvertsCorrectly()
        {
            var result = StringHelper.ToCamelCase("hello world");
            await Assert.That(result).IsEqualTo("helloWorld");
        }

        [Test]
        public async Task ToKebabCase_ConvertsCorrectly()
        {
            var result = StringHelper.ToKebabCase("HelloWorld");
            await Assert.That(result).IsEqualTo("hello-world");
        }

        [Test]
        public async Task ToSnakeCase_ConvertsCorrectly()
        {
            var result = StringHelper.ToSnakeCase("HelloWorld");
            await Assert.That(result).IsEqualTo("hello_world");
        }

        #endregion

        #region Encoding Tests

        [Test]
        public async Task ToBase64_EncodesCorrectly()
        {
            var result = StringHelper.ToBase64("Hello");
            await Assert.That(result).IsEqualTo("SGVsbG8=");
        }

        [Test]
        public async Task FromBase64_DecodesCorrectly()
        {
            var result = StringHelper.FromBase64("SGVsbG8=");
            await Assert.That(result).IsEqualTo("Hello");
        }

        [Test]
        public async Task ToBase64_FromBase64_RoundTrip()
        {
            var original = "Hello World 123!";
            var encoded = StringHelper.ToBase64(original);
            var decoded = StringHelper.FromBase64(encoded);
            await Assert.That(decoded).IsEqualTo(original);
        }

        #endregion

        #region Prefix/Suffix Tests

        [Test]
        public async Task EnsureEndsWith_AddsWhenMissing()
        {
            var result = StringHelper.EnsureEndsWith("Hello", "!");
            await Assert.That(result).IsEqualTo("Hello!");
        }

        [Test]
        public async Task EnsureEndsWith_DoesNotDuplicateWhenPresent()
        {
            var result = StringHelper.EnsureEndsWith("Hello!", "!");
            await Assert.That(result).IsEqualTo("Hello!");
        }

        [Test]
        public async Task EnsureStartsWith_AddsWhenMissing()
        {
            var result = StringHelper.EnsureStartsWith("World", "Hello");
            await Assert.That(result).IsEqualTo("HelloWorld");
        }

        [Test]
        public async Task EnsureStartsWith_DoesNotDuplicateWhenPresent()
        {
            var result = StringHelper.EnsureStartsWith("HelloWorld", "Hello");
            await Assert.That(result).IsEqualTo("HelloWorld");
        }

        #endregion

        #region TryParseDateOnlyInvariant Tests

        [Test]
        public async Task TryParseDateOnlyInvariant_WithValidString_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02";

            // Act
            var success = input.TryParseDateOnlyInvariant("yyyy-MM-dd", out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(new DateOnly(2025, 1, 2));
        }

        [Test]
        public async Task TryParseDateOnlyInvariant_WithMultipleFormats_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02";
            var formats = new[] { "yyyy/MM/dd", "yyyy-MM-dd" };

            // Act
            var success = input.TryParseDateOnlyInvariant(formats, out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(new DateOnly(2025, 1, 2));
        }

        [Test]
        public async Task TryParseDateOnlyInvariant_WithInvalidString_ReturnsFalse()
        {
            // Arrange
            var input = "not a date";

            // Act
            var success = input.TryParseDateOnlyInvariant("yyyy-MM-dd", out var result);

            // Assert
            await Assert.That(success).IsFalse();
            await Assert.That(result).IsEqualTo(default(DateOnly));
        }

        [Test]
        public async Task TryParseDateOnlyInvariant_WithNull_ReturnsFalse()
        {
            // Arrange
            string? input = null;

            // Act
            var success = input.TryParseDateOnlyInvariant("yyyy-MM-dd", out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        [Test]
        public async Task TryParseDateOnlyInvariant_WithWrongFormat_ReturnsFalse()
        {
            // Arrange
            var input = "01/02/2025";

            // Act
            var success = input.TryParseDateOnlyInvariant("yyyy-MM-dd", out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        #endregion

        #region TryParseDateTimeInvariant Tests

        [Test]
        public async Task TryParseDateTimeInvariant_WithValidString_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02T03:04:05.0000000Z";

            // Act
            var success = input.TryParseDateTimeInvariant("O", out var result, DateTimeStyles.RoundtripKind);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result.Year).IsEqualTo(2025);
            await Assert.That(result.Month).IsEqualTo(1);
            await Assert.That(result.Day).IsEqualTo(2);
        }

        [Test]
        public async Task TryParseDateTimeInvariant_WithMultipleFormats_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02T03:04:05Z";
            var formats = new[] { "O", "yyyy-MM-ddTHH:mm:ssZ" };

            // Act
            var success = input.TryParseDateTimeInvariant(formats, out var result, DateTimeStyles.RoundtripKind);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result.Year).IsEqualTo(2025);
        }

        [Test]
        public async Task TryParseDateTimeInvariant_WithInvalidString_ReturnsFalse()
        {
            // Arrange
            var input = "not a datetime";

            // Act
            var success = input.TryParseDateTimeInvariant("O", out var result);

            // Assert
            await Assert.That(success).IsFalse();
            await Assert.That(result).IsEqualTo(default(DateTime));
        }

        [Test]
        public async Task TryParseDateTimeInvariant_WithNull_ReturnsFalse()
        {
            // Arrange
            string? input = null;

            // Act
            var success = input.TryParseDateTimeInvariant("O", out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        #endregion

        #region TryParseDateTimeOffsetInvariant Tests

        [Test]
        public async Task TryParseDateTimeOffsetInvariant_WithValidString_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02T03:04:05.0000000+00:00";

            // Act
            var success = input.TryParseDateTimeOffsetInvariant("O", out var result, DateTimeStyles.RoundtripKind);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result.Year).IsEqualTo(2025);
            await Assert.That(result.Month).IsEqualTo(1);
            await Assert.That(result.Day).IsEqualTo(2);
        }

        [Test]
        public async Task TryParseDateTimeOffsetInvariant_WithMultipleFormats_ReturnsTrue()
        {
            // Arrange
            var input = "2025-01-02T03:04:05+00:00";
            var formats = new[] { "O", "yyyy-MM-ddTHH:mm:sszzz" };

            // Act
            var success = input.TryParseDateTimeOffsetInvariant(formats, out var result, DateTimeStyles.RoundtripKind);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result.Year).IsEqualTo(2025);
        }

        [Test]
        public async Task TryParseDateTimeOffsetInvariant_WithInvalidString_ReturnsFalse()
        {
            // Arrange
            var input = "not a date";

            // Act
            var success = input.TryParseDateTimeOffsetInvariant("O", out var result);

            // Assert
            await Assert.That(success).IsFalse();
            await Assert.That(result).IsEqualTo(default(DateTimeOffset));
        }

        [Test]
        public async Task TryParseDateTimeOffsetInvariant_WithNull_ReturnsFalse()
        {
            // Arrange
            string? input = null;

            // Act
            var success = input.TryParseDateTimeOffsetInvariant("O", out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        #endregion

        #region TryParseTimeSpanInvariant Tests

        [Test]
        public async Task TryParseTimeSpanInvariant_WithValidString_ReturnsTrue()
        {
            // Arrange
            var input = "01:02:03";

            // Act
            var success = input.TryParseTimeSpanInvariant("c", out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(new TimeSpan(1, 2, 3));
        }

        [Test]
        public async Task TryParseTimeSpanInvariant_WithMultipleFormats_ReturnsTrue()
        {
            // Arrange
            var input = "01:02:03";
            var formats = new[] { "c", "g" };

            // Act
            var success = input.TryParseTimeSpanInvariant(formats, out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(new TimeSpan(1, 2, 3));
        }

        [Test]
        public async Task TryParseTimeSpanInvariant_WithInvalidString_ReturnsFalse()
        {
            // Arrange
            var input = "not a timespan";

            // Act
            var success = input.TryParseTimeSpanInvariant("c", out var result);

            // Assert
            await Assert.That(success).IsFalse();
            await Assert.That(result).IsEqualTo(default(TimeSpan));
        }

        [Test]
        public async Task TryParseTimeSpanInvariant_WithNull_ReturnsFalse()
        {
            // Arrange
            string? input = null;

            // Act
            var success = input.TryParseTimeSpanInvariant("c", out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        #endregion

        #region TryParseLongInvariant Tests

        [Test]
        public async Task TryParseLongInvariant_WithValidString_ReturnsTrue()
        {
            // Arrange
            var input = "42";

            // Act
            var success = input.TryParseLongInvariant(out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(42L);
        }

        [Test]
        public async Task TryParseLongInvariant_WithNegativeNumber_ReturnsTrue()
        {
            // Arrange
            var input = "-123";

            // Act
            var success = input.TryParseLongInvariant(out var result);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(-123L);
        }

        [Test]
        public async Task TryParseLongInvariant_WithInvalidString_ReturnsFalse()
        {
            // Arrange
            var input = "not a number";

            // Act
            var success = input.TryParseLongInvariant(out var result);

            // Assert
            await Assert.That(success).IsFalse();
            await Assert.That(result).IsEqualTo(0L);
        }

        [Test]
        public async Task TryParseLongInvariant_WithNull_ReturnsFalse()
        {
            // Arrange
            string? input = null;

            // Act
            var success = input.TryParseLongInvariant(out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        [Test]
        public async Task TryParseLongInvariant_WithDecimal_ReturnsFalse()
        {
            // Arrange
            var input = "123.45";

            // Act
            var success = input.TryParseLongInvariant(out var result);

            // Assert
            await Assert.That(success).IsFalse();
        }

        #endregion

        #region Fluent API Examples

        [Test]
        public async Task FluentUsage_ChainedOperations()
        {
            // Arrange
            var dateString = "2025-01-15";

            // Act
            if(dateString.TryParseDateOnlyInvariant("yyyy-MM-dd", out var date))
            {
                var dayOfWeek = date.DayOfWeek;

                // Assert
                await Assert.That(dayOfWeek).IsEqualTo(DayOfWeek.Wednesday);
            }
            else
            {
                throw new Exception("Should have parsed successfully");
            }
        }

        [Test]
        public async Task FluentUsage_WithExtensionChain()
        {
            // Arrange
            var timeString = "  12:34:56  ";

            // Act
            var trimmed = timeString.Trim();
            var success = trimmed.TryParseTimeSpanInvariant("c", out var timeSpan);

            // Assert
            await Assert.That(success).IsTrue();
            await Assert.That(timeSpan.Hours).IsEqualTo(12);
            await Assert.That(timeSpan.Minutes).IsEqualTo(34);
            await Assert.That(timeSpan.Seconds).IsEqualTo(56);
        }

        #endregion
    }
}
