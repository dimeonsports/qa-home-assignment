using FluentAssertions;

namespace CardValidation.UnitTests;

public class IssueDateUnitTests : TestBase
{
    // -------- POSITIVE (Valid EC) --------

    [Theory]
    [InlineData("01/99")]     // EC-V1: MM/YY, month boundary (01), future (2099)
    [InlineData("12/2099")]   // EC-V2: MM/YYYY, month boundary (12), future
    public void ValidateIssueDate_FutureDates_ReturnsTrue(string issueDate)
    {
        BaseService.ValidateIssueDate(issueDate).Should().BeTrue();
    }

    // -------- NEGATIVE (Invalid EC) --------

    [Theory]
    [InlineData("00/24")]     // EC-I1: invalid month (00)
    [InlineData("13/25")]     // EC-I1: invalid month (13)
    [InlineData("1/24")]      // EC-I2: month must be 2 digits
    [InlineData("01-24")]     // EC-I2: wrong separator
    [InlineData("0124")]      // EC-I2: missing separator
    [InlineData("01/2")]      // EC-I2: invalid year length
    [InlineData("01/")]       // EC-I2: incomplete
    [InlineData("ab/cd")]     // EC-I3: non-numeric
    [InlineData("01/2a")]     // EC-I3: non-numeric year
    [InlineData("01/20")]     // EC-I4: valid format, but past (MM/YY)
    [InlineData("12/1999")]   // EC-I4: valid format, but past (MM/YYYY)
    public void ValidateIssueDate_InvalidOrPast_ReturnsFalse(string issueDate)
    {
        BaseService.ValidateIssueDate(issueDate).Should().BeFalse();
    }
}
