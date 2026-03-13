using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Locations;

[PokeApiResource("region")]
public record Region(
    int Id,
    IEnumerable<NamedApiResource<Location>> Locations,
    string Name,
    IEnumerable<NameEntry> Names,
    NamedApiResource<Generation> MainGeneration,
    IEnumerable<NamedApiResource<Pokedex>> Pokedexes,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);