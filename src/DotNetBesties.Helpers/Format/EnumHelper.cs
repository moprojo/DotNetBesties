using System;
using System.ComponentModel;
using System.Reflection;

namespace DotNetBesties.Helpers.Format
{
    /// <summary>
    /// Helper class for converting and managing Enum values.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the name of the enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The name of the enum value, or null if no name was found.</returns>
        public static string? GetEnumName<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Enum.GetName(value);
        }

        /// <summary>
        /// Checks if the given enum value is defined in the enum type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>True if the value is defined; otherwise, false.</returns>
        public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Enum.IsDefined(value);
        }

        /// <summary>
        /// Retrieves the description attribute of the enum value, if available.
        /// Otherwise, returns the name of the enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The description or string representation of the enum value.</returns>
        public static string ToDescription<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);
            var name = Enum.GetName(value);

            if (name == null)
            {
                return value.ToString();
            }

            var fieldInfo = type.GetField(name);
            if (fieldInfo == null)
            {
                return value.ToString();
            }

            var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);
            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// Gets the underlying integer value of the enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The underlying integer value.</returns>
        public static int ToInt<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Checks if the enum has the specified flag set.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <param name="flag">The flag to check.</param>
        /// <returns><c>true</c> if the flag is set; otherwise, <c>false</c>.</returns>
        public static bool HasFlag<TEnum>(TEnum value, TEnum flag) where TEnum : struct, Enum
        {
            return value.HasFlag(flag);
        }

        /// <summary>
        /// Parses a string representation of an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="ignoreCase">Whether to ignore case during parsing.</param>
        /// <returns>The parsed enum value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        /// <exception cref="ArgumentException">Thrown when value is not found in the enum.</exception>
        public static TEnum Parse<TEnum>(string value, bool ignoreCase = false) where TEnum : struct, Enum
        {
            return Enum.Parse<TEnum>(value, ignoreCase);
        }

        /// <summary>
        /// Tries to parse a string representation of an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="result">The parsed enum value.</param>
        /// <param name="ignoreCase">Whether to ignore case during parsing.</param>
        /// <returns>True if parsing was successful; otherwise, false.</returns>
        public static bool TryParse<TEnum>(string? value, out TEnum result, bool ignoreCase = false) where TEnum : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out result);
        }

        /// <summary>
        /// Gets all values of the specified enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns>An array of enum values.</returns>
        public static TEnum[] GetValues<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>();
        }

        /// <summary>
        /// Gets all names of the specified enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns>An array of enum names.</returns>
        public static string[] GetNames<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetNames<TEnum>();
        }
    }
}
