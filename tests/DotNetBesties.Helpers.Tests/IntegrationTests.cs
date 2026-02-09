using System;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using System.Globalization;
using DotNetBesties.Helpers.Format;
using DotNetBesties.Helpers.Serialization;
using DotNetBesties.Helpers.Validation;
using DotNetBesties.Helpers.Common;
using DotNetBesties.Helpers.Cryptology;
using DotNetBesties.Helpers.Extensions;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;

namespace DotNetBesties.Helpers.Tests;

public class IntegrationTests
{
    #region Helper Methods Integration Tests

    [Test]
    public async Task DateTime_ToDateTimeOffset_ToUnix_RoundTrip()
    {
        var now = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);
        var dto = DateTimeOffsetHelper.FromDateTime(now);
        var unix = LongHelper.ToUnixTimeSeconds(dto);
        var backDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unix);
        await Assert.That(backDto).IsEqualTo(dto);
    }

    [Test]
    public async Task DateOnly_ToDateTimeOffset_WithZone_RoundTrip()
    {
        var date = new DateOnly(2024, 12, 24);
        var time = new TimeOnly(10, 0);
        var zone = TimeZoneInfo.Utc;
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, zone);
        var backDate = DateOnly.FromDateTime(dto.UtcDateTime);
        await Assert.That(backDate).IsEqualTo(date);
    }

    [Test]
    public async Task DateTimeOffset_FormatParse_RoundTrip()
    {
        var dto = new DateTimeOffset(2024, 8, 15, 12, 0, 0, TimeSpan.Zero);
        var text = StringHelper.FromDateTimeOffset(dto);
        var parsed = DateTimeOffsetHelper.ParseExactInvariant(text, "O", DateTimeStyles.RoundtripKind);
        await Assert.That(parsed).IsEqualTo(dto);
    }

    [Test]
    public async Task TryParseDateTimeOffset_ToDateOnly_RoundTrip()
    {
        var input = "2024-06-30T23:00:00.0000000+00:00";
        var ok = BoolHelper.TryParseExactDateTimeOffsetInvariant(input, "O", out var dto, DateTimeStyles.RoundtripKind);
        await Assert.That(ok).IsTrue();
        var date = DateOnlyHelper.FromDateTimeOffset(dto);
        var text = StringHelper.FromDateOnly(date);
        var parsed = DateOnlyHelper.ParseExactInvariant(text, "yyyy-MM-dd");
        await Assert.That(parsed).IsEqualTo(date);
    }

    [Test]
    public async Task TimeSpan_String_Parse_Total()
    {
        var ts = new TimeSpan(1, 30, 45);
        var formatted = StringHelper.FromTimeSpan(ts);
        var parsed = TimeSpanHelper.ParseExactInvariant(formatted, "c");
        await Assert.That(parsed).IsEqualTo(ts);
        await Assert.That(DoubleHelper.TotalSeconds(parsed)).IsEqualTo(ts.TotalSeconds);
        await Assert.That(FloatHelper.TotalHours(parsed)).IsEqualTo((float)ts.TotalHours);
    }

    [Test]
    public async Task Guid_Creation_Format_Parse_RoundTrip()
    {
        // Create new GUID
        var guid = GuidHelper.NewGuid();
        await Assert.That(GuidHelper.IsEmpty(guid)).IsFalse();
        
        // Convert to different formats
        var guidString = GuidHelper.ToStringD(guid);
        var guidN = GuidHelper.ToStringN(guid);
        var guidB = GuidHelper.ToStringB(guid);
        var guidBase64 = GuidHelper.ToBase64String(guid);
        
        // Parse back
        var parsedD = GuidHelper.Parse(guidString);
        var parsedN = GuidHelper.ParseOrNull(guidN);
        var parsedB = GuidHelper.ParseExactOrNull(guidB, "B");
        
        await Assert.That(parsedD).IsEqualTo(guid);
        await Assert.That(parsedN).IsEqualTo(guid);
        await Assert.That(parsedB).IsEqualTo(guid);
    }

    [Test]
    public async Task Integer_Math_DateTime_Integration()
    {
        // Create a date and extract components
        var date = new DateTime(2024, 3, 15, 14, 30, 45, 123, DateTimeKind.Utc);
        
        var year = IntHelper.Year(date);
        var month = IntHelper.Month(date);
        var day = IntHelper.Day(date);
        var hour = IntHelper.Hour(date);
        
        await Assert.That(year).IsEqualTo(2024);
        await Assert.That(month).IsEqualTo(3);
        await Assert.That(day).IsEqualTo(15);
        
        // Test mathematical operations
        await Assert.That(IntHelper.IsEven(month + 1)).IsTrue();
        await Assert.That(IntHelper.IsPrime(day)).IsFalse();
        
        var clampedHour = IntHelper.Clamp(hour, 0, 12);
        await Assert.That(clampedHour).IsEqualTo(12);
    }

    [Test]
    public async Task Decimal_String_Parse_Math_RoundTrip()
    {
        var value = 123.456m;
        var text = value.ToString(CultureInfo.InvariantCulture);
        var parsed = DecimalHelper.ParseInvariant(text);
        
        await Assert.That(parsed).IsEqualTo(value);
        
        var rounded = DecimalHelper.Round(parsed, 2);
        var ceiling = DecimalHelper.Ceiling(parsed);
        var floor = DecimalHelper.Floor(parsed);
        
        await Assert.That(rounded).IsEqualTo(123.46m);
        await Assert.That(ceiling).IsEqualTo(124m);
        await Assert.That(floor).IsEqualTo(123m);
    }

    [Test]
    public async Task Char_String_Validation_Integration()
    {
        var text = "Hello World!";
        
        var firstChar = CharHelper.GetFirstOrDefault(text);
        var lastChar = CharHelper.GetLastOrDefault(text);
        
        await Assert.That(firstChar).IsEqualTo('H');
        await Assert.That(lastChar).IsEqualTo('!');
        await Assert.That(CharHelper.IsVowel(CharHelper.ToLower(firstChar))).IsFalse();
        await Assert.That(CharHelper.IsVowel('e')).IsTrue();
        
        var repeated = CharHelper.Repeat('*', 5);
        await Assert.That(repeated).IsEqualTo("*****");
    }

    [Test]
    public async Task Collection_Operations_Integration()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Test various collection operations
        await Assert.That(CollectionHelper.HasAny(numbers)).IsTrue();
        await Assert.That(CollectionHelper.IsNullOrEmpty(numbers)).IsFalse();
        
        var (evens, odds) = CollectionHelper.Partition(numbers, n => IntHelper.IsEven(n));
        await Assert.That(evens.Count()).IsEqualTo(5);
        await Assert.That(odds.Count()).IsEqualTo(5);
        
        var chunks = CollectionHelper.ChunkBy(numbers, 3).ToList();
        await Assert.That(chunks.Count).IsEqualTo(4);
        
        var random = CollectionHelper.GetRandom(numbers, new Random(42));
        await Assert.That(numbers.Contains(random)).IsTrue();
    }

    [Test]
    public async Task Json_Serialization_DeepClone_Integration()
    {
        var testData = new TestData
        {
            Id = GuidHelper.NewGuid(),
            Name = "Integration Test",
            Value = 42.5m,
            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)
        };
        
        // Serialize to JSON
        var json = JsonHelper.Serialize(testData);
        await Assert.That(JsonHelper.IsValidJson(json)).IsTrue();
        
        // Deserialize back
        var deserialized = JsonHelper.Deserialize<TestData>(json);
        await Assert.That(deserialized.Id).IsEqualTo(testData.Id);
        await Assert.That(deserialized.Name).IsEqualTo(testData.Name);
        
        // Deep clone using ObjectHelper
        var cloned = ObjectHelper.DeepClone(testData);
        await Assert.That(cloned.Id).IsEqualTo(testData.Id);
        await Assert.That(ObjectHelper.IsNotNull(cloned)).IsTrue();
    }

    [Test]
    public async Task Color_Hex_RGB_Conversion_Integration()
    {
        // Create color from RGB
        var red = ColorHelper.RgbToColor(255, 0, 0);
        
        // Convert to hex and back
        var hexString = StringHelper.ColorToHexString(red);
        var (r, g, b) = ColorHelper.HexToRGB(hexString);
        
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(0);
        await Assert.That(b).IsEqualTo(0);
        
        // Test color operations
        // Use a darker red so we can test lightening
        var darkRed = ColorHelper.RgbToColor(128, 0, 0);
        var lighterRed = ColorHelper.Lighten(darkRed, 0.3);
        await Assert.That(lighterRed.R).IsGreaterThan(darkRed.R);
        
        var invertedRed = ColorHelper.Invert(red);
        await Assert.That((int)invertedRed.B).IsEqualTo(255);
        
        var contrastText = ColorHelper.GetContrastingTextColor(red);
        await Assert.That(contrastText).IsEqualTo(Color.White);
    }

    [Test]
    public async Task Email_Validation_Regex_Integration()
    {
        var validEmail = "test@example.com";
        var invalidEmail = "invalid.email";
        
        // Test with InternetValidationHelper
        await Assert.That(InternetValidationHelper.IsValidEmail(validEmail)).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidEmail(invalidEmail)).IsFalse();
        
        // Test with RegexHelper
        var isMatch = RegexHelper.IsMatch(validEmail, RegexHelper.EmailPattern.ToString());
        await Assert.That(RegexHelper.EmailPattern.IsMatch(validEmail)).IsTrue();
    }

    [Test]
    public async Task Uri_Validation_Domain_Integration()
    {
        var validUrl = "https://www.example.com";
        var validDomain = "example.com";
        var validIp = "192.168.1.1";
        
        await Assert.That(InternetValidationHelper.IsValidUri(validUrl)).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidDomainName(validDomain)).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidIpAddress(validIp)).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidIPv4Address(validIp)).IsTrue();
        await Assert.That(InternetValidationHelper.IsValidPortNumber(8080)).IsTrue();
    }

    [Test]
    public async Task Enum_Operations_Integration()
    {
        var testEnum = TestEnum.Second;
        
        var name = EnumHelper.GetEnumName(testEnum);
        var intValue = EnumHelper.ToInt(testEnum);
        var isDefined = EnumHelper.IsDefined(testEnum);
        
        await Assert.That(name).IsEqualTo("Second");
        await Assert.That(intValue).IsEqualTo(2);
        await Assert.That(isDefined).IsTrue();
        
        var parsed = EnumHelper.Parse<TestEnum>("Third");
        await Assert.That(parsed).IsEqualTo(TestEnum.Third);
        
        var allValues = EnumHelper.GetValues<TestEnum>();
        await Assert.That(allValues.Length).IsEqualTo(3);
    }

    [Test]
    public async Task TimeOnly_DateTime_Integration()
    {
        var time = new TimeOnly(14, 30, 45);
        var timeString = StringHelper.FromTimeOnly(time);
        var parsedTime = TimeOnlyHelper.ParseExactInvariant(timeString, "HH:mm:ss");
        
        await Assert.That(parsedTime).IsEqualTo(time);
        
        // TimeOnly to TimeSpan to extract components
        var timeSpan = TimeOnlyHelper.ToTimeSpan(time);
        var hour = IntHelper.Hours(timeSpan);
        var minute = IntHelper.Minutes(timeSpan);
        
        await Assert.That(hour).IsEqualTo(14);
        await Assert.That(minute).IsEqualTo(30);
    }

    [Test]
    public async Task Complex_DateTime_Workflow_Integration()
    {
        // Start with DateOnly and TimeOnly
        var date = new DateOnly(2024, 6, 15);
        var time = new TimeOnly(10, 30, 0);
        
        // Combine to DateTimeOffset
        var dto = DateTimeOffsetHelper.FromDateOnly(date, time, TimeZoneInfo.Utc);
        
        // Extract components using IntHelper
        var year = IntHelper.Year(dto);
        var month = IntHelper.Month(dto);
        var day = IntHelper.Day(dto);
        var hour = IntHelper.Hour(dto);
        
        await Assert.That(year).IsEqualTo(2024);
        await Assert.That(month).IsEqualTo(6);
        await Assert.That(day).IsEqualTo(15);
        await Assert.That(hour).IsEqualTo(10);
        
        // Convert to Unix timestamp
        var unixTime = LongHelper.ToUnixTimeSeconds(dto);
        var backToDto = DateTimeOffsetHelper.FromUnixTimeSeconds(unixTime);
        
        await Assert.That(backToDto.Year).IsEqualTo(year);
        
        // Format to string and parse back
        var formatted = StringHelper.FromDateTimeOffset(dto);
        var parsed = DateTimeOffsetHelper.ParseExactInvariant(formatted, "O", DateTimeStyles.RoundtripKind);
        
        await Assert.That(parsed.Date).IsEqualTo(dto.Date);
    }

    [Test]
    public async Task Regex_String_Manipulation_Integration()
    {
        var text = "  Hello    World!  ";
        
        // Test regex patterns
        await Assert.That(RegexHelper.AlphanumericPattern.IsMatch("ABC123")).IsTrue();
        await Assert.That(RegexHelper.NumericPattern.IsMatch("12345")).IsTrue();
        await Assert.That(RegexHelper.HexPattern.IsMatch("1A2B3C")).IsTrue();
        
        // Replace multiple whitespaces
        var cleaned = RegexHelper.Replace(text, @"\s+", " ");
        await Assert.That(cleaned.Contains("    ")).IsFalse();
        
        // Escape special characters
        var escaped = RegexHelper.Escape("test.*pattern");
        await Assert.That(escaped.Contains(@"\.")).IsTrue();
    }

    [Test]
    public async Task Bool_TryParse_Integration()
    {
        var intString = "123";
        var success = int.TryParse(intString, out var result);
        
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(123);
        
        var decimalString = "123.45";
        var decimalParsed = DecimalHelper.ParseInvariantOrNull(decimalString);
        
        await Assert.That(decimalParsed.HasValue).IsTrue();
        await Assert.That(decimalParsed.Value).IsEqualTo(123.45m);
        
        var guidString = GuidHelper.ToStringD(GuidHelper.NewGuid());
        var guidResult = GuidHelper.ParseOrNull(guidString);
        
        await Assert.That(guidResult.HasValue).IsTrue();
        await Assert.That(GuidHelper.IsEmpty(guidResult.Value)).IsFalse();
    }

    [Test]
    public async Task Collection_Async_Operations_Integration()
    {
        var items = new[] { 1, 2, 3, 4, 5 };
        var results = new List<int>();
        
        await CollectionHelper.ForEachAsync(items, async item =>
        {
            await Task.Delay(1);
            results.Add(item * 2);
        });
        
        await Assert.That(results.Count).IsEqualTo(5);
        await Assert.That(results.Contains(10)).IsTrue();
    }

    [Test]
    public async Task TimeSpan_Components_Integration()
    {
        var duration = new TimeSpan(2, 14, 30, 45, 500);
        
        var days = IntHelper.Days(duration);
        var hours = IntHelper.Hours(duration);
        var minutes = IntHelper.Minutes(duration);
        var seconds = IntHelper.Seconds(duration);
        
        await Assert.That(days).IsEqualTo(2);
        await Assert.That(hours).IsEqualTo(14);
        await Assert.That(minutes).IsEqualTo(30);
        await Assert.That(seconds).IsEqualTo(45);
        
        var totalHours = DoubleHelper.TotalHours(duration);
        await Assert.That(totalHours).IsGreaterThan(62.0);
    }

    [Test]
    public async Task String_Manipulation_Integration()
    {
        var text = "Hello World";
        
        // Test case conversions
        var pascal = StringHelper.ToPascalCase(text);
        var camel = StringHelper.ToCamelCase(text);
        var kebab = StringHelper.ToKebabCase(text);
        var snake = StringHelper.ToSnakeCase(text);
        
        await Assert.That(pascal).IsEqualTo("HelloWorld");
        await Assert.That(camel).IsEqualTo("helloWorld");
        await Assert.That(kebab).IsEqualTo("hello-world");
        await Assert.That(snake).IsEqualTo("hello_world");
        
        // Test truncate
        var truncated = StringHelper.Truncate("This is a very long text", 10);
        await Assert.That(truncated.Length).IsLessThanOrEqualTo(10);
        
        // Test repeat
        var repeated = StringHelper.Repeat("ab", 3);
        await Assert.That(repeated).IsEqualTo("ababab");
    }

    [Test]
    public async Task Base64_Encoding_Integration()
    {
        var original = "Hello, World!";
        
        var encoded = StringHelper.ToBase64(original);
        var decoded = StringHelper.FromBase64(encoded);
        
        await Assert.That(decoded).IsEqualTo(original);
        await Assert.That(encoded).IsNotEqualTo(original);
    }

    [Test]
    public async Task Object_Type_Checking_Integration()
    {
        var nullObject = (string?)null;
        var notNullObject = "test";
        var primitiveObject = 42;
        var stringObject = "string";
        
        await Assert.That(ObjectHelper.IsNull(nullObject)).IsTrue();
        await Assert.That(ObjectHelper.IsNotNull(notNullObject)).IsTrue();
        await Assert.That(ObjectHelper.IsPrimitive(primitiveObject)).IsTrue();
        await Assert.That(ObjectHelper.IsPrimitive(stringObject)).IsTrue();
    }

    [Test]
    public async Task Stream_String_ByteArray_RoundTrip_Integration()
    {
        var originalText = "Hello from StreamHelper!";
        
        // String -> Stream
        var stream = StreamHelper.FromString(originalText);
        await Assert.That(StreamHelper.IsReadable(stream)).IsTrue();
        await Assert.That(StreamHelper.IsSeekable(stream)).IsTrue();
        
        // Stream -> Byte Array
        stream.Position = 0;
        var bytes = StreamHelper.ToByteArray(stream);
        await Assert.That(bytes.Length).IsGreaterThan(0);
        
        // Byte Array -> Stream
        var newStream = StreamHelper.FromByteArray(bytes);
        
        // Stream -> String
        var backToString = StreamHelper.ToString(newStream);
        await Assert.That(backToString).IsEqualTo(originalText);
        
        stream.Dispose();
        newStream.Dispose();
    }

    [Test]
    public async Task Encryption_AES_Hash_Integration()
    {
        var plainText = "Sensitive Data";
        var key = "1234567890123456"; // 16 bytes for AES-128
        
        // Encrypt
        var encrypted = EncryptionHelper.EncryptAES(plainText, key);
        await Assert.That(encrypted).IsNotEqualTo(plainText);
        await Assert.That(encrypted.Contains(":")).IsTrue(); // IV:Data format
        
        // Decrypt
        var decrypted = EncryptionHelper.DecryptAES(encrypted, key);
        await Assert.That(decrypted).IsEqualTo(plainText);
        
        // Hash operations
        var sha256Hash = EncryptionHelper.HashSHA256(plainText);
        var sha256Hex = EncryptionHelper.HashSHA256Hex(plainText);
        
        await Assert.That(sha256Hash).IsNotEqualTo(plainText);
        await Assert.That(sha256Hex).IsNotEqualTo(plainText);
        await Assert.That(sha256Hex.Length).IsEqualTo(64); // SHA-256 = 32 bytes = 64 hex chars
        
        // Verify hashes are deterministic
        var sha256Hash2 = EncryptionHelper.HashSHA256(plainText);
        await Assert.That(sha256Hash).IsEqualTo(sha256Hash2);
    }

    [Test]
    public async Task CultureInfo_DateTime_Formatting_Integration()
    {
        var date = new DateTime(2024, 3, 15, 14, 30, 0);
        
        // Get cultures
        var deCulture = CultureInfoHelper.GetCultureOrInvariant("de-DE");
        var enCulture = CultureInfoHelper.GetCultureOrInvariant("en-US");
        var invariant = CultureInfoHelper.InvariantCulture;
        
        await Assert.That(deCulture).IsNotNull();
        await Assert.That(enCulture).IsNotNull();
        
        // Format date with different cultures
        var deFormatted = date.ToString("D", deCulture);
        var enFormatted = date.ToString("D", enCulture);
        
        await Assert.That(deFormatted).IsNotEqualTo(enFormatted);
        
        // Test GetCultureOrInvariant
        var validCulture = CultureInfoHelper.GetCultureOrInvariant("fr-FR");
        var invalidCulture = CultureInfoHelper.GetCultureOrInvariant("invalid-culture");
        
        await Assert.That(validCulture.Name).IsEqualTo("fr-FR");
        await Assert.That(invalidCulture).IsEqualTo(invariant);
        
        // Test culture properties
        var currencySymbol = CultureInfoHelper.GetCurrencySymbol(enCulture);
        await Assert.That(currencySymbol).IsEqualTo("$");
        
        var uses24Hour = CultureInfoHelper.Uses24HourFormat(deCulture);
        await Assert.That(uses24Hour).IsTrue();
    }

    [Test]
    public async Task MultiValidation_Workflow_Integration()
    {
        // Phone number validation
        var validUsPhone = "555-555-5555";
        var validE164Phone = "+14155552671";
        
        await Assert.That(PhoneNumberValidationHelper.IsValidUsPhoneNumber(validUsPhone)).IsTrue();
        await Assert.That(PhoneNumberValidationHelper.IsValidE164Format(validE164Phone)).IsTrue();
        
        // Postal code validation
        var usZip = "90210";
        var ukPostal = "SW1A 1AA";
        var germanPostal = "10115";
        
        await Assert.That(PostalCodeValidationHelper.IsValidUsZipCode(usZip)).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidUkPostalCode(ukPostal)).IsTrue();
        await Assert.That(PostalCodeValidationHelper.IsValidGermanPostalCode(germanPostal)).IsTrue();
        
        // ISBN validation
        var isbn10 = "0306406152";
        var isbn13 = "9780306406157";
        
        await Assert.That(IsbnValidationHelper.IsValidIsbn10(isbn10)).IsTrue();
        await Assert.That(IsbnValidationHelper.IsValidIsbn13(isbn13)).IsTrue();
        
        // Credit card validation (Luhn algorithm)
        var validCard = "4532015112830366"; // Valid test card
        await Assert.That(CreditCardValidationHelper.IsValidLuhn(validCard)).IsTrue();
        
        var cardType = CreditCardValidationHelper.GetCardType(validCard);
        await Assert.That(cardType).IsEqualTo("Visa");
        
        var maskedCard = CreditCardValidationHelper.Mask(validCard);
        await Assert.That(maskedCard.EndsWith("0366")).IsTrue();
        
        // IBAN validation
        var validIban = "DE89370400440532013000";
        await Assert.That(IbanValidationHelper.IsValidIban(validIban)).IsTrue();
    }

    [Test]
    public async Task VIN_Validation_Integration()
    {
        var validVin = "1HGBH41JXMN109186";
        var invalidVin = "INVALID123VIN456";
        
        await Assert.That(VinValidationHelper.IsValidVin(validVin)).IsTrue();
        await Assert.That(VinValidationHelper.IsValidVin(invalidVin)).IsFalse();
        
        // Extract information from VIN
        if (VinValidationHelper.IsValidVin(validVin))
        {
            var wmi = VinValidationHelper.GetWorldManufacturerId(validVin);
            await Assert.That(wmi).IsEqualTo("1HG");
            
            var yearCode = VinValidationHelper.GetModelYearCode(validVin);
            await Assert.That(yearCode.HasValue).IsTrue();
            
            if (yearCode.HasValue)
            {
                var year = VinValidationHelper.GetModelYear(yearCode.Value);
                await Assert.That(year.HasValue).IsTrue();
                if (year.HasValue)
                {
                    await Assert.That(year.Value).IsGreaterThan(1980);
                }
            }
        }
    }

    [Test]
    public async Task Complete_Data_Workflow_Integration()
    {
        // Create a complex data object
        var userId = GuidHelper.NewGuid();
        var username = "john.doe";
        var email = "john.doe@example.com";
        var phone = "+14155552671";
        var createdAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);
        
        // Validate data
        await Assert.That(InternetValidationHelper.IsValidEmail(email)).IsTrue();
        await Assert.That(PhoneNumberValidationHelper.IsValidE164Format(phone)).IsTrue();
        await Assert.That(GuidHelper.IsEmpty(userId)).IsFalse();
        
        // Format data - ToPascalCase on "john.doe" produces "John.doe" (only first letter is capitalized)
        var usernamePascal = StringHelper.ToPascalCase(username);
        var createdAtString = StringHelper.FromDateTime(createdAt);
        var year = IntHelper.Year(createdAt);
        
        // PascalCase converts "john.doe" to "John.doe" (not "JohnDoe")
        await Assert.That(usernamePascal).IsEqualTo("John.doe");
        await Assert.That(year).IsEqualTo(2024);
        
        // Create JSON object
        var userData = new
        {
            Id = userId,
            Username = usernamePascal,
            Email = email,
            Phone = phone,
            CreatedAt = createdAtString,
            Year = year
        };
        
        // Serialize
        var json = JsonHelper.Serialize(userData);
        await Assert.That(JsonHelper.IsValidJson(json)).IsTrue();
        
        // Hash sensitive data
        var hashedEmail = EncryptionHelper.HashSHA256(email);
        await Assert.That(hashedEmail).IsNotEqualTo(email);
        
        // Convert to stream
        using var stream = StreamHelper.FromString(json);
        await Assert.That(StreamHelper.IsReadable(stream)).IsTrue();
        
        var jsonFromStream = StreamHelper.ToString(stream);
        await Assert.That(jsonFromStream).IsEqualTo(json);
    }

    #endregion

    #region Extension Methods Integration Tests

    [Test]
    public async Task StringExtensions_Fluent_Integration()
    {
        var text = "  Hello World  ";
        
        // Test extension methods in fluent style
        var result = text
            .Trim()
            .ToLower()
            .ToPascalCase()
            .EnsureEndsWith("!");
        
        // ToPascalCase converts "hello world" to "HelloWorld"
        await Assert.That(result).IsEqualTo("HelloWorld!");
        
        // Test parsing extensions
        var dateString = "2024-03-15";
        var success = dateString.TryParseDateOnlyInvariant("yyyy-MM-dd", out var date);
        await Assert.That(success).IsTrue();
        await Assert.That(date.Year).IsEqualTo(2024);
        
        // Test string validation extensions
        await Assert.That("12345".IsNumeric()).IsTrue();
        await Assert.That("ABC123".IsAlphanumeric()).IsTrue();
        await Assert.That("hello".HasValue()).IsTrue();
        await Assert.That(((string?)null).IsNullOrEmpty()).IsTrue();
    }

    [Test]
    public async Task DateTimeExtensions_Integration()
    {
        var date = new DateTime(2024, 3, 15, 14, 30, 45, DateTimeKind.Utc);
        
        // Use extension methods
        var startOfDay = date.StartOfDay();
        var endOfDay = date.EndOfDay();
        var nextDay = date.AddDays(1);
        
        await Assert.That(startOfDay.Hour).IsEqualTo(0);
        await Assert.That(startOfDay.Minute).IsEqualTo(0);
        await Assert.That(endOfDay.Hour).IsEqualTo(23);
        await Assert.That(endOfDay.Minute).IsEqualTo(59);
        
        // Test age calculation
        var birthDate = new DateTime(1990, 3, 15);
        var age = birthDate.GetAge();
        await Assert.That(age).IsGreaterThan(30);
        
        // Test week operations
        var weekStart = date.StartOfWeek();
        await Assert.That(weekStart.DayOfWeek).IsEqualTo(DayOfWeek.Monday);
        
        // Test queries
        await Assert.That(date.IsInPast()).IsTrue();
        await Assert.That(DateTime.Now.AddDays(1).IsInFuture()).IsTrue();
        await Assert.That(DateTime.Today.IsToday()).IsTrue();
    }

    [Test]
    public async Task IntExtensions_Integration()
    {
        var number = 42;
        
        // Test extension methods
        await Assert.That(number.IsEven()).IsTrue();
        await Assert.That((number + 1).IsOdd()).IsTrue();
        await Assert.That(17.IsPrime()).IsTrue();
        
        var clamped = 100.Clamp(0, 50);
        await Assert.That(clamped).IsEqualTo(50);
        
        var abs = (-42).Abs();
        await Assert.That(abs).IsEqualTo(42);
    }

    [Test]
    public async Task DecimalExtensions_Integration()
    {
        var value = 123.456m;
        
        // Test extension methods
        var rounded = value.Round(2);
        var ceiling = value.Ceiling();
        var floor = value.Floor();
        
        await Assert.That(rounded).IsEqualTo(123.46m);
        await Assert.That(ceiling).IsEqualTo(124m);
        await Assert.That(floor).IsEqualTo(123m);
        
        var abs = (-123.456m).Abs();
        await Assert.That(abs).IsEqualTo(123.456m);
    }

    [Test]
    public async Task GuidExtensions_Integration()
    {
        var guid = Guid.NewGuid();
        
        // Test extension methods
        await Assert.That(guid.IsEmpty()).IsFalse();
        await Assert.That(Guid.Empty.IsEmpty()).IsTrue();
        
        var guidString = guid.ToStringN();
        await Assert.That(guidString.Length).IsEqualTo(32);
        
        var base64 = guid.ToBase64String();
        await Assert.That(base64).IsNotNull();
    }

    [Test]
    public async Task CollectionExtensions_Integration()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Test extension methods
        await Assert.That(numbers.IsNullOrEmpty()).IsFalse();
        await Assert.That(numbers.HasAny()).IsTrue();
        
        var evens = numbers.Where(n => n.IsEven()).ToList();
        await Assert.That(evens.Count).IsEqualTo(2);
        
        var shuffled = numbers.Shuffle().ToList();
        await Assert.That(shuffled.Count).IsEqualTo(numbers.Count);
        
        var random = numbers.GetRandom();
        await Assert.That(numbers.Contains(random)).IsTrue();
    }

    [Test]
    public async Task EnumExtensions_Integration()
    {
        var testEnum = TestEnum.Second;
        
        // Test extension methods
        var description = testEnum.ToDescription();
        var intValue = testEnum.ToInt();
        
        await Assert.That(intValue).IsEqualTo(2);
        await Assert.That(testEnum.IsDefined()).IsTrue();
        await Assert.That(testEnum.HasFlag(TestEnum.Second)).IsTrue();
    }

    [Test]
    public async Task TimeSpanExtensions_Integration()
    {
        var duration = TimeSpan.FromHours(2.5);
        
        // Test extension methods
        var totalHours = duration.TotalHours;
        var totalMinutes = duration.TotalMinutes;
        
        await Assert.That(totalHours).IsEqualTo(2.5);
        await Assert.That(totalMinutes).IsEqualTo(150);
        
        var formatted = duration.ToInvariantString();
        await Assert.That(formatted).IsNotNull();
    }

    [Test]
    public async Task DateOnlyExtensions_Integration()
    {
        var date = new DateOnly(2024, 3, 15);
        
        // Test extension methods
        var nextDay = date.AddDays(1);
        var nextMonth = date.AddMonths(1);
        
        await Assert.That(nextDay.Day).IsEqualTo(16);
        await Assert.That(nextMonth.Month).IsEqualTo(4);
        
        // Test conversion to DateTime
        var dateTime = date.ToDateTime(new TimeOnly(14, 30));
        await Assert.That(dateTime.Year).IsEqualTo(2024);
        await Assert.That(dateTime.Hour).IsEqualTo(14);
    }

    [Test]
    public async Task TimeOnlyExtensions_Integration()
    {
        var time = new TimeOnly(14, 30, 45);
        
        // Test extension methods
        var laterTime = time.AddHours(2);
        var earlierTime = time.AddMinutes(-30);
        
        await Assert.That(laterTime.Hour).IsEqualTo(16);
        await Assert.That(earlierTime.Hour).IsEqualTo(14);
        await Assert.That(earlierTime.Minute).IsEqualTo(0);
        
        var timeSpan = time.ToTimeSpan();
        await Assert.That(timeSpan.Hours).IsEqualTo(14);
    }

    [Test]
    public async Task ColorExtensions_Integration()
    {
        var red = Color.FromArgb(255, 0, 0);
        
        // Test extension methods via helper
        var hexString = StringHelper.ColorToHexString(red);
        await Assert.That(hexString).IsEqualTo("#FF0000");
        
        var (r, g, b) = red.ToRGB();
        await Assert.That(r).IsEqualTo(255);
        await Assert.That(g).IsEqualTo(0);
        await Assert.That(b).IsEqualTo(0);
        
        var inverted = red.Invert();
        await Assert.That(inverted.B).IsEqualTo((byte)255);
        
        await Assert.That(red.IsDark()).IsTrue();
    }

    [Test]
    public async Task BoolExtensions_Integration()
    {
        var trueValue = true;
        var falseValue = false;
        
        // Test extension methods
        await Assert.That(trueValue.ToYesNo()).IsEqualTo("Yes");
        await Assert.That(falseValue.ToYesNo()).IsEqualTo("No");
        
        await Assert.That(trueValue.ToInt()).IsEqualTo(1);
        await Assert.That(falseValue.ToInt()).IsEqualTo(0);
    }

    [Test]
    public async Task ObjectExtensions_Integration()
    {
        var testObject = new TestData
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Value = 100m,
            CreatedAt = DateTime.UtcNow
        };
        
        // Test extension methods
        await Assert.That(testObject.IsNull()).IsFalse();
        await Assert.That(((TestData?)null).IsNull()).IsTrue();
        
        var cloned = testObject.DeepClone();
        await Assert.That(cloned.Id).IsEqualTo(testObject.Id);
        await Assert.That(cloned).IsNotEqualTo(testObject); // Different reference
        
        await Assert.That(42.IsPrimitive()).IsTrue();
        await Assert.That("string".IsPrimitive()).IsTrue();
    }

    [Test]
    public async Task Extensions_Complete_Workflow_Integration()
    {
        // Complex workflow using only extension methods
        var userData = new
        {
            Username = "john_doe",
            Email = "  JOHN.DOE@EXAMPLE.COM  ",
            BirthDate = new DateTime(1990, 6, 15),
            Score = 87.654m,
            IsActive = true
        };
        
        // Process username
        var processedUsername = userData.Username
            .Replace("_", " ")
            .ToPascalCase();
        await Assert.That(processedUsername).IsEqualTo("JohnDoe");
        
        // Process email
        var processedEmail = userData.Email
            .Trim()
            .ToLower();
        await Assert.That(processedEmail).IsEqualTo("john.doe@example.com");
        
        // Calculate age
        var age = userData.BirthDate.GetAge();
        await Assert.That(age).IsGreaterThan(30);
        
        // Process score
        var roundedScore = userData.Score.Round(1);
        await Assert.That(roundedScore).IsEqualTo(87.7m);
        
        // Process boolean
        var activeStatus = userData.IsActive.ToYesNo();
        await Assert.That(activeStatus).IsEqualTo("Yes");
        
        // Create GUID and convert
        var userId = Guid.NewGuid();
        var userIdShort = userId.ToStringN();
        await Assert.That(userIdShort.Length).IsEqualTo(32);
        await Assert.That(userId.IsEmpty()).IsFalse();
        
        // Work with dates
        var today = DateOnly.FromDateTime(DateTime.Today);
        var nextMonth = today.AddMonths(1);
        
        await Assert.That(nextMonth.Month).IsGreaterThan(today.Month);
        
        // Work with DateTimes
        var now = DateTime.Now;
        var startOfDay = now.StartOfDay();
        var startOfMonth = now.StartOfMonth();
        
        await Assert.That(startOfDay.Hour).IsEqualTo(0);
        await Assert.That(startOfMonth.Day).IsEqualTo(1);
    }

    #endregion

    // Helper class for JSON tests
    private class TestData
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Helper enum for enum tests
    private enum TestEnum
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}
