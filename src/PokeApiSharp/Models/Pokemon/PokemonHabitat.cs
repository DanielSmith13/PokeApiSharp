using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Habitats are generally different terrain Pokémon can be found
/// in but can also be areas designated for rare or legendary Pokémon.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonSpecies">A list of the Pokémon species that can be found in this habitat.</param>
[PokeApiResource("pokemon-habitat")]
public record PokemonHabitat(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);