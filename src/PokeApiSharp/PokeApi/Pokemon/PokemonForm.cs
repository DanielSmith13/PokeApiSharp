using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokemon-form")]
public record PokemonForm(
    int Id,
    string Name,
    int Order,
    int FormOrder,
    bool IsDefault,
    bool IsBattleOnly,
    bool IsMega,
    string FormName,
    NamedApiResource<Pokemon> Pokemon,
    IEnumerable<PokemonFormType> Types,
    PokemonFormSprites Sprites,
    NamedApiResource<VersionGroup> VersionGroup,
    IEnumerable<NameEntry> Names,
    IEnumerable<NameEntry> FormNames
);

public record PokemonFormType(
    int Slot,
    NamedApiResource<PokemonType> Type
);

public record PokemonFormSprites(
    string FrontDefault,
    string FrontShiny,
    string BackDefault,
    string BackShiny
);