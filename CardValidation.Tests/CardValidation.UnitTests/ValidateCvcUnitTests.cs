using FluentAssertions;

namespace CardValidation.UnitTests;

public class ValidateCvcUnitTests : TestBase
{
    // -------- POSITIVE (Valid EC) --------

    [Theory]
    [InlineData("000")]   // EC-V1: 3 digits (min valid)
    [InlineData("9999")]  // EC-V2: 4 digits (max valid)
    public void ValidateCvc_Valid_ReturnsTrue(string cvc)
    {
        BaseService.ValidateCvc(cvc).Should().BeTrue();
    }

    // -------- NEGATIVE (Invalid EC) --------

    [Theory]
    [InlineData("")]      // EC-I1: empty
    [InlineData("1")]     // EC-I1: 1 digit (below min)
    [InlineData("12")]    // EC-I1: 2 digits (below min)
    [InlineData("12345")] // EC-I2: 5 digits (above max)
    [InlineData("12a")]   // EC-I3: non-numeric
    [InlineData("abc")]   // EC-I3: non-numeric
    [InlineData(" 123")]  // EC-I4: leading space
    [InlineData("123 ")]  // EC-I4: trailing space
    [InlineData("12 3")]  // EC-I4: inner space
    [InlineData("12-3")]  // EC-I4: hyphen
    public void ValidateCvc_Invalid_ReturnsFalse(string cvc)
    {
        BaseService.ValidateCvc(cvc).Should().BeFalse();
    }
}

