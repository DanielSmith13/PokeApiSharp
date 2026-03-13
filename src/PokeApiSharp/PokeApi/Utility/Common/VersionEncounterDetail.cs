using System.Collections.Generic;
using PokeApiSharp.PokeApi.Games;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record VersionEncounterDetail(
    NamedApiResource<Version> Version,
    int MaxChance,
    IEnumerable<Encounter> EncounterDetails
);