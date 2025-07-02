using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions
{
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            [System.ComponentModel.Description("First Value")]
            First,
            Second
        }

        [Test]
        public async Task IsDefined_ShouldReturnTrueForDefinedEnum()
        {
            var result = TestEnum.First.IsDefined();
            await Assert.That(result).IsTrue();
        }

        [Test]
        public async Task IsDefined_ShouldReturnFalseForUndefinedEnum()
        {
            var result = ((TestEnum)999).IsDefined();
            await Assert.That(result).IsFalse();
        }

        [Test]
        public async Task ToDescription_ShouldReturnDescriptionForEnum()
        {
            var description = TestEnum.First.ToDescription();
            await Assert.That(description).IsEqualTo("First Value");
        }

        [Test]
        public async Task ToDescription_ShouldReturnNameForEnumWithoutDescription()
        {
            var description = TestEnum.Second.ToDescription();
            await Assert.That(description).IsEqualTo("Second");
        }

        [Test]
        public async Task GetEnumTypeName_ShouldReturnEnumTypeName()
        {
            var result = TestEnum.First.GetEnumTypeName();
            await Assert.That(result).IsEqualTo(nameof(TestEnum));
        }
    }
}