using System;
using System.Text.Json;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Serialization;

namespace DotNetBesties.Helpers.Tests.Serialization;

public class JsonHelperTests
{
    private class TestClass
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Optional { get; set; }
    }

    private class NestedTestClass
    {
        public string Value { get; set; } = string.Empty;
        public TestClass? Nested { get; set; }
    }

    #region Serialize Tests

    [Test]
    public async Task Serialize_SerializesObject()
    {
        var obj = new TestClass { Name = "John", Age = 30 };
        var json = JsonHelper.Serialize(obj);

        await Assert.That(json).Contains("John"); // Case-sensitive match
        await Assert.That(json).Contains("30");
    }

    [Test]
    public async Task Serialize_WithNullProperties_OmitsNulls()
    {
        var obj = new TestClass { Name = "John", Age = 30, Optional = null };
        var json = JsonHelper.Serialize(obj);

        await Assert.That(json).DoesNotContain("optional");
    }

    [Test]
    public async Task Serialize_WithCustomOptions_UsesOptions()
    {
        var obj = new TestClass { Name = "John", Age = 30 };
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonHelper.Serialize(obj, options);

        await Assert.That(json).Contains("\n");
    }

    [Test]
    public async Task SerializePretty_SerializesWithIndentation()
    {
        var obj = new TestClass { Name = "John", Age = 30 };
        var json = JsonHelper.SerializePretty(obj);

        await Assert.That(json).Contains("\n");
        await Assert.That(json).Contains("  ");
    }

    [Test]
    public async Task SerializePretty_WithComplexObject_FormatsCorrectly()
    {
        var obj = new NestedTestClass 
        { 
            Value = "parent", 
            Nested = new TestClass { Name = "John", Age = 30 } 
        };
        var json = JsonHelper.SerializePretty(obj);

        await Assert.That(json).Contains("parent");
        await Assert.That(json).Contains("John"); // Case-sensitive match
        await Assert.That(json).Contains("\n");
    }

    #endregion

    #region Deserialize Tests

    [Test]
    public async Task Deserialize_DeserializesObject()
    {
        var json = "{\"name\":\"John\",\"age\":30}";
        var obj = JsonHelper.Deserialize<TestClass>(json);

        await Assert.That(obj).IsNotNull();
        await Assert.That(obj.Name).IsEqualTo("John");
        await Assert.That(obj.Age).IsEqualTo(30);
    }

    [Test]
    public async Task Deserialize_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => JsonHelper.Deserialize<TestClass>(null!)));
    }

    [Test]
    public async Task Deserialize_WithInvalidJson_ThrowsException()
    {
        await Assert.ThrowsAsync<JsonException>(
            async () => await Task.Run(() => JsonHelper.Deserialize<TestClass>("invalid json")));
    }

    [Test]
    public async Task Deserialize_WithNullResult_ThrowsException()
    {
        var json = "null";
        await Assert.ThrowsAsync<JsonException>(
            async () => await Task.Run(() => JsonHelper.Deserialize<TestClass>(json)));
    }

    [Test]
    public async Task Deserialize_CaseInsensitive_Works()
    {
        var json = "{\"NAME\":\"John\",\"AGE\":30}";
        var obj = JsonHelper.Deserialize<TestClass>(json);

        await Assert.That(obj.Name).IsEqualTo("John");
        await Assert.That(obj.Age).IsEqualTo(30);
    }

    [Test]
    public async Task Deserialize_WithCustomOptions_UsesOptions()
    {
        var json = "{\"Name\":\"John\",\"Age\":30}";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false };
        var obj = JsonHelper.Deserialize<TestClass>(json, options);

        await Assert.That(obj).IsNotNull();
    }

    [Test]
    public async Task Deserialize_WithNestedObject_DeserializesCorrectly()
    {
        var json = "{\"value\":\"parent\",\"nested\":{\"name\":\"John\",\"age\":30}}";
        var obj = JsonHelper.Deserialize<NestedTestClass>(json);

        await Assert.That(obj.Value).IsEqualTo("parent");
        await Assert.That(obj.Nested).IsNotNull();
        await Assert.That(obj.Nested!.Name).IsEqualTo("John");
    }

    #endregion

    #region TryDeserialize Tests

    [Test]
    public async Task TryDeserialize_WithValidJson_ReturnsTrue()
    {
        var json = "{\"name\":\"John\",\"age\":30}";
        var success = JsonHelper.TryDeserialize<TestClass>(json, out var obj);

        await Assert.That(success).IsTrue();
        await Assert.That(obj).IsNotNull();
        await Assert.That(obj!.Name).IsEqualTo("John");
    }

    [Test]
    public async Task TryDeserialize_WithInvalidJson_ReturnsFalse()
    {
        var success = JsonHelper.TryDeserialize<TestClass>("invalid json", out var obj);

        await Assert.That(success).IsFalse();
        await Assert.That(obj).IsNull();
    }

    [Test]
    public async Task TryDeserialize_WithNull_ReturnsFalse()
    {
        var success = JsonHelper.TryDeserialize<TestClass>(null, out var obj);

        await Assert.That(success).IsFalse();
        await Assert.That(obj).IsNull();
    }

    [Test]
    public async Task TryDeserialize_WithWhitespace_ReturnsFalse()
    {
        var success = JsonHelper.TryDeserialize<TestClass>("   ", out var obj);

        await Assert.That(success).IsFalse();
        await Assert.That(obj).IsNull();
    }

    [Test]
    public async Task TryDeserialize_WithEmptyString_ReturnsFalse()
    {
        var success = JsonHelper.TryDeserialize<TestClass>("", out var obj);

        await Assert.That(success).IsFalse();
    }

    [Test]
    public async Task TryDeserialize_WithIncompleteJson_ReturnsFalse()
    {
        var success = JsonHelper.TryDeserialize<TestClass>("{\"name\":\"John\"", out var obj);

        await Assert.That(success).IsFalse();
    }

    [Test]
    public async Task TryDeserialize_WithCustomOptions_UsesOptions()
    {
        var json = "{\"Name\":\"John\",\"Age\":30}";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false };
        var success = JsonHelper.TryDeserialize<TestClass>(json, out var obj, options);

        await Assert.That(success).IsTrue();
    }

    #endregion

    #region IsValidJson Tests

    [Test]
    public async Task IsValidJson_WithValidJson_ReturnsTrue()
    {
        var validJsons = new[]
        {
            "{\"name\":\"John\",\"age\":30}",
            "[1,2,3]",
            "null",
            "true",
            "false",
            "123",
            "\"test\"",
            "[]",
            "{}"
        };

        foreach (var json in validJsons)
        {
            await Assert.That(JsonHelper.IsValidJson(json)).IsTrue();
        }
    }

    [Test]
    public async Task IsValidJson_WithInvalidJson_ReturnsFalse()
    {
        var invalidJsons = new[]
        {
            "invalid json",
            "{name:\"John\"}",
            "[1,2,",
            "{\"name\":}",
            "undefined",
            "'single quotes'"
        };

        foreach (var json in invalidJsons)
        {
            await Assert.That(JsonHelper.IsValidJson(json)).IsFalse();
        }
    }

    [Test]
    public async Task IsValidJson_WithNull_ReturnsFalse()
    {
        await Assert.That(JsonHelper.IsValidJson(null)).IsFalse();
    }

    [Test]
    public async Task IsValidJson_WithEmptyString_ReturnsFalse()
    {
        await Assert.That(JsonHelper.IsValidJson("")).IsFalse();
    }

    [Test]
    public async Task IsValidJson_WithWhitespace_ReturnsFalse()
    {
        await Assert.That(JsonHelper.IsValidJson("   ")).IsFalse();
    }

    #endregion

    #region PrettyPrint Tests

    [Test]
    public async Task PrettyPrint_FormatsJson()
    {
        var json = "{\"name\":\"John\",\"age\":30}";
        var pretty = JsonHelper.PrettyPrint(json);

        await Assert.That(pretty).Contains("\n");
        await Assert.That(pretty).Contains("  ");
        await Assert.That(pretty).Contains("name");
        await Assert.That(pretty).Contains("John");
    }

    [Test]
    public async Task PrettyPrint_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => JsonHelper.PrettyPrint(null!)));
    }

    [Test]
    public async Task PrettyPrint_WithInvalidJson_ThrowsException()
    {
        await Assert.ThrowsAsync<JsonException>(
            async () => await Task.Run(() => JsonHelper.PrettyPrint("invalid json")));
    }

    [Test]
    public async Task PrettyPrint_WithAlreadyPrettyJson_ReformatsConsistently()
    {
        var json = @"{
  ""name"": ""John"",
  ""age"": 30
}";
        var pretty = JsonHelper.PrettyPrint(json);

        await Assert.That(pretty).Contains("\n");
        await Assert.That(pretty).Contains("name");
    }

    [Test]
    public async Task PrettyPrint_WithArray_FormatsCorrectly()
    {
        var json = "[1,2,3,4,5]";
        var pretty = JsonHelper.PrettyPrint(json);

        await Assert.That(pretty).Contains("\n");
    }

    #endregion

    #region Minify Tests

    [Test]
    public async Task Minify_RemovesWhitespace()
    {
        var json = @"{
  ""name"": ""John"",
  ""age"": 30
}";
        var minified = JsonHelper.Minify(json);

        await Assert.That(minified).DoesNotContain("\n");
        await Assert.That(minified).DoesNotContain("  ");
        await Assert.That(minified).Contains("name");
    }

    [Test]
    public async Task Minify_WithNull_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => JsonHelper.Minify(null!)));
    }

    [Test]
    public async Task Minify_WithInvalidJson_ThrowsException()
    {
        await Assert.ThrowsAsync<JsonException>(
            async () => await Task.Run(() => JsonHelper.Minify("invalid json")));
    }

    [Test]
    public async Task Minify_WithAlreadyMinifiedJson_RemainsMinified()
    {
        var json = "{\"name\":\"John\",\"age\":30}";
        var minified = JsonHelper.Minify(json);

        await Assert.That(minified).DoesNotContain("\n");
        await Assert.That(minified).Contains("name");
    }

    [Test]
    public async Task Minify_WithArray_MinifiesCorrectly()
    {
        var json = @"[
  1,
  2,
  3
]";
        var minified = JsonHelper.Minify(json);

        await Assert.That(minified).DoesNotContain("\n");
        await Assert.That(minified).Contains("1");
    }

    #endregion

    #region Clone Tests

    [Test]
    public async Task Clone_CreatesDeepCopy()
    {
        var original = new TestClass { Name = "John", Age = 30 };
        var clone = JsonHelper.Clone(original);

        await Assert.That(clone).IsNotNull();
        await Assert.That(clone.Name).IsEqualTo(original.Name);
        await Assert.That(clone.Age).IsEqualTo(original.Age);
        await Assert.That(ReferenceEquals(clone, original)).IsFalse();
    }

    [Test]
    public async Task Clone_WithNestedObject_CreatesDeepCopy()
    {
        var original = new NestedTestClass
        {
            Value = "parent",
            Nested = new TestClass { Name = "John", Age = 30 }
        };
        var clone = JsonHelper.Clone(original);

        await Assert.That(clone.Nested).IsNotNull();
        await Assert.That(ReferenceEquals(clone.Nested, original.Nested)).IsFalse();
        await Assert.That(clone.Nested!.Name).IsEqualTo("John");
    }

    [Test]
    public async Task Clone_ModifyingClone_DoesNotAffectOriginal()
    {
        var original = new TestClass { Name = "John", Age = 30 };
        var clone = JsonHelper.Clone(original);
        
        clone.Name = "Jane";
        clone.Age = 25;

        await Assert.That(original.Name).IsEqualTo("John");
        await Assert.That(original.Age).IsEqualTo(30);
    }

    [Test]
    public async Task Clone_WithCustomOptions_UsesOptions()
    {
        var original = new TestClass { Name = "John", Age = 30 };
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var clone = JsonHelper.Clone(original, options);

        await Assert.That(clone.Name).IsEqualTo("John");
    }

    #endregion

    #region Integration Tests

    [Test]
    public async Task SerializeDeserialize_RoundTrip_PreservesData()
    {
        var original = new TestClass { Name = "John", Age = 30 };
        var json = JsonHelper.Serialize(original);
        var deserialized = JsonHelper.Deserialize<TestClass>(json);

        await Assert.That(deserialized.Name).IsEqualTo(original.Name);
        await Assert.That(deserialized.Age).IsEqualTo(original.Age);
    }

    [Test]
    public async Task PrettyPrintMinify_RoundTrip_ProducesSameData()
    {
        var json = "{\"name\":\"John\",\"age\":30}";
        var pretty = JsonHelper.PrettyPrint(json);
        var minified = JsonHelper.Minify(pretty);

        // Both should be valid and produce the same object
        var obj1 = JsonHelper.Deserialize<TestClass>(json);
        var obj2 = JsonHelper.Deserialize<TestClass>(minified);

        await Assert.That(obj2.Name).IsEqualTo(obj1.Name);
        await Assert.That(obj2.Age).IsEqualTo(obj1.Age);
    }

    #endregion
}
