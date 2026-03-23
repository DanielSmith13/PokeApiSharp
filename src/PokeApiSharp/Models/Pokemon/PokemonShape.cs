using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Shapes used for sorting Pokémon in a Pokédex.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="AwesomeNames">The "scientific" name of this Pokémon shape listed in different languages.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonSpecies">A list of the Pokémon species that have this shape.</param>
[PokeApiResource("pokemon-shape")]
public record PokemonShape(
    int Id,
    string Name,
    IEnumerable<AwesomeNameEntry> AwesomeNames,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);

/// <summary>
/// Represents a localised "scientific" name for a Pokémon shape.
/// </summary>
/// <param name="AwesomeName">The localised "scientific" name for an API resource in a specific language.</param>
/// <param name="Language">The language this "scientific" name is in.</param>
public record AwesomeNameEntry(
    string AwesomeName,
    NamedApiResource<Language> Language
);