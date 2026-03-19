using PokeApiSharp.Attributes;
namespace PokeApiSharp;

/// <summary>
/// Location areas are sections of areas, such as floors in a building or cave.
/// Each area has its own set of possible Pokémon encounters.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="GameIndex">The internal id of an API resource within game data.</param>
/// <param name="EncounterMethodRates">A list of methods in which Pokémon may be encountered in this area and how likely the method will occur depending on the version of the game.</param>
/// <param name="Location">The location this location area can be found in.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonEncounters">A list of Pokémon that can be encountered in this area along with version specific details about the encounter.</param>
[PokeApiResource("location-area")]
public record LocationArea(
    int Id,
    string Name,
    int GameIndex,
    IEnumerable<EncounterMethodRate> EncounterMethodRates,
    NamedApiResource<Location> Location,
    IEnumerable<NameEntry> Names,
    IEnumerable<PokemonEncounter> PokemonEncounters);

/// <summary>
/// A method in which Pokémon may be encountered in an area and how likely the method will occur depending on the version of the game.
/// </summary>
/// <param name="EncounterMethod">The method in which Pokémon may be encountered in an area.</param>
/// <param name="VersionDetails">The chance of the encounter to occur on a version of the game.</param>
public record EncounterMethodRate(
    NamedApiResource<EncounterMethod> EncounterMethod,
    IEnumerable<EncounterVersionDetail> VersionDetails);
    
/// <summary>
/// The chance of the encounter to occur on a version of the game.
/// </summary>
/// <param name="Rate">The chance of an encounter to occur.</param>
/// <param name="Version">The version of the game in which the encounter can occur with the given chance.</param>
public record EncounterVersionDetail(
    int Rate,
    NamedApiResource<Version> Version);

/// <summary>
/// A list of versions and encounters with Pokémon that might happen in the referenced location area.
/// </summary>
/// <param name="Pokemon">The Pokémon being encountered.</param>
/// <param name="VersionDetails">A list of versions and encounters with Pokémon that might happen in the referenced location area.</param>
public record PokemonEncounter(
    NamedApiResource<Pokemon> Pokemon,
    IEnumerable<VersionEncounterDetail> VersionDetails);