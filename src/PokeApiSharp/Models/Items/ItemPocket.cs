using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Pockets within the players bag used for storing items by category.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Categories">A list of item categories that are relevant to this item pocket.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("item-pocket")]
public record ItemPocket(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<ItemCategory>> Categories,
    IEnumerable<NameEntry> Names);