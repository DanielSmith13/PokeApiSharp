using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Locations;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Pokemon;
using PokeApiSharp.PokeApi.Utility.Common;
using Type = PokeApiSharp.PokeApi.Pokemon.Type;

namespace PokeApiSharp.PokeApi.Games;

[PokeApiResource("generation")]
public record Generation(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Ability>> Abilities,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<Region>> MainRegion,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies,
    IEnumerable<NamedApiResource<Type>> Types,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);