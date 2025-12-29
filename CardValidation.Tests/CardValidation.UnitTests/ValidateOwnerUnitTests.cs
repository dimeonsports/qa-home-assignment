using FluentAssertions;

namespace CardValidation.UnitTests;

public class ValidateOwnerUnitTests : TestBase
{
    // -------- POSITIVE (Valid EC) --------

    [Theory]
    [InlineData("Jim")]                 // EC-V1: 1 word (letters only)
    [InlineData("Jim Carrey")]          // EC-V2: 2 words (single spaces)
    [InlineData("Jim Carrey Man")]      // EC-V3: 3 words (max allowed by {1,3})
    [InlineData("Jim Carrey ")]         // EC-V4: trailing space allowed by regex ( ? )
    public void ValidateOwner_Valid_ReturnsTrue(string owner)
    {
        BaseService.ValidateOwner(owner).Should().BeTrue();
    }

    // -------- NEGATIVE (Invalid EC) --------

    [Theory]
    [InlineData("")]                        // EC-I1: empty string (0 words)
    [InlineData(" ")]                       // EC-I1: whitespace only (no letters)
    [InlineData(" Jim Carrey")]             // EC-I2: leading space (fails ^)
    [InlineData("Jim99")]                   // EC-I3: contains digits
    [InlineData("Jim_Carrey")]              // EC-I4: contains underscore
    [InlineData("Jim-Carrey")]              // EC-I4: contains hyphen
    [InlineData("Jim'Carrey")]              // EC-I4: contains apostrophe
    [InlineData("Jim`Carrey")]              // EC-I4: contains backtick
    [InlineData("Jim Carrey Cable Man")]    // EC-I5: 4 words (exceeds {1,3})
    public void ValidateOwner_Invalid_ReturnsFalse(string owner)
    {
        BaseService.ValidateOwner(owner).Should().BeFalse();
    }
}
