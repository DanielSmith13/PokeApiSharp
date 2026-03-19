using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Evolution triggers are the events and conditions that cause a Pokémon to evolve.
/// Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Methods_of_Evolution">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonSpecies">A list of Pokémon species that result from this evolution trigger.</param>
[PokeApiResource("evolution-trigger")]
public record EvolutionTrigger(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies);