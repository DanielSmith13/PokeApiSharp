using PokeApiSharp.Attributes;
namespace PokeApiSharp;

/// <summary>
/// Item attributes define particular aspects of items, e.g. "usable in battle" or "consumable".
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Items">A list of items that have this attribute.</param>
/// <param name="Names">The name of this item attribute listed in different languages.</param>
/// <param name="Descriptions">The description of this item attribute listed in different languages.</param>
[PokeApiResource("item-attribute")]
public record ItemAttribute(
    int Id,
    string Name,
    IEnumerable<Item> Items,
    IEnumerable<NameEntry> Names,
    IEnumerable<DescriptionEntry> Descriptions);