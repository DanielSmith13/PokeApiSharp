using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Egg Groups are categories which determine which Pokémon are able to interbreed.
/// Pokémon may belong to either one or two Egg Groups. Check out
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Egg_Group">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonSpecies">A list of all Pokémon species that are members of this egg group.</param>
[PokeApiResource("egg-group")]
public record EggGroup(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);