using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokemon-habitat")]
public record PokemonHabitat(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);