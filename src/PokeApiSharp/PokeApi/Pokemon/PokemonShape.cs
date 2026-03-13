using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokemon-shape")]
public record PokemonShape(
    int Id,
    string Name,
    IEnumerable<AwesomeNameEntry> AwesomeNames,
    IEnumerable<NameEntry> Names,
    IEnumerable<PokemonSpecies> PokemonSpecies
);

public record AwesomeNameEntry(
    string AwesomeName,
    Language Language
);