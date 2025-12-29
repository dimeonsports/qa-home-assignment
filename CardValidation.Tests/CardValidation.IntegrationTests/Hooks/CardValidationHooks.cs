using CardValidation.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;

namespace CardValidation.IntegrationTests.Hooks;

[Binding]
public sealed class CardValidationHooks
{
    private const string FactoryKey = "factory";
    private const string ClientKey = "client";

    private readonly ScenarioContext _scenario;

    public CardValidationHooks(ScenarioContext scenario)
    {
        _scenario = scenario;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var factory = new WebApplicationFactory<CardValidationController>();

        var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        _scenario[FactoryKey] = factory;
        _scenario[ClientKey] = client;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenario.TryGetValue(ClientKey, out HttpClient? client))
            client?.Dispose();

        if (_scenario.TryGetValue(FactoryKey, out WebApplicationFactory<CardValidationController>? factory))
            factory?.Dispose();
    }
}
