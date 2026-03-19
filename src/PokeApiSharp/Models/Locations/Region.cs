using PokeApiSharp.Attributes;
namespace PokeApiSharp;

/// <summary>
/// A region is an organized area of the Pokémon world. Most often,
/// the main difference between regions is the species of Pokémon
/// that can be encountered within them.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Locations">A list of locations that can be found in this region.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="MainGeneration">The generation this region was introduced in.</param>
/// <param name="Pokedexes">A list of Pokédexes that catalogue Pokémon in this region.</param>
/// <param name="VersionGroups">A list of version groups where this region can be visited.</param>
[PokeApiResource("region")]
public record Region(
    int Id,
    IEnumerable<NamedApiResource<Location>> Locations,
    string Name,
    IEnumerable<NameEntry> Names,
    NamedApiResource<Generation> MainGeneration,
    IEnumerable<NamedApiResource<Pokedex>> Pokedexes,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);