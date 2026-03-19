using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Targets moves can be directed at during battle. Targets can be Pokémon, environments or even other moves.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Descriptions">The description of this resource listed in different languages.</param>
/// <param name="Moves">A list of moves that are directed at this target.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("move-target")]
public record MoveTarget(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names);