using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Colours used for sorting Pokémon in a Pokédex.
/// The colour listed in the Pokédex is usually the colour
/// most noticeable or covering each Pokémon's body. No orange category exists;
/// Pokémon that are primarily orange are listed as red or brown.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonSpecies">A list of the Pokémon species that have this color.</param>
[PokeApiResource("pokemon-color")]
public record PokemonColour(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);