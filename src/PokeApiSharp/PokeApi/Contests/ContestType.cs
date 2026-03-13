using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Berries;
using PokeApiSharp.PokeApi.Utility;

namespace PokeApiSharp.PokeApi.Contests;

[PokeApiResource("contest-type")]
public record Contest(
    int Id,
    string Name,
    NamedApiResource<BerryFlavour> BerryFlavor,
    IEnumerable<ContestName> Names);

public record ContestName(
    string Name,
    string Colour,
    NamedApiResource<Language> Language
);