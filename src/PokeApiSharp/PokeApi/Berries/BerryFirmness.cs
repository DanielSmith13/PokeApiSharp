using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Berries;

[PokeApiResource("berry-firmness")]
public record BerryFirmness(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Berry>> Berries,
    IEnumerable<NameEntry> Names
);