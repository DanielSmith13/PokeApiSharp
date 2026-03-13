using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Items;

[PokeApiResource("item-attribute")]
public record ItemAttribute(
    int Id,
    string Name,
    IEnumerable<Item> Items,
    IEnumerable<NameEntry> Names,
    IEnumerable<DescriptionEntry> Descriptions);