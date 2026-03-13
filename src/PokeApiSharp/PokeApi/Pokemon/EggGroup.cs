using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("egg-group")]
public record EggGroup(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);