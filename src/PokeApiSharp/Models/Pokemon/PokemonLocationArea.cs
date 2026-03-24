namespace PokeApiSharp;

/// <summary>
/// Pokémon Location Areas are areas where Pokémon can be found.
/// </summary>
/// <param name="LocationArea">The location area the referenced Pokémon can be encountered in.</param>
/// <param name="VersionDetails">A list of versions and encounters with the referenced Pokémon that might happen.</param>
public record PokemonLocationArea(
    NamedApiResource<LocationArea> LocationArea,
    IEnumerable<VersionEncounterDetail> VersionDetails
);