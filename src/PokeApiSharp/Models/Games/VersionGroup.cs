using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Version groups categorize highly similar versions of the games.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Order">Order for sorting. Almost by the date of release, except similar versions are grouped together.</param>
/// <param name="Generation">The generation this version was introduced in.</param>
/// <param name="MoveLearnMethods">A list of methods in which Pokémon can learn moves in this version group.</param>>
/// <param name="Pokedexes">A list of Pokédexes introduced in this version group.</param>
/// <param name="Regions">A list of regions that can be visited in this version group.</param>
/// <param name="Versions">The versions this version group owns.</param>
[PokeApiResource("version-group")]
public record VersionGroup(
    int Id,
    string Name,
    int Order,
    NamedApiResource<Generation> Generation,
    IEnumerable<NamedApiResource<MoveLearnMethod>> MoveLearnMethods,
    IEnumerable<NamedApiResource<Pokedex>> Pokedexes,
    IEnumerable<NamedApiResource<Region>> Regions,
    IEnumerable<NamedApiResource<Version>> Versions);