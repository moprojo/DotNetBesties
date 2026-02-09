using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Extensions;

namespace DotNetBesties.Helpers.Tests.Extensions;

public class EnumExtensionsTests
{
    private enum TestEnum
    {
        [System.ComponentModel.Description("First Value")]
        First = 1,
        Second = 2,
        Third = 3
    }

    [Flags]
    private enum TestFlagsEnum
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4,
        All = Read | Write | Execute
    }

    #region GetEnumName Tests

    [Test]
    public async Task GetEnumName_ReturnsEnumName()
    {
        var result = TestEnum.First.GetEnumName();
        await Assert.That(result).IsEqualTo("First");
    }

    [Test]
    public async Task GetEnumName_WithUndefinedValue_ReturnsNull()
    {
        var result = ((TestEnum)999).GetEnumName();
        await Assert.That(result).IsNull();
    }

    #endregion

    #region IsDefined Tests

    [Test]
    public async Task IsDefined_WithDefinedValue_ReturnsTrue()
    {
        var result = TestEnum.First.IsDefined();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsDefined_WithUndefinedValue_ReturnsFalse()
    {
        var result = ((TestEnum)999).IsDefined();
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region ToDescription Tests

    [Test]
    public async Task ToDescription_WithDescription_ReturnsDescription()
    {
        var description = TestEnum.First.ToDescription();
        await Assert.That(description).IsEqualTo("First Value");
    }

    [Test]
    public async Task ToDescription_WithoutDescription_ReturnsName()
    {
        var description = TestEnum.Second.ToDescription();
        await Assert.That(description).IsEqualTo("Second");
    }

    [Test]
    public async Task ToDescription_WithUndefinedValue_ReturnsValueString()
    {
        var description = ((TestEnum)999).ToDescription();
        await Assert.That(description).IsEqualTo("999");
    }

    #endregion

    #region ToInt Tests

    [Test]
    public async Task ToInt_ReturnsCorrectValue()
    {
        var result = TestEnum.First.ToInt();
        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ToInt_WithSecondValue_ReturnsCorrectValue()
    {
        var result = TestEnum.Second.ToInt();
        await Assert.That(result).IsEqualTo(2);
    }

    #endregion

    #region HasFlagFast Tests

    [Test]
    public async Task HasFlagFast_WithSetFlag_ReturnsTrue()
    {
        var value = TestFlagsEnum.Read | TestFlagsEnum.Write;
        await Assert.That(value.HasFlagFast(TestFlagsEnum.Read)).IsTrue();
    }

    [Test]
    public async Task HasFlagFast_WithUnsetFlag_ReturnsFalse()
    {
        var value = TestFlagsEnum.Read | TestFlagsEnum.Write;
        await Assert.That(value.HasFlagFast(TestFlagsEnum.Execute)).IsFalse();
    }

    [Test]
    public async Task HasFlagFast_WithAllFlags_ReturnsTrue()
    {
        var value = TestFlagsEnum.All;
        await Assert.That(value.HasFlagFast(TestFlagsEnum.Read)).IsTrue();
        await Assert.That(value.HasFlagFast(TestFlagsEnum.Write)).IsTrue();
        await Assert.That(value.HasFlagFast(TestFlagsEnum.Execute)).IsTrue();
    }

    #endregion
}