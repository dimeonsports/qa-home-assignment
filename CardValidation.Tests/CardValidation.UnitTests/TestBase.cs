using CardValidation.Core.Services;

namespace CardValidation.UnitTests;

public abstract class TestBase
{
    protected CardValidationService BaseService { get; }

    protected TestBase()
    {
        BaseService = new CardValidationService();
    }
}
