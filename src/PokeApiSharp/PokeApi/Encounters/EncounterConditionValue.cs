using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Encounters;

[PokeApiResource("encounter-condition-value")]
public record EncounterConditionValue(
    int Id,
    string Name,
    NamedApiResource<EncounterCondition> EncounterCondition,
    IEnumerable<NameEntry> Names
);