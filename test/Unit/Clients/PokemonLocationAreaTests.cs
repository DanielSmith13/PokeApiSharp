using System.Net;
using System.Text;
using PokeApiSharp;
using Unit.Utils;
using Xunit;

namespace Unit.Clients;

public class PokemonLocationAreaTests
{
    private const string PokemonLocationAreaJson = """
    [
      {
        "location_area": {
          "name": "canalave-city-area",
          "url": "https://pokeapi.co/api/v2/location-area/1/"
        },
        "version_details": [
          {
            "encounter_details": [
              {
                "chance": 100,
                "condition_values": [],
                "max_level": 5,
                "method": {
                  "name": "old-rod",
                  "url": "https://pokeapi.co/api/v2/encounter-method/2/"
                },
                "min_level": 3
              }
            ],
            "max_chance": 100,
            "version": {
              "name": "diamond",
              "url": "https://pokeapi.co/api/v2/version/12/"
            }
          }
        ]
      }
    ]
    """;

    [Fact]
    public async Task GetPokemonLocationAreasAsync_ByName_UsesExpectedUrl()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(PokemonLocationAreaJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var result = await sut.GetPokemonLocationAreasAsync("pikachu");

        Assert.NotNull(result);
        var list = result.ToList();
        Assert.Single(list);
        Assert.Equal("canalave-city-area", list[0].LocationArea.Name);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal("https://pokeapi.co/api/v2/pokemon/pikachu/encounters", handler.LastRequest.RequestUri?.ToString());
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }

    [Fact]
    public async Task GetPokemonLocationAreasAsync_ById_UsesExpectedUrl()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(PokemonLocationAreaJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var result = await sut.GetPokemonLocationAreasAsync(25);

        Assert.NotNull(result);
        Assert.NotNull(handler.LastRequest);
        Assert.Equal("https://pokeapi.co/api/v2/pokemon/25/encounters", handler.LastRequest.RequestUri?.ToString());
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }
}
