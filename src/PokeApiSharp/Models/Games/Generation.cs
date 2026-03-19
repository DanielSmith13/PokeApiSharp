using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// A generation is a grouping of the Pokémon games that separates them
/// based on the Pokémon they include. In each generation, a new set of Pokémon, Moves, Abilities,
/// and Types that did not exist in the previous generation are released.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Abilities">A list abilities that were introduced in this generation.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="MainRegion">The main region travelled in this generation.</param>
/// <param name="Moves">A list of moves that were introduced in this generation.</param>
/// <param name="PokemonSpecies">A list of Pokémon species that were introduced in this generation.</param>
/// <param name="Types">A list of types that were introduced in this generation.</param>
/// <param name="VersionGroups">A list of version groups that were introduced in this generation.</param>
[PokeApiResource("generation")]
public record Generation(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Ability>> Abilities,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<Region>> MainRegion,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies,
    IEnumerable<NamedApiResource<Type>> Types,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);