using PokeApiSharp;
using WireMock.Server;

namespace Integration;

public class PokemonClientTests : IDisposable
{
    private readonly WireMockServer _server = WireMockServer.Start();
    
    [Fact]
    public async Task GetById_ReturnsPokemonDto()
    {
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Fixtures", "pokemon-bulbasaur.json");
        var fixtureJson = await File.ReadAllTextAsync(fixturePath);
        
        _server.Given(
            WireMock.RequestBuilders.Request.Create()
                .WithPath("/api/v2/pokemon/1")
                .UsingGet()
        )
        .RespondWith(
            WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(fixtureJson)
        );
        
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_server.Urls[0] + "/api/v2/");
        
        var pokeApiClient = new PokeApiClient(httpClient);
        var pokemon = await pokeApiClient.GetAsync<Pokemon>(1);
        
        Assert.NotNull(pokemon);
        Assert.Equal("bulbasaur", pokemon.Name);
        Assert.Equal(1, pokemon.Id);
    }
    
    [Fact]
    public async Task GetByName_ReturnsPokemonDto()
    {
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Fixtures", "pokemon-bulbasaur.json");
        var fixtureJson = await File.ReadAllTextAsync(fixturePath);
        
        _server.Given(
                WireMock.RequestBuilders.Request.Create()
                    .WithPath("/api/v2/pokemon/bulbasaur")
                    .UsingGet()
            )
            .RespondWith(
                WireMock.ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(fixtureJson)
            );
        
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_server.Urls[0] + "/api/v2/");
        
        var pokeApiClient = new PokeApiClient(httpClient);
        var pokemon = await pokeApiClient.GetAsync<Pokemon>("bulbasaur");
        
        Assert.NotNull(pokemon);
        Assert.Equal("bulbasaur", pokemon.Name);
        Assert.Equal(1, pokemon.Id);
    }
    
    [Fact]
    public async Task List_ReturnsAllPokemonDtos()
    {
        NamedApiResourceList<Pokemon> dto = new(
            Count: 1350,
            Next: null,
            Previous: null,
            Results: Enumerable.Range(1, 20)
                .Select(i => new NamedApiResource<Pokemon>($"pokemon-{i}", i.ToString()))
                .ToList()
        );
        
        _server.Given(
                    WireMock.RequestBuilders.Request.Create()
                        .WithPath("/api/v2/pokemon")
                        .UsingGet()
                )
                .RespondWith(
                    WireMock.ResponseBuilders.Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(System.Text.Json.JsonSerializer.Serialize(dto))
                );
        
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_server.Urls[0] + "/api/v2/");
        
        var pokeApiClient = new PokeApiClient(httpClient);
        var list = await pokeApiClient.ListAsync<Pokemon>();
        
        Assert.NotNull(list);
        Assert.Equal(20, list.Results.Count);
        Assert.Equal(1350, list.Count);
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}