using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;
using System.ComponentModel;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Format
{
    public class EnumHelperTests
    {
        private enum TestEnum
        {
            [Description("First Value")]
            First,
            Second,
            Third
        }

        [Test]
        public async Task GetEnumName_ShouldReturnEnumName()
        {
            var result = EnumHelper.GetEnumName(TestEnum.First);
            await Assert.That(result).IsEqualTo("First");
        }

        [Test]
        public async Task IsDefined_ShouldReturnFalseForUndefinedEnum()
        {
            var result = EnumHelper.IsDefined((TestEnum)999);
            await Assert.That(result).IsFalse();
        }

        [Test]
        public async Task IsDefined_ShouldReturnTrueForDefinedEnum()
        {
            var result = EnumHelper.IsDefined(TestEnum.First);
            await Assert.That(result).IsTrue();
        }

        [Test]
        public async Task ToDescription_ShouldReturnDescriptionForEnum()
        {
            var description = EnumHelper.ToDescription(TestEnum.First);
            await Assert.That(description).IsEqualTo("First Value");
        }

        [Test]
        public async Task ToDescription_ShouldReturnNameForEnumWithoutDescription()
        {
            var description = EnumHelper.ToDescription(TestEnum.Second);
            await Assert.That(description).IsEqualTo("Second");
        }

        [Test]
        public async Task Parse_ShouldReturnCorrectEnum()
        {
            var result = EnumHelper.Parse<TestEnum>("First");
            await Assert.That(result).IsEqualTo(TestEnum.First);
        }

        [Test]
        public async Task Parse_ShouldThrowExceptionForInvalidValue()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await Task.Run(() => EnumHelper.Parse<TestEnum>("Invalid")));
        }

        [Test]
        public async Task TryParse_ShouldReturnTrueForValidValue()
        {
            var success = EnumHelper.TryParse<TestEnum>("Second", out var result);
            await Assert.That(success).IsTrue();
            await Assert.That(result).IsEqualTo(TestEnum.Second);
        }

         [Test]
        public async Task TryParse_ShouldReturnFalseForInvalidValue()
        {
            var success = EnumHelper.TryParse<TestEnum>("Invalid", out var result);
            await Assert.That(success).IsFalse();
        }

        [Test]
        public async Task GetValues_ShouldReturnAllValues()
        {
            var values = EnumHelper.GetValues<TestEnum>();
            await Assert.That(values).HasCount(3);
            await Assert.That(values).Contains(TestEnum.First);
            await Assert.That(values).Contains(TestEnum.Second);
             await Assert.That(values).Contains(TestEnum.Third);
        }

        [Test]
        public async Task GetNames_ShouldReturnAllNames()
        {
            var names = EnumHelper.GetNames<TestEnum>();
            await Assert.That(names).HasCount(3);
            await Assert.That(names).Contains("First");
            await Assert.That(names).Contains("Second");
            await Assert.That(names).Contains("Third");
        }
    }
}
