using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Items;

[PokeApiResource("item-category")]
public record ItemCategory(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Item>> Items,
    IEnumerable<NameEntry> Names,
    NamedApiResource<ItemPocket> Pocket);