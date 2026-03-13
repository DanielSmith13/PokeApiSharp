using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record VersionEncounterDetail(
    NamedApiResource<Version> Version,
    int MaxChance,
    IEnumerable<Encounter> EncounterDetails
);