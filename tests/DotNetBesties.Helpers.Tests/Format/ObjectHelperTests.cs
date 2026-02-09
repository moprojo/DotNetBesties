using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class ObjectHelperTests
{
    #region As Tests

    [Test]
    public async Task As_WithCorrectType_ReturnsTypedObject()
    {
        object obj = "test";
        var result = ObjectHelper.As<string>(obj);
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task As_WithIncorrectType_ReturnsNull()
    {
        object obj = 123;
        var result = ObjectHelper.As<string>(obj);
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task As_WithNull_ReturnsNull()
    {
        object? obj = null;
        var result = ObjectHelper.As<string>(obj);
        await Assert.That(result).IsNull();
    }

    #endregion

    #region Cast Tests

    [Test]
    public async Task Cast_WithCorrectType_ReturnsTypedValue()
    {
        object obj = 123;
        var result = ObjectHelper.Cast<int>(obj);
        await Assert.That(result).IsEqualTo(123);
    }

    [Test]
    public async Task Cast_WithIncorrectType_ThrowsException()
    {
        object obj = "test";
        await Assert.ThrowsAsync<InvalidCastException>(async () => await Task.Run(() => ObjectHelper.Cast<int>(obj)));
    }

    #endregion

    #region DeepClone Tests

    [Test]
    public async Task DeepClone_ClonesObjectCorrectly()
    {
        var original = new TestClass { Value = "test" };
        var clone = ObjectHelper.DeepClone(original);

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
        var clone = ObjectHelper.DeepClone(original);

        await Assert.That(clone.Nested).IsNotNull();
        await Assert.That(clone.Nested!.Value).IsEqualTo("child");
        await Assert.That(ReferenceEquals(clone.Nested, original.Nested)).IsFalse();
    }

    [Test]
    public async Task DeepClone_WithNull_ThrowsException()
    {
        TestClass? obj = null;
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Task.Run(() => ObjectHelper.DeepClone(obj!)));
    }

    #endregion

    #region IsNull Tests

    [Test]
    public async Task IsNull_WithNull_ReturnsTrue()
    {
        object? obj = null;
        var result = ObjectHelper.IsNull(obj);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNull_WithNonNull_ReturnsFalse()
    {
        object obj = new object();
        var result = ObjectHelper.IsNull(obj);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region IsNotNull Tests

    [Test]
    public async Task IsNotNull_WithNonNull_ReturnsTrue()
    {
        object obj = new object();
        var result = ObjectHelper.IsNotNull(obj);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNotNull_WithNull_ReturnsFalse()
    {
        object? obj = null;
        var result = ObjectHelper.IsNotNull(obj);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region IsPrimitive Tests

    [Test]
    public async Task IsPrimitive_WithPrimitiveTypes_ReturnsTrue()
    {
        var primitives = new object[] { 123, 123L, 123.45f, 123.45d, true, 'c', (byte)1, (short)1 };

        foreach (var primitive in primitives)
        {
            await Assert.That(ObjectHelper.IsPrimitive(primitive)).IsTrue();
        }
    }

    [Test]
    public async Task IsPrimitive_WithString_ReturnsTrue()
    {
        var result = ObjectHelper.IsPrimitive("test");
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsPrimitive_WithDecimal_ReturnsTrue()
    {
        var result = ObjectHelper.IsPrimitive(123.45m);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsPrimitive_WithNonPrimitiveTypes_ReturnsFalse()
    {
        var nonPrimitives = new object[] { new object(), new TestClass(), DateTime.Now };

        foreach (var nonPrimitive in nonPrimitives)
        {
            await Assert.That(ObjectHelper.IsPrimitive(nonPrimitive)).IsFalse();
        }
    }

    [Test]
    public async Task IsPrimitive_WithNull_ReturnsFalse()
    {
        object? obj = null;
        var result = ObjectHelper.IsPrimitive(obj);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region Test Classes

    private class TestClass
    {
        public string Value { get; set; } = string.Empty;
    }

    private class TestClassWithNested
    {
        public string Value { get; set; } = string.Empty;
        public TestClass? Nested { get; set; }
    }

    #endregion
}
