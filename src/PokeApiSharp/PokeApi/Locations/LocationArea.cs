using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Encounters;
using PokeApiSharp.PokeApi.Utility.Common;
using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Locations;

[PokeApiResource("location-area")]
public record LocationArea(
    int Id,
    string Name,
    int GameIndex,
    IEnumerable<EncounterMethodRate> EncounterMethodRates,
    NamedApiResource<Location> Location,
    IEnumerable<NameEntry> Names,
    IEnumerable<PokemonEncounter> PokemonEncounters);

public record EncounterMethodRate(
    NamedApiResource<EncounterMethod> EncounterMethod,
    IEnumerable<EncounterVersionDetail> VersionDetails);
    
public record EncounterVersionDetail(
    int Rate,
    NamedApiResource<Version> Version);

public record PokemonEncounter(
    NamedApiResource<Pokemon.Pokemon> Pokemon,
    IEnumerable<VersionEncounterDetail> VersionDetails);