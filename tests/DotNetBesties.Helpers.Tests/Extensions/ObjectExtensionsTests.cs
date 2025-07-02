using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Test]
        public async Task IsNull_ShouldReturnTrueForNullObject()
        {
            object? obj = null;
            var result = obj.IsNull();
            await Assert.That(result).IsTrue();
        }

        [Test]
        public async Task IsNotNull_ShouldReturnTrueForNonNullObject()
        {
            object obj = new object();
            var result = obj.IsNotNull();
            await Assert.That(result).IsTrue();
        }

        [Test]
        public async Task As_ShouldReturnCorrectType()
        {
            object obj = "test";
            var result = obj.As<string>();
            await Assert.That(result).IsEqualTo("test");
        }

        [Test]
        public async Task As_ShouldReturnNullForIncorrectType()
        {
            object obj = "test";
            var result = obj.As<string>();
            await Assert.That(result).IsNull();
        }

        [Test]
        public async Task Cast_ShouldReturnCorrectType()
        {
            object obj = 123;
            var result = obj.Cast<int>();
            await Assert.That(result).IsEqualTo(123);
        }

        [Test]
        public async Task Cast_ShouldThrowExceptionForIncorrectType()
        {
            object obj = "test";
            await Assert.ThrowsAsync<InvalidCastException>(async () => await Task.Run(() => obj.Cast<int>()));
        }

        [Test]
        public async Task DeepClone_ShouldCloneObjectCorrectly()
        {
            var original = new TestClass { Value = "test" };
            var clone = original.DeepClone();

            await Assert.That(clone).IsNotNull();
            await Assert.That(clone.Value).IsEqualTo(original.Value);
            await Assert.That(clone.Equals(original)).IsFalse();
        }

        [Test]
        public async Task IsPrimitive_ShouldReturnTrueForPrimitiveTypes()
        {
            var primitives = new object[] { 123, "string", 123.45m, true };

            foreach (var primitive in primitives)
            {
                await Assert.That(primitive.IsPrimitive()).IsTrue();
            }
        }

        [Test]
        public async Task IsPrimitive_ShouldReturnFalseForNonPrimitiveTypes()
        {
            var nonPrimitives = new object[] { new object(), new TestClass() };

            foreach (var nonPrimitive in nonPrimitives)
            {
                await Assert.That(nonPrimitive.IsPrimitive()).IsFalse();
            }
        }

        private class TestClass
        {
            public string Value { get; set; } = string.Empty;
        }
    }
}