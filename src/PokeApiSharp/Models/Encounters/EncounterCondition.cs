using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Conditions which affect what Pokémon might appear in the wild, e.g. day or night.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="Values">A list of possible values for this encounter condition.</param>
[PokeApiResource("encounter-condition")]
public record EncounterCondition(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<EncounterConditionValue>> Values
);