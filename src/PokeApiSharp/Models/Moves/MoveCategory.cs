using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Very general categories that loosely group move effects.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Moves">A list of moves that fall into this category.</param>
/// <param name="Descriptions">The description of this resource listed in different languages.</param>
[PokeApiResource("move-category")]
public record MoveCategory(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<DescriptionEntry> Descriptions);