using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Genders were introduced in Generation II to breed
/// Pokémon but can also result in visual differences
/// or even different evolutionary lines. Check out
/// <see href ="https://bulbapedia.bulbagarden.net/wiki/Gender">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="PokemonSpeciesDetails">The name of this resource listed in different languages.</param>
/// <param name="RequiredForEvolution">A list of all Pokémon species that are members of this egg group.</param>
[PokeApiResource("gender")]
public record Gender(
    int Id,
    string Name,
    IEnumerable<PokemonSpeciesGender> PokemonSpeciesDetails,
    IEnumerable<NamedApiResource<PokemonSpecies>> RequiredForEvolution
);

/// <summary>
/// Represents the gender of a Pokémon species.
/// </summary>
/// <param name="Rate">The chance of this Pokémon being female, in eighths; or -1 for genderless.</param>
/// <param name="PokemonSpecies">A Pokémon species that can be the referenced gender.</param>
public record PokemonSpeciesGender(
    int Rate,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);