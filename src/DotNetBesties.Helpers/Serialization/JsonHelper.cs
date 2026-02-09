using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetBesties.Helpers.Serialization;

/// <summary>
/// Provides helper methods for JSON serialization and deserialization.
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Default JSON serializer options with common settings.
    /// </summary>
    public static JsonSerializerOptions DefaultOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Pretty-print JSON serializer options with indentation.
    /// </summary>
    public static JsonSerializerOptions PrettyPrintOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Serializes an object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string Serialize<T>(T value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, options ?? DefaultOptions);
    }

    /// <summary>
    /// Serializes an object to a pretty-printed JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A pretty-printed JSON string representation of the object.</returns>
    public static string SerializePretty<T>(T value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, options ?? PrettyPrintOptions);
    }

    /// <summary>
    /// Deserializes a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>The deserialized object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when json is null.</exception>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static T Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(json);
        return JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions)
            ?? throw new JsonException("Deserialization resulted in null.");
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="result">The deserialized object if successful, otherwise default.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns><c>true</c> if deserialization was successful; otherwise, <c>false</c>.</returns>
    public static bool TryDeserialize<T>(string? json, out T? result, JsonSerializerOptions? options = null)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            result = JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions);
            return result != null;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates whether a string is valid JSON.
    /// </summary>
    /// <param name="json">The string to validate.</param>
    /// <returns><c>true</c> if the string is valid JSON; otherwise, <c>false</c>.</returns>
    public static bool IsValidJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            using var document = JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Pretty-prints a JSON string with indentation.
    /// </summary>
    /// <param name="json">The JSON string to format.</param>
    /// <returns>A formatted JSON string with indentation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when json is null.</exception>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static string PrettyPrint(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        using var document = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(document, PrettyPrintOptions);
    }

    /// <summary>
    /// Minifies a JSON string by removing all whitespace.
    /// </summary>
    /// <param name="json">The JSON string to minify.</param>
    /// <returns>A minified JSON string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when json is null.</exception>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static string Minify(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        using var document = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(document, DefaultOptions);
    }

    /// <summary>
    /// Creates a deep clone of an object using JSON serialization.
    /// </summary>
    /// <typeparam name="T">The type of the object to clone.</typeparam>
    /// <param name="source">The object to clone.</param>
    /// <param name="options">Optional serializer options.</param>
    /// <returns>A deep clone of the object.</returns>
    public static T Clone<T>(T source, JsonSerializerOptions? options = null)
    {
        var json = Serialize(source, options);
        return Deserialize<T>(json, options);
    }
}
