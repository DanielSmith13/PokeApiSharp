using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Encounter condition values are the various states that an encounter condition can have, i.e. time of day can be either day or night.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Condition">The condition this encounter condition value pertains to.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("encounter-condition-value")]
public record EncounterConditionValue(
    int Id,
    string Name,
    NamedApiResource<EncounterCondition> Condition,
    IEnumerable<NameEntry> Names
);