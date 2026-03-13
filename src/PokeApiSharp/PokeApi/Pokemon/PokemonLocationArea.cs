using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Locations;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokemon-location-area")]
public record PokemonLocationArea(
    NamedApiResource<LocationArea> LocationArea,
    IEnumerable<VersionEncounterDetail> VersionDetails
);