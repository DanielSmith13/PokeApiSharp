using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Methods by which Pokémon can learn moves.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Descriptions">The description of this resource listed in different languages.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="VersionGroups">A list of version groups where moves can be learned through this method.</param>
[PokeApiResource("move-learn-method")]
public record MoveLearnMethod(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);