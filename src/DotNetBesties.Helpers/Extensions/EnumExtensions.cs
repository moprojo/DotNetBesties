using System;
using System.ComponentModel;
using System.Reflection;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="Enum"/> types.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the name of the enum value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The name of the enum value, or null if no name was found.</returns>
    public static string? GetEnumName<TEnum>(this TEnum value) where TEnum : struct, Enum
        => Format.EnumHelper.GetEnumName(value);

    /// <summary>
    /// Checks if the given enum value is defined in the enum type.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns><c>true</c> if the value is defined; otherwise, <c>false</c>.</returns>
    public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, Enum
        => Format.EnumHelper.IsDefined(value);

    /// <summary>
    /// Retrieves the description attribute of the enum value, if available.
    /// Otherwise, returns the name of the enum value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The description or string representation of the enum value.</returns>
    public static string ToDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
        => Format.EnumHelper.ToDescription(value);

    /// <summary>
    /// Gets the underlying integer value of the enum.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The underlying integer value.</returns>
    public static int ToInt<TEnum>(this TEnum value) where TEnum : struct, Enum
        => Format.EnumHelper.ToInt(value);

    /// <summary>
    /// Checks if the enum has the specified flag set.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <param name="flag">The flag to check.</param>
    /// <returns><c>true</c> if the flag is set; otherwise, <c>false</c>.</returns>
    public static bool HasFlagFast<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum
        => Format.EnumHelper.HasFlag(value, flag);
}