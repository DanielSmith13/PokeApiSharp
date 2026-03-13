using System.Net;
using System.Text;
using PokeApiSharp;
using PokeApiSharp.PokeApi.Pokemon;
using Unit.Utils;

namespace Unit.Clients;

public class PokeApiClientTests
{
    private const string BaseAddress = "https://pokeapi.co/api/v2/";
    private const string ResourcePath = "pokemon";
    private static Uri GetExpectedUrl(int i) => new($"{BaseAddress}{ResourcePath}/{i}");
    private static Uri GetExpectedUrl(string name) => new($"{BaseAddress}{ResourcePath}/{name}");
    private static Uri GetExpectedListUrl(int? limit = 20, int? offset = 0) => 
        new($"{BaseAddress}{ResourcePath}?limit={limit}&offset={offset}");
    
    private const string ResourceJson = "{\"id\":263,\"name\":\"zigzagoon\"}";
    private const string ListJson = "{\"count\":1,\"next\":null,\"previous\":null,\"results\":[{\"name\":\"zigzagoon\",\"url\":\"https://pokeapi.co/api/v2/pokemon/263/\"}]}";
    
    [Fact]
    public void Constructor_SetsBaseAddress_WhenNull()
    {
        var httpClient = new HttpClient();
        var pokeApiClient = new PokeApiClient(httpClient);

        Assert.NotNull(pokeApiClient);
        Assert.NotNull(httpClient.BaseAddress);
        Assert.Equal(new Uri("https://pokeapi.co/api/v2/"), httpClient.BaseAddress);
    }
    
    [Fact]
    public void Constructor_DoesNotSetBaseAddress_WhenNotNull()
    {
        var baseAddress = new Uri("https://pokeapi.co/api/v2/test");
        var httpClient = new HttpClient { BaseAddress = baseAddress };
        var pokeApiClient = new PokeApiClient(httpClient);
        
        Assert.NotNull(pokeApiClient);
        Assert.NotNull(httpClient.BaseAddress);
        Assert.Equal(baseAddress, httpClient.BaseAddress);
    }
    
    [Theory]
    [InlineData(263)]
    public async Task GetAsync_ById_UsesExpectedUrl(int id)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };
        
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        
        var sut = new PokeApiClient(httpClient);
        
        var result = await sut.GetAsync<Pokemon>(id);
        
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("zigzagoon", result.Name);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedUrl(id), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }
    

    [Theory]
    [InlineData("zigzagoon")]
    [InlineData("  ZiGzAgOoN  ")]
    public async Task GetAsync_ByName_UsesExpectedUrl(string name)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };
        
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        
        var sut = new PokeApiClient(httpClient);
        
        var result = await sut.GetAsync<Pokemon>(name);
        
        Assert.NotNull(result);
        Assert.Equal(263, result.Id);
        Assert.Equal("zigzagoon", result.Name);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedUrl("zigzagoon"), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData(100, null)]
    [InlineData(null, 100)]
    [InlineData(100, 100)]
    public async Task ListAsync_UsesProvidedParams_AndDefaultsTo20LimitAnd0Offset(int? limit, int? offset)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ListJson, Encoding.UTF8, "application/json")
        };
        
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        
        var sut = new PokeApiClient(httpClient);
        
        
        var result = await sut.ListAsync<Pokemon>(limit, offset);
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Null(result.Next);
        Assert.Null(result.Previous);
        Assert.NotNull(result.Results);
        Assert.Equal("https://pokeapi.co/api/v2/pokemon/263/", result.Results[0].Url);
        Assert.Equal("zigzagoon", result.Results[0].Name);
        

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedListUrl(limit, offset), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }
}