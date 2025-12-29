using CardValidation.Core.Enums;
using FluentAssertions;

namespace CardValidation.UnitTests;

public class PaymentSystemTypeUnitTests : TestBase
{
    // -------- POSITIVE (Valid EC) --------

    [Theory]
    [InlineData("4111111111111", PaymentSystemType.Visa)]               // EC-V1: Visa 13 digits
    [InlineData("4111111111111111", PaymentSystemType.Visa)]            // EC-V1: Visa 16 digits
    [InlineData("5555555555554444", PaymentSystemType.MasterCard)]      // EC-V2: MasterCard 55xx
    [InlineData("2221000000000000", PaymentSystemType.MasterCard)]      // EC-V2: MasterCard lower boundary 2221
    [InlineData("2720990000000000", PaymentSystemType.MasterCard)]      // EC-V2: MasterCard upper boundary 2720
    [InlineData("378282246310005", PaymentSystemType.AmericanExpress)]  // EC-V3: AmEx 37
    [InlineData("340000000000000", PaymentSystemType.AmericanExpress)]  // EC-V3: AmEx 34
    public void GetPaymentSystemType_ValidNumbers_ReturnExpectedType(string number, PaymentSystemType expected)
    {
        BaseService.GetPaymentSystemType(number).Should().Be(expected);
    }

    // -------- NEGATIVE (Invalid EC) --------

    [Theory]
    [InlineData("9999999999999999")]        // EC-I1: unknown BIN, 16 digits
    [InlineData("6111111111111111")]        // EC-I1: unsupported prefix
    [InlineData("411111111111111")]         // EC-I2: Visa-like, wrong length (15)
    [InlineData("123")]                     // EC-I2: too short
    [InlineData("411111111111111a")]        // EC-I3: non-numeric
    [InlineData("4111 1111 1111 1111")]     // EC-I3: contains spaces
    [InlineData("4111-1111-1111-1111")]     // EC-I3: contains hyphens
    [InlineData("")]                        // EC-I4: empty
    [InlineData("   ")]                     // EC-I4: whitespace only
    public void GetPaymentSystemType_InvalidNumbers_ThrowNotImplementedException(string number)
    {
        Action act = () => BaseService.GetPaymentSystemType(number);
        act.Should().Throw<NotImplementedException>();
    }
}