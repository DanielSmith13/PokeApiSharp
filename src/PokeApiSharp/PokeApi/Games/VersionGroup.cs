using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Locations;

namespace PokeApiSharp.PokeApi.Games;

[PokeApiResource("version-group")]
public record VersionGroup(
    int Id,
    string Name,
    int Order,
    NamedApiResource<Generation> Generation,
    IEnumerable<NamedApiResource<Pokedex>> Pokedexes,
    IEnumerable<NamedApiResource<Region>> Regions,
    IEnumerable<NamedApiResource<Version>> Versions);