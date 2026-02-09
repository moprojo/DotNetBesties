# ✨ DotNetBesties.Helpers

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A comprehensive collection of powerful helper classes and extension methods for .NET 9 applications. This library provides productivity-boosting utilities for common development tasks including string manipulation, validation, formatting, and more.

## 🚀 Features

### 📝 String Extensions & Helpers
- **Null/Empty Checks**: `IsNullOrEmpty()`, `IsNullOrWhiteSpace()`, `HasValue()`
- **Validation**: `IsNumeric()`, `IsAlphanumeric()`
- **Case Conversion**: `ToPascalCase()`, `ToCamelCase()`, `ToKebabCase()`, `ToSnakeCase()`, `ToTitleCase()`
- **Encoding**: `ToBase64()`, `FromBase64()`
- **Manipulation**: `Truncate()`, `Reverse()`, `RemoveWhitespace()`, `CollapseWhitespace()`
- **Comparison**: `EqualsIgnoreCase()`, `StartsWithIgnoreCase()`, `EndsWithIgnoreCase()`
- **Substring Operations**: `Left()`, `Right()`, `Mid()`
- **Replace**: `ReplaceFirst()`, `ReplaceLast()`
- **Utilities**: `CountOccurrences()`, `Repeat()`, `EnsureStartsWith()`, `EnsureEndsWith()`

### 🔢 Numeric Extensions
- **Int Extensions**: Formatting, parsing, range checking
- **Long Extensions**: Large number operations
- **Float/Double Extensions**: Precision handling
- **Decimal Extensions**: Financial calculations

### 📅 Date & Time Extensions
- **DateTime Extensions**: Formatting, manipulation, comparisons
- **DateOnly Extensions**: Date-specific operations (.NET 6+)
- **TimeOnly Extensions**: Time-specific operations (.NET 6+)
- **DateTimeOffset Extensions**: Timezone-aware operations
- **TimeSpan Extensions**: Duration calculations

### ✅ Validation Helpers
- **IBAN Validation**: International bank account number validation with mod-97 algorithm
- **Credit Card Validation**: Luhn algorithm implementation
- **ISBN Validation**: Book number validation (ISBN-10 and ISBN-13)
- **VIN Validation**: Vehicle identification number validation
- **Phone Number Validation**: International phone number formats
- **Postal Code Validation**: Country-specific postal code validation
- **Internet Validation**: Email, URL, and IP address validation

### 🎨 Format Helpers
- **CollectionHelper**: List and array formatting
- **ColorHelper**: Color conversion and manipulation
- **GuidHelper**: GUID generation and formatting
- **ObjectHelper**: Object serialization and formatting

### 🔒 Security & Cryptography
- **EncryptionHelper**: Encryption and decryption utilities

### 📦 Serialization
- **JsonHelper**: JSON serialization/deserialization utilities

### 🛠️ Common Utilities
- **RegexHelper**: Regular expression utilities
- **StreamHelper**: Stream operations
- **CultureInfoHelper**: Culture-specific operations

### 🎯 Other Extensions
- **EnumExtensions**: Enum utilities and parsing
- **BoolExtensions**: Boolean operations
- **CharExtensions**: Character operations
- **ColorExtensions**: System.Drawing.Color extensions
- **ObjectExtensions**: Generic object operations
- **CollectionExtensions**: IEnumerable and collection extensions

## 📦 Installation

### Using .NET CLI
```bash
dotnet add package DotNetBesties.Helpers
```

### Using Package Manager Console
```powershell
Install-Package DotNetBesties.Helpers
```

### Using PackageReference
```xml
<PackageReference Include="DotNetBesties.Helpers" Version="1.0.0" />
```

## 🎯 Quick Start

### String Extensions Example
```csharp
using DotNetBesties.Helpers.Extensions;

// Null/Empty checks
string? name = null;
if (name.IsNullOrEmpty()) 
{
    name = "John Doe";
}

// Case conversion
string title = "hello world".ToTitleCase(); // "Hello World"
string variable = "HelloWorld".ToCamelCase(); // "helloWorld"
string url = "Hello World".ToKebabCase(); // "hello-world"

// String manipulation
string text = "This is a very long text".Truncate(10); // "This is..."
string encoded = "Hello".ToBase64(); // "SGVsbG8="
string decoded = encoded.FromBase64(); // "Hello"

// Comparison
bool matches = "Hello".EqualsIgnoreCase("hello"); // true
```

### Validation Examples
```csharp
using DotNetBesties.Helpers.Validation;

// IBAN validation
bool isValid = IbanValidationHelper.IsValidIban("DE89370400440532013000");
string formatted = IbanValidationHelper.FormatIban("DE89370400440532013000");
// Result: "DE89 3704 0044 0532 0130 00"

// Credit card validation
bool isValidCard = CreditCardValidationHelper.IsValidCreditCard("4532015112830366");

// ISBN validation
bool isValidIsbn = IsbnValidationHelper.IsValidIsbn("978-3-16-148410-0");

// Phone number validation
bool isValidPhone = PhoneNumberValidationHelper.IsValidPhoneNumber("+49 123 456789", "DE");
```

### DateTime Extensions Example
```csharp
using DotNetBesties.Helpers.Extensions;

DateTime now = DateTime.Now;

// Date operations
DateTime tomorrow = now.AddDays(1);
bool isWeekend = now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday;

// Formatting
string formatted = now.ToString("yyyy-MM-dd");
```

### Collection Extensions Example
```csharp
using DotNetBesties.Helpers.Extensions;

var numbers = new[] { 1, 2, 3, 4, 5 };

// Collection operations
bool hasItems = numbers.Any();
int sum = numbers.Sum();
```

## 📂 Project Structure

```
DotNetBesties.Helpers/
├── src/
│   └── DotNetBesties.Helpers/
│       ├── Common/              # Common utilities
│       │   ├── CultureInfoHelper.cs
│       │   ├── RegexHelper.cs
│       │   └── StreamHelper.cs
│       ├── Cryptology/          # Encryption utilities
│       │   └── EncryptionHelper.cs
│       ├── Extensions/          # Extension methods
│       │   ├── BoolExtensions.cs
│       │   ├── CharExtensions.cs
│       │   ├── CollectionExtensions.cs
│       │   ├── ColorExtensions.cs
│       │   ├── DateOnlyExtensions.cs
│       │   ├── DateTimeExtensions.cs
│       │   ├── DateTimeOffsetExtensions.cs
│       │   ├── DecimalExtensions.cs
│       │   ├── DoubleExtensions.cs
│       │   ├── EnumExtensions.cs
│       │   ├── FloatExtensions.cs
│       │   ├── GuidExtensions.cs
│       │   ├── IntExtensions.cs
│       │   ├── LongExtensions.cs
│       │   ├── ObjectExtensions.cs
│       │   ├── StringExtensions.cs
│       │   ├── TimeOnlyExtensions.cs
│       │   └── TimeSpanExtensions.cs
│       ├── Format/              # Formatting helpers
│       │   ├── BoolHelper.cs
│       │   ├── CharHelper.cs
│       │   ├── CollectionHelper.cs
│       │   ├── ColorHelper.cs
│       │   ├── DateOnlyHelper.cs
│       │   ├── DateTimeHelper.cs
│       │   ├── DateTimeOffsetHelper.cs
│       │   ├── DecimalHelper.cs
│       │   ├── DoubleHelper.cs
│       │   ├── EnumHelper.cs
│       │   ├── FloatHelper.cs
│       │   ├── GuidHelper.cs
│       │   ├── IntHelper.cs
│       │   ├── LongHelper.cs
│       │   ├── ObjectHelper.cs
│       │   ├── StringHelper.cs
│       │   ├── TimeOnlyHelper.cs
│       │   └── TimeSpanHelper.cs
│       ├── Serialization/       # Serialization utilities
│       │   └── JsonHelper.cs
│       └── Validation/          # Validation helpers
│           ├── CreditCardValidationHelper.cs
│           ├── IbanValidationHelper.cs
│           ├── InternetValidationHelper.cs
│           ├── IsbnValidationHelper.cs
│           ├── PhoneNumberValidationHelper.cs
│           ├── PostalCodeValidationHelper.cs
│           └── VinValidationHelper.cs
└── tests/
    └── DotNetBesties.Helpers.Tests/
        ├── Common/              # Common tests
        ├── Cryptology/          # Encryption tests
        ├── Extensions/          # Extension tests
        ├── Format/              # Format tests
        ├── Serialization/       # Serialization tests
        └── Validation/          # Validation tests
```

## 🧪 Testing

The project includes comprehensive unit tests using [TUnit](https://github.com/thomhurst/TUnit), covering all helper methods and extension functions.

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## ⚙️ Requirements

- **.NET 9.0** or higher
- **C# 13.0** or higher

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# coding conventions
- Include XML documentation for public APIs
- Write unit tests for new features
- Some Integration tests are implemented, but not all. Please consider adding more integration tests for new features.
- Ensure all tests pass before submitting PR

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with ❤️ by the Moprojo team
- Inspired by community needs for productive .NET development
- Special thanks to all contributors

## 💬 Support

- 🐛 **Issues**: [GitHub Issues](https://github.com/moprojo/DotNetBesties.Helpers/issues)
- 💡 **Discussions**: [GitHub Discussions](https://github.com/moprojo/DotNetBesties.Helpers/discussions)
- 📧 **Email**: info@moprojo.de

## 🗺️ Roadmap

- [ ] Add more validation helpers
- [ ] Performance optimizations
- [ ] Additional collection extensions
- [ ] Async/await extensions
- [ ] More cryptography utilities

## 📊 Stats

![GitHub stars](https://img.shields.io/github/stars/moprojo/DotNetBesties.Helpers?style=social)
![GitHub forks](https://img.shields.io/github/forks/moprojo/DotNetBesties.Helpers?style=social)
![GitHub watchers](https://img.shields.io/github/watchers/moprojo/DotNetBesties.Helpers?style=social)

---

**Made with ❤️ by [Moprojo](https://moprojo.de)**
