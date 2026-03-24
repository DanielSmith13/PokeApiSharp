using System.Text.Json;
using PokeApiSharp;

namespace Unit.Models;

public class PokemonSpritesTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    [Fact]
    public void PokemonSprites_CanBeDeserializedWithComplexStructure()
    {
        const string json = """
                            {
                                        "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/1.png",
                                        "back_female": null,
                                        "back_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/shiny/1.png",
                                        "back_shiny_female": null,
                                        "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png",
                                        "front_female": null,
                                        "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/1.png",
                                        "front_shiny_female": null,
                                        "other": {
                                          "dream_world": {
                                            "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/1.svg",
                                            "front_female": null
                                          },
                                          "home": {
                                            "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/1.png",
                                            "front_female": null,
                                            "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/shiny/1.png",
                                            "front_shiny_female": null
                                          },
                                          "official-artwork": {
                                            "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png",
                                            "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/shiny/1.png"
                                          },
                                          "showdown": {
                                            "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/back/1.gif",
                                            "back_female": null,
                                            "back_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/back/shiny/1.gif",
                                            "back_shiny_female": null,
                                            "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/1.gif",
                                            "front_female": null,
                                            "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/shiny/1.gif",
                                            "front_shiny_female": null
                                          }
                                        },
                                        "versions": {
                                          "generation-i": {
                                            "red-blue": {
                                              "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/back/1.png",
                                              "back_gray": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/back/gray/1.png",
                                              "back_transparent": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/transparent/back/1.png",
                                              "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/1.png",
                                              "front_gray": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/gray/1.png",
                                              "front_transparent": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/transparent/1.png"
                                            },
                                            "yellow": {
                                              "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/back/1.png",
                                              "back_gray": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/back/gray/1.png",
                                              "back_transparent": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/transparent/back/1.png",
                                              "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/1.png",
                                              "front_gray": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/gray/1.png",
                                              "front_transparent": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/yellow/transparent/1.png"
                                            }
                                          }
                                        }
                                    }
                            """;

        var sprites = JsonSerializer.Deserialize<PokemonSprites>(json, _options);

        Assert.NotNull(sprites);
        Assert.Equal("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png",
            sprites.FrontDefault);

        Assert.NotNull(sprites.Other);
        Assert.NotNull(sprites.Other.DreamWorld);
        Assert.Equal("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/1.svg",
            sprites.Other.DreamWorld.FrontDefault);

        Assert.NotNull(sprites.Other.Home);
        Assert.Equal("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/shiny/1.png",
            sprites.Other.Home.FrontShiny);

        Assert.NotNull(sprites.Other.OfficialArtwork);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png",
            sprites.Other.OfficialArtwork.FrontDefault);

        Assert.NotNull(sprites.Other.Showdown);
        Assert.Equal("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/1.gif",
            sprites.Other.Showdown.FrontDefault);

        Assert.NotNull(sprites.Versions);
        Assert.NotNull(sprites.Versions.GenerationI);
        Assert.NotNull(sprites.Versions.GenerationI.RedBlue);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/1.png",
            sprites.Versions.GenerationI.RedBlue.FrontDefault);
    }
}