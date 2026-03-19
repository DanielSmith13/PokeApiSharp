namespace PokeApiSharp;

/// <summary>
/// Represents encounter detail for a Pokémon encounter within a version.
/// </summary>
/// <param name="Version">The game version this encounter happens in.</param>
/// <param name="MaxChance">The total percentage of all encounter potential.</param>
/// <param name="EncounterDetails">A list of encounters and their specifics.</param>
public record VersionEncounterDetail(
    NamedApiResource<Version> Version,
    int MaxChance,
    IEnumerable<Encounter> EncounterDetails
);