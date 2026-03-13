using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Contests;

[PokeApiResource("contest-effect")]
public record ContestEffect(
    int Id,
    int Appeal,
    int Jam,
    IEnumerable<EffectEntry> EffectEntries,
    IEnumerable<FlavourTextEntry> FlavourTextEntries);