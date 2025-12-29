using FluentAssertions;

namespace CardValidation.UnitTests;

public class ValidateNumberUnitTests : TestBase
{
    // -------- VISA --------
    // Visa regex: ^4[0-9]{12}(?:[0-9]{3})?$
    // Valid: 13 or 16 digits, starts with 4, digits only

    [Theory]
    [InlineData("4111111111111")]        // VISA-EC-V1: 13 digits (min valid length)
    [InlineData("4111111111111111")]     // VISA-EC-V2: 16 digits (max valid length)
    public void ValidateNumber_Visa_Valid_ReturnsTrue(string number)
    {
        BaseService.ValidateNumber(number).Should().BeTrue();
    }

    [Theory]
    [InlineData("411111111111")]            // VISA-EC-I1: 12 digits (below min)
    [InlineData("41111111111111")]          // VISA-EC-I1: 14 digits (invalid length)
    [InlineData("411111111111111")]         // VISA-EC-I1: 15 digits (invalid length)
    [InlineData("41111111111111111")]       // VISA-EC-I1: 17 digits (above max)
    [InlineData("6111111111111111")]        // VISA-EC-I2: unsupported prefix (not Visa/MC/AmEx)
    [InlineData("411111111111111a")]        // VISA-EC-I3: non-numeric character
    [InlineData("4111-1111-1111-1111")]     // VISA-EC-I4: contains hyphens
    [InlineData("4111 1111 1111 1111")]     // VISA-EC-I4: contains spaces
    public void ValidateNumber_Visa_Invalid_ReturnsFalse(string number)
    {
        BaseService.ValidateNumber(number).Should().BeFalse();
    }

    // -------- MASTERCARD --------
    // MasterCard regex: supports 51-55 and 2221-2720, always 16 digits

    [Theory]
    [InlineData("5100000000000000")]     // MC-EC-V1: 51xx (classic range)
    [InlineData("5500000000000000")]     // MC-EC-V1: 55xx (classic range upper)
    [InlineData("2221000000000000")]     // MC-EC-V2: 2221 (new range lower boundary)
    [InlineData("2720990000000000")]     // MC-EC-V2: 2720 (new range upper boundary)
    public void ValidateNumber_MasterCard_Valid_ReturnsTrue(string number)
    {
        BaseService.ValidateNumber(number).Should().BeTrue();
    }

    [Theory]
    [InlineData("5000000000000000")]     // MC-EC-I1: 50xx (below classic range)
    [InlineData("5600000000000000")]     // MC-EC-I1: 56xx (above classic range)
    [InlineData("2220000000000000")]     // MC-EC-I2: 2220 (below new range)
    [InlineData("2721000000000000")]     // MC-EC-I2: 2721 (above new range)
    [InlineData("222100000000000")]      // MC-EC-I3: wrong length (15 digits)
    [InlineData("22210000000000000")]    // MC-EC-I3: wrong length (17 digits)
    public void ValidateNumber_MasterCard_Invalid_ReturnsFalse(string number)
    {
        BaseService.ValidateNumber(number).Should().BeFalse();
    }

    // -------- AMEX --------
    // AmEx regex: ^3[47][0-9]{13}$ (15 digits total)

    [Theory]
    [InlineData("340000000000000")]      // AMEX-EC-V1: 34 prefix (valid)
    [InlineData("370000000000000")]      // AMEX-EC-V2: 37 prefix (valid)
    public void ValidateNumber_Amex_Valid_ReturnsTrue(string number)
    {
        BaseService.ValidateNumber(number).Should().BeTrue();
    }

    [Theory]
    [InlineData("350000000000000")]     // AMEX-EC-I1: wrong prefix (35)
    [InlineData("360000000000000")]     // AMEX-EC-I1: wrong prefix (36)
    [InlineData("34000000000000")]      // AMEX-EC-I2: wrong length (14 digits)
    [InlineData("3400000000000000")]    // AMEX-EC-I2: wrong length (16 digits)
    [InlineData("34000000000000a")]     // AMEX-EC-I3: non-numeric character
    [InlineData("3400 00000 00000")]    // AMEX-EC-I4: contains spaces
    [InlineData("3400-00000-00000")]    // AMEX-EC-I4: contains hyphens
    public void ValidateNumber_Amex_Invalid_ReturnsFalse(string number)
    {
        BaseService.ValidateNumber(number).Should().BeFalse();
    }

    // -------- GENERAL --------

    [Theory]
    [InlineData("")]                    // GEN-EC-I1: empty
    [InlineData("   ")]                 // GEN-EC-I1: whitespace only
    [InlineData("37")]                  // GEN-EC-I2: too short
    [InlineData("0000000000000000")]    // GEN-EC-I3: unknown pattern (digits only)
    public void ValidateNumber_General_Invalid_ReturnsFalse(string number)
    {
        BaseService.ValidateNumber(number).Should().BeFalse();
    }

    // -------- SUSPICIOUS (document current regex behavior) --------

    [Theory]
    [InlineData("4111111111111111\n")]  // SUSP-EC-V1: trailing newline is ignored by $ anchor in .NET regex
    public void ValidateNumber_TrailingNewline_IsTreatedAsValid(string number)
    {
        BaseService.ValidateNumber(number).Should().BeTrue();
    }
}
