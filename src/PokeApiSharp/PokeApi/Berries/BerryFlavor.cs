using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Contests;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Berries;

[PokeApiResource("berry-flavor")]
public record BerryFlavour(
    int Id,
    string Name,
    IEnumerable<FlavourBerryMap> Berries,
    NamedApiResource<Contest> ContestType,
    IEnumerable<NameEntry> Names);
    
public record FlavourBerryMap(
    int Potency,
    NamedApiResource<Berry> Berry
);