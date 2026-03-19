using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Item categories determine where items will be placed in the players bag.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Items">A list of items that are a part of this category.</param>
/// <param name="Names">The name of this item category listed in different languages.</param>
/// <param name="Pocket">The pocket items in this category would be put in.</param>
[PokeApiResource("item-category")]
public record ItemCategory(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Item>> Items,
    IEnumerable<NameEntry> Names,
    NamedApiResource<ItemPocket> Pocket);