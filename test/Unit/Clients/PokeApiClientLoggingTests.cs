using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using PokeApiSharp;
using Unit.Utils;

namespace Unit.Clients;

public class PokeApiClientLoggingTests
{
    private const string BaseAddress = "https://pokeapi.co/api/v2/";
    private const string jsonWithExtra = "{\"id\":1,\"name\":\"bulbasaur\",\"extra_prop\":\"value\"}";
    private const string jsonValid = "{\"id\":1,\"name\":\"bulbasaur\"}";

    [Fact]
    public async Task GetAsync_LogsWarning_WhenUnmappedPropertiesPresent()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonWithExtra, Encoding.UTF8, "application/json")
        };
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var logger = new MockLogger<PokeApiClient>();
        var sut = new PokeApiClient(httpClient, logger: logger);
        
        await sut.GetAsync<Pokemon>(1);
        
        var warning = Assert.Single(logger.LoggedMessages);
        Assert.Equal(LogLevel.Warning, warning.Level);
        Assert.Contains("Unmapped properties found for resource Pokemon: extra_prop", warning.Message);
    }

    [Fact]
    public async Task GetAsync_DoesNotLog_WhenNoUnmappedProperties()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonValid, Encoding.UTF8, "application/json")
        };
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var logger = new MockLogger<PokeApiClient>();
        var sut = new PokeApiClient(httpClient, logger: logger);
        
        await sut.GetAsync<Pokemon>(1);
        
        Assert.Empty(logger.LoggedMessages);
    }

    [Fact]
    public async Task GetAsync_DoesNotCrash_WhenLoggerIsNull()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonWithExtra, Encoding.UTF8, "application/json")
        };
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var sut = new PokeApiClient(httpClient, logger: null);

        var exception = await Record.ExceptionAsync(() => sut.GetAsync<Pokemon>(1));
        
        Assert.Null(exception);
    }

    [Fact]
    public async Task ListAsync_LogsWarning_WhenUnmappedPropertiesInList()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonWithExtra, Encoding.UTF8, "application/json")
        };
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var logger = new MockLogger<PokeApiClient>();
        var sut = new PokeApiClient(httpClient, logger: logger);
        
        await sut.ListAsync<Pokemon>();
        
        var warning = Assert.Single(logger.LoggedMessages);
        Assert.Equal(LogLevel.Warning, warning.Level);
        Assert.Contains("Unmapped properties found for resource NamedApiResourceList`1: extra_in_list", warning.Message);
    }
}
