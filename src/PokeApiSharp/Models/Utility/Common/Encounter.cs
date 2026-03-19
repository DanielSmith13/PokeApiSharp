namespace PokeApiSharp;

/// <summary>
/// Represents an encounter for a Pokémon.
/// </summary>
/// <param name="MinLevel">The lowest level the Pokémon could be encountered at.</param>
/// <param name="MaxLevel">The highest level the Pokémon could be encountered at.</param>
/// <param name="ConditionValues">A list of condition values that must be in effect for this encounter to occur.</param>
/// <param name="Chance">Percentage chance that this encounter will occur.</param>
/// <param name="Method">The method by which this encounter happens.</param>
public record Encounter(
    int MinLevel,
    int MaxLevel,
    IEnumerable<NamedApiResource<EncounterConditionValue>> ConditionValues,
    int Chance,
    NamedApiResource<EncounterMethod> Method
);