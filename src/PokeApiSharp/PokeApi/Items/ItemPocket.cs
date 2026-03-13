using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Items;

[PokeApiResource("item-pocket")]
public record ItemPocket(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<ItemCategory>> Categories,
    IEnumerable<NameEntry> Names);