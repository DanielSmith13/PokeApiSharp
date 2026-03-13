using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("characteristic")]
public record Characteristic(
    int Id,
    int GeneModulo,
    IEnumerable<int> PossibleValues,
    NamedApiResource<Stat> HighestStat,
    IEnumerable<DescriptionEntry> Descriptions);