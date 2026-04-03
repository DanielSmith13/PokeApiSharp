using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// A Pokédex is a handheld electronic encyclopedia device;
/// one which is capable of recording and retaining information
/// of the various Pokémon in a given region except the national dex
/// and some smaller dexes related to portions of a region.
/// See <see href="https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9dex">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="IsMainSeries">Whether this Pokédex originated in the main series of the video games.</param>
/// <param name="Descriptions">The description of this resource listed in different languages.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PokemonEntries">A list of Pokémon catalogued in this Pokédex and their indexes.</param>
/// <param name="Region">The region this Pokédex catalogues Pokémon for.</param>
/// <param name="VersionGroups">A list of version groups where this Pokédex was introduced.</param>
[PokeApiResource("pokedex")]
public record Pokedex(
    int Id,
    string Name,
    bool IsMainSeries,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NameEntry> Names,
    IEnumerable<PokemonEntry> PokemonEntries,
    NamedApiResource<Region>? Region,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups
);

/// <summary>
/// A reference to a Pokémon species and its index in a Pokédex.
/// </summary>
/// <param name="EntryNumber">The index of this Pokémon species entry within the Pokédex.</param>
/// <param name="PokemonSpecies">The Pokémon species being encountered.</param>
public record PokemonEntry(
    int EntryNumber,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);