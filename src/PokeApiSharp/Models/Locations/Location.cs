using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Locations that can be visited within the games.
/// Locations make up sizeable portions of regions, like cities or routes.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Region">The region this location can be found in.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="GameIndices">The game indices relevant to this location by generation.</param>
/// <param name="Areas">A list of areas that can be found within this location.</param>
[PokeApiResource("location")]
public record Location(
    int Id,
    string Name,
    NamedApiResource<Region> Region,
    IEnumerable<NameEntry> Names,
    IEnumerable<GenerationGameIndex> GameIndices,
    IEnumerable<NamedApiResource<LocationArea>> Areas);