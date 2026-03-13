using System.Collections.Generic;
using PokeApiSharp.PokeApi.Encounters;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record Encounter(
    int MinLevel,
    int MaxLevel,
    IEnumerable<NamedApiResource<EncounterConditionValue>> ConditionValues,
    int Chance,
    NamedApiResource<EncounterMethod> Method
);