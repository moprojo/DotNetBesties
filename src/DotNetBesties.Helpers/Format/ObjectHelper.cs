using System;
using System.Text.Json;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helper class for object conversions.
/// </summary>
public static class ObjectHelper
{
    /// <summary>
    /// Attempts to cast the object to the specified reference type.
    /// </summary>
    /// <typeparam name="T">The target reference type.</typeparam>
    /// <param name="obj">The object to cast.</param>
    /// <returns>The cast object, or <c>null</c> if the cast fails.</returns>
    public static T? As<T>(object? obj) where T : class
    {
        return obj as T;
    }

    /// <summary>
    /// Casts the object to the specified value type.
    /// Throws an exception if the cast is invalid.
    /// </summary>
    /// <typeparam name="T">The target value type.</typeparam>
    /// <param name="obj">The object to cast.</param>
    /// <returns>The cast value.</returns>
    /// <exception cref="InvalidCastException">Thrown when the cast is invalid.</exception>
    /// <exception cref="NullReferenceException">Thrown when obj is null.</exception>
    public static T Cast<T>(object obj) where T : struct
    {
        return (T)obj;
    }

    /// <summary>
    /// Creates a deep clone of the object using JSON serialization.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to clone.</param>
    /// <returns>A deep clone of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
    public static T DeepClone<T>(T obj) where T : class
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException("Deserialization failed.");
    }

    /// <summary>
    /// Checks if the object is null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns><c>true</c> if the object is null; otherwise, <c>false</c>.</returns>
    public static bool IsNull(object? obj)
    {
        return obj == null;
    }

    /// <summary>
    /// Checks if the object is not null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns><c>true</c> if the object is not null; otherwise, <c>false</c>.</returns>
    public static bool IsNotNull(object? obj)
    {
        return obj != null;
    }

    /// <summary>
    /// Determines if the object is a primitive type, string, or decimal.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns><c>true</c> if the object is a primitive type, string, or decimal; otherwise, <c>false</c>.</returns>
    public static bool IsPrimitive(object? obj)
    {
        if (obj == null) return false;
        return obj.GetType().IsPrimitive || obj is string || obj is decimal;
    }
}
