using System.Net.Http.Json;
using Reqnroll;

namespace CardValidation.IntegrationTests.Steps;

[Binding]
public sealed class CardValidationStepsDefinitions
{
    private const string Endpoint = "/CardValidation/card/credit/validate";

    private const string ClientKey = "client";

    private readonly ScenarioContext _scenario;

    private object? _payload;
    private HttpResponseMessage? _response;
    private string? _body;

    public CardValidationStepsDefinitions(ScenarioContext scenario)
    {
        _scenario = scenario;
    }

    [Given("card payload is:")]
    public void GivenCardPayloadIs(DataTable table)
    {
        var row = table.Rows[0];

        _payload = new
        {
            Owner = row["Owner"],
            Number = row["Number"],
            Date = row["Date"],
            Cvv = row["Cvv"]
        };
    }

    [When("card validation request is sent")]
    public async Task WhenCardValidationRequestIsSent()
    {
        if (!_scenario.TryGetValue(ClientKey, out HttpClient? client) || client is null)
            throw new InvalidOperationException("HttpClient was not initialized. Check CardValidationHooks.");

        if (_payload is null)
            throw new InvalidOperationException("Payload was not initialized. Check Given step.");

        _response = await client.PostAsJsonAsync(Endpoint, _payload);
        _body = await _response.Content.ReadAsStringAsync();
    }

    [Then("status code is {int}")]
    public void ThenStatusCodeIs(int expected)
    {
        if (_response is null)
            throw new InvalidOperationException("Response was not initialized. Check When step.");

        var actual = (int)_response.StatusCode;

        if (actual != expected)
            throw new Exception($"Expected status {expected}, but was {actual}. Body: {_body}");
    }

    [Then("response includes {string}")]
    public void ThenResponseIncludes(string expected)
    {
        _body ??= string.Empty;

        if (!_body.Contains(expected))
            throw new Exception($"Expected response to include '{expected}', but got: {_body}");
    }
}