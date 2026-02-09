using System;
using System.Collections.Generic;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

public class ObjectExtensionsTests
{
    #region As Tests

    [Test]
    public async Task As_WithCorrectType_ReturnsTypedObject()
    {
        object obj = "test";
        var result = obj.As<string>();
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task As_WithIncorrectType_ReturnsNull()
    {
        object obj = 123;
        var result = obj.As<string>();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task As_WithNull_ReturnsNull()
    {
        object? obj = null;
        var result = obj.As<string>();
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task As_WithDerivedType_ReturnsTypedObject()
    {
        object obj = new DerivedTestClass { Value = "test", Extra = "extra" };
        var result = obj.As<TestClass>();
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value).IsEqualTo("test");
    }

    #endregion

    #region Cast Tests

    [Test]
    public async Task Cast_WithCorrectType_ReturnsTypedValue()
    {
        object obj = 123;
        var result = obj.Cast<int>();
        await Assert.That(result).IsEqualTo(123);
    }

    [Test]
    public async Task Cast_WithIncorrectType_ThrowsException()
    {
        object obj = "test";
        await Assert.ThrowsAsync<InvalidCastException>(async () => await Task.Run(() => obj.Cast<int>()));
    }

    [Test]
    public async Task Cast_WithNull_ThrowsNullReferenceException()
    {
        object? obj = null;
        await Assert.ThrowsAsync<NullReferenceException>(
            async () => await Task.Run(() => obj!.Cast<int>()));
    }

    [Test]
    public async Task Cast_WithBoxedValue_Works()
    {
        object obj = (double)3.14;
        var result = obj.Cast<double>();
        await Assert.That(result).IsEqualTo(3.14);
    }

    #endregion

    #region DeepClone Tests

    [Test]
    public async Task DeepClone_ClonesObjectCorrectly()
    {
        var original = new TestClass { Value = "test" };
        var clone = original.DeepClone();

        await Assert.That(clone).IsNotNull();
        await Assert.That(clone.Value).IsEqualTo(original.Value);
        await Assert.That(ReferenceEquals(clone, original)).IsFalse();
    }

    [Test]
    public async Task DeepClone_WithNestedObject_ClonesDeep()
    {
        var original = new TestClassWithNested
        {
            Value = "parent",
            Nested = new TestClass { Value = "child" }
        };
        var clone = original.DeepClone();

        await Assert.That(clone.Nested).IsNotNull();
        await Assert.That(clone.Nested!.Value).IsEqualTo("child");
        await Assert.That(ReferenceEquals(clone.Nested, original.Nested)).IsFalse();
    }

    [Test]
    public async Task DeepClone_WithNull_ThrowsException()
    {
        TestClass? obj = null;
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Task.Run(() => obj!.DeepClone()));
    }

    [Test]
    public async Task DeepClone_WithComplexNestedStructure_ClonesCorrectly()
    {
        var original = new TestClassWithNested
        {
            Value = "root",
            Nested = new TestClass
            {
                Value = "child"
            }
        };

        var clone = original.DeepClone();
        clone.Value = "modified_root";
        clone.Nested!.Value = "modified_child";

        await Assert.That(original.Value).IsEqualTo("root");
        await Assert.That(original.Nested.Value).IsEqualTo("child");
    }

    [Test]
    public async Task DeepClone_WithCollections_ClonesCorrectly()
    {
        var original = new TestClassWithCollection
        {
            Values = new List<string> { "a", "b", "c" }
        };
        var clone = original.DeepClone();

        clone.Values.Add("d");

        await Assert.That(original.Values.Count).IsEqualTo(3);
        await Assert.That(clone.Values.Count).IsEqualTo(4);
        await Assert.That(ReferenceEquals(original.Values, clone.Values)).IsFalse();
    }

    #endregion

    #region IsNull Tests

    [Test]
    public async Task IsNull_WithNull_ReturnsTrue()
    {
        object? obj = null;
        var result = obj.IsNull();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNull_WithNonNull_ReturnsFalse()
    {
        object obj = new object();
        var result = obj.IsNull();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNull_WithNullString_ReturnsTrue()
    {
        string? str = null;
        await Assert.That(str.IsNull()).IsTrue();
    }

    [Test]
    public async Task IsNull_WithEmptyString_ReturnsFalse()
    {
        string str = "";
        await Assert.That(str.IsNull()).IsFalse();
    }

    #endregion

    #region IsNotNull Tests

    [Test]
    public async Task IsNotNull_WithNonNull_ReturnsTrue()
    {
        object obj = new object();
        var result = obj.IsNotNull();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNotNull_WithNull_ReturnsFalse()
    {
        object? obj = null;
        var result = obj.IsNotNull();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsNotNull_WithEmptyString_ReturnsTrue()
    {
        string str = "";
        await Assert.That(str.IsNotNull()).IsTrue();
    }

    #endregion

    #region IsPrimitive Tests

    [Test]
    public async Task IsPrimitive_WithPrimitiveTypes_ReturnsTrue()
    {
        var primitives = new object[] { 123, 123L, 123.45f, 123.45d, true, 'c', (byte)1, (short)1 };

        foreach (var primitive in primitives)
        {
            await Assert.That(primitive.IsPrimitive()).IsTrue();
        }
    }

    [Test]
    public async Task IsPrimitive_WithString_ReturnsTrue()
    {
        var result = "test".IsPrimitive();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsPrimitive_WithDecimal_ReturnsTrue()
    {
        var result = 123.45m.IsPrimitive();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsPrimitive_WithNonPrimitiveTypes_ReturnsFalse()
    {
        var nonPrimitives = new object[] { new object(), new TestClass(), DateTime.Now };

        foreach (var nonPrimitive in nonPrimitives)
        {
            await Assert.That(nonPrimitive.IsPrimitive()).IsFalse();
        }
    }

    [Test]
    public async Task IsPrimitive_WithNull_ReturnsFalse()
    {
        object? obj = null;
        var result = obj.IsPrimitive();
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IsPrimitive_WithGuid_ReturnsFalse()
    {
        var guid = Guid.NewGuid();
        await Assert.That(guid.IsPrimitive()).IsFalse();
    }

    [Test]
    public async Task IsPrimitive_WithEnum_ReturnsFalse()
    {
        var enumValue = DayOfWeek.Monday;
        await Assert.That(enumValue.IsPrimitive()).IsFalse();
    }

    [Test]
    public async Task IsPrimitive_WithNullableInt_ReturnsTrue()
    {
        int? value = 42;
        // Nullable<int> when boxed becomes int, which is primitive
        object boxedValue = value;
        await Assert.That(boxedValue.IsPrimitive()).IsTrue();
    }

    #endregion

    #region Test Classes

    private class TestClass
    {
        public string Value { get; set; } = string.Empty;
    }

    private class DerivedTestClass : TestClass
    {
        public string Extra { get; set; } = string.Empty;
    }

    private class TestClassWithNested
    {
        public string Value { get; set; } = string.Empty;
        public TestClass? Nested { get; set; }
    }

    private class TestClassWithCollection
    {
        public List<string> Values { get; set; } = new();
    }

    #endregion
}