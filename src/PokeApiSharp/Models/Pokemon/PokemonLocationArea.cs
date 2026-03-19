using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Pokémon Location Areas are ares where Pokémon can be found.
/// </summary>
/// <param name="LocationArea">The location area the referenced Pokémon can be encountered in.</param>
/// <param name="VersionDetails">A list of versions and encounters with the referenced Pokémon that might happen.</param>
[PokeApiResource("pokemon-location-area")]
public record PokemonLocationArea(
    NamedApiResource<LocationArea> LocationArea,
    IEnumerable<VersionEncounterDetail> VersionDetails
);