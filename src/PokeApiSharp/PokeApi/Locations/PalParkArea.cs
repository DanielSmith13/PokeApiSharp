using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Pokemon;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Locations;

[PokeApiResource("pal-park-area")] 
public record PalParkArea(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<PalParkEncounterSpecies> PokemonEncounters);

public record PalParkEncounterSpecies(
    int BaseScore,
    int Rate,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);