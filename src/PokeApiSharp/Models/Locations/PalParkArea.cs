using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Areas used for grouping Pokémon encounters in Pal Park.
/// They're like habitats that are specific to <see href="https://bulbapedia.bulbagarden.net/wiki/Pal_Park">Pal Park</see>.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonEncounters">A list of Pokémon encountered in this pal park area along with details.</param>
[PokeApiResource("pal-park-area")] 
public record PalParkArea(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<PalParkEncounterSpecies> PokemonEncounters);

/// <summary>
/// Details about a Pokémon encountered in a pal park area, including the base score, encounter rate, and the Pokémon species itself.
/// </summary>
/// <param name="BaseScore">The base score given to the player when this Pokémon is caught during a pal park run.</param>
/// <param name="Rate">The base rate for encountering this Pokémon in this pal park area.</param>
/// <param name="PokemonSpecies">The Pokémon species being encountered.</param>
public record PalParkEncounterSpecies(
    int BaseScore,
    int Rate,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);