using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Items;

[PokeApiResource("item-fling-effect")]
public record ItemFlingEffect(
    int Id,
    string Name,
    IEnumerable<EffectEntry> EffectEntries,
    IEnumerable<Item> Items);