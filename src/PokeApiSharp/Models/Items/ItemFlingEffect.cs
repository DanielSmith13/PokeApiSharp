using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// The various effects of the move "Fling" when used with different items.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="EffectEntries">The result of this fling effect listed in different languages.</param>
/// <param name="Items">A list of items that have this fling effect.</param>
[PokeApiResource("item-fling-effect")]
public record ItemFlingEffect(
    int Id,
    string Name,
    IEnumerable<EffectEntry> EffectEntries,
    IEnumerable<NamedApiResource<Item>> Items);