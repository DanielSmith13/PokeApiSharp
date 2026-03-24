using System.Text.Json;
using PokeApiSharp;

namespace Unit.Models;

public class TypeSpritesTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    [Fact]
    public void TypeSprites_CanBeDeserializedWithComplexStructure()
    {
        const string json = """
                            {
                                        "generation-iii": {
                                          "colosseum": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/colosseum/1.png",
                                            "symbol_icon": null
                                          },
                                          "emerald": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/emerald/1.png",
                                            "symbol_icon": null
                                          },
                                          "firered-leafgreen": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/firered-leafgreen/1.png",
                                            "symbol_icon": null
                                          },
                                          "ruby-sapphire": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/ruby-sapphire/1.png",
                                            "symbol_icon": null
                                          },
                                          "xd": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/xd/1.png",
                                            "symbol_icon": null
                                          }
                                        },
                                        "generation-iv": {
                                          "diamond-pearl": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/diamond-pearl/1.png",
                                            "symbol_icon": null
                                          },
                                          "heartgold-soulsilver": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/heartgold-soulsilver/1.png",
                                            "symbol_icon": null
                                          },
                                          "platinum": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/platinum/1.png",
                                            "symbol_icon": null
                                          }
                                        },
                                        "generation-ix": {
                                          "scarlet-violet": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-ix/scarlet-violet/1.png",
                                            "symbol_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-ix/scarlet-violet/small/1.png"
                                          }
                                        },
                                        "generation-v": {
                                          "black-2-white-2": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-v/black-2-white-2/1.png",
                                            "symbol_icon": null
                                          },
                                          "black-white": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-v/black-white/1.png",
                                            "symbol_icon": null
                                          }
                                        },
                                        "generation-vi": {
                                          "omega-ruby-alpha-sapphire": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vi/omega-ruby-alpha-sapphire/1.png",
                                            "symbol_icon": null
                                          },
                                          "x-y": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vi/x-y/1.png",
                                            "symbol_icon": null
                                          }
                                        },
                                        "generation-vii": {
                                          "lets-go-pikachu-lets-go-eevee": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vii/lets-go-pikachu-lets-go-eevee/1.png",
                                            "symbol_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vii/lets-go-pikachu-lets-go-eevee/small/1.png"
                                          },
                                          "sun-moon": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vii/sun-moon/1.png",
                                            "symbol_icon": null
                                          },
                                          "ultra-sun-ultra-moon": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vii/ultra-sun-ultra-moon/1.png",
                                            "symbol_icon": null
                                          }
                                        },
                                        "generation-viii": {
                                          "brilliant-diamond-shining-pearl": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/brilliant-diamond-shining-pearl/1.png",
                                            "symbol_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/brilliant-diamond-shining-pearl/small/1.png"
                                          },
                                          "legends-arceus": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/legends-arceus/1.png",
                                            "symbol_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/legends-arceus/small/1.png"
                                          },
                                          "sword-shield": {
                                            "name_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/sword-shield/1.png",
                                            "symbol_icon": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/sword-shield/small/1.png"
                                          }
                                        }
                                    }
                            """;

        var sprites = JsonSerializer.Deserialize<TypeSprites>(json, _options);

        Assert.NotNull(sprites);

        Assert.NotNull(sprites.GenerationIII);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/colosseum/1.png",
            sprites.GenerationIII.Colosseum.NameIcon);
        Assert.Null(sprites.GenerationIII.Colosseum.SymbolIcon);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iii/firered-leafgreen/1.png",
            sprites.GenerationIII.FireRedLeafGreen.NameIcon);

        Assert.NotNull(sprites.GenerationIV);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/diamond-pearl/1.png",
            sprites.GenerationIV.DiamondPearl.NameIcon);

        Assert.NotNull(sprites.GenerationIX);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-ix/scarlet-violet/1.png",
            sprites.GenerationIX.ScarletViolet.NameIcon);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-ix/scarlet-violet/small/1.png",
            sprites.GenerationIX.ScarletViolet.SymbolIcon);

        Assert.NotNull(sprites.GenerationV);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-v/black-2-white-2/1.png",
            sprites.GenerationV.Black2White2.NameIcon);

        Assert.NotNull(sprites.GenerationVI);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vi/omega-ruby-alpha-sapphire/1.png",
            sprites.GenerationVI.OmegaRubyAlphaSapphire.NameIcon);

        Assert.NotNull(sprites.GenerationVII);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-vii/lets-go-pikachu-lets-go-eevee/1.png",
            sprites.GenerationVII.LetsGoPikachuLetsGoEevee.NameIcon);

        Assert.NotNull(sprites.GenerationVIII);
        Assert.Equal(
            "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-viii/legends-arceus/small/1.png",
            sprites.GenerationVIII.LegendsArceus.SymbolIcon);
    }
}