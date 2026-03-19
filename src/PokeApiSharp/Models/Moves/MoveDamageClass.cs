using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Damage classes moves can have, e.g. physical, special, or non-damaging.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Descriptions">The description of this resource listed in different languages.</param>
/// <param name="Moves">A list of moves that fall into this damage class.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("move-damage-class")]
public record MoveDamageClass(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names
);