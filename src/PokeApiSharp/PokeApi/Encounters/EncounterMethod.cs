using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Encounters;

[PokeApiResource("encounter-method")]
public record EncounterMethod(
    int Id,
    string Name,
    int Order,
    IEnumerable<NameEntry> Names
);