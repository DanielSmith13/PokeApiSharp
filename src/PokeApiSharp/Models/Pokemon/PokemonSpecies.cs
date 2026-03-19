using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// A Pokémon Species forms the basis for at least one Pokémon.
/// Attributes of a Pokémon species are shared across all varieties
/// of Pokémon within the species. A good example is Wormadam;
/// Wormadam is the species which can be found in three different
/// varieties, Wormadam-Trash, Wormadam-Sandy, and Wormadam-Plant.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Order">
/// The order in which species should be sorted. Based on National Dex
/// order, except families are grouped together and sorted by stage.
/// </param>
/// <param name="GenderRate">The chance of this Pokémon being female, in eighths; or -1 for genderless.</param>
/// <param name="CaptureRate">The base capture rate; up to 255. The higher the number, the easier the catch.</param>
/// <param name="BaseHappiness">
/// The happiness when caught by a normal Pokéball; up to 255. The higher the number, the happier the Pokémon is.
/// </param>
/// <param name="IsBaby">Whether this is a baby Pokémon.</param>
/// <param name="IsLegendary">Whether this is a legendary Pokémon.</param>
/// <param name="IsMythical">Whether this is a mythical Pokémon.</param>
/// <param name="HatchCounter">
/// Initial hatch counter: one must walk Y × (hatch_counter + 1) steps
/// before this Pokémon's egg hatches, unless utilising bonuses like
/// Flame Body's. Y varies per generation. In Generations II, III, and VII,
/// Egg cycles are 256 steps long. In Generation IV, Egg cycles are 255 steps long.
/// In Pokémon Brilliant Diamond and Shining Pearl, Egg cycles are also 255 steps long,
/// but are shorter on special dates. In Generations V and VI, Egg cycles are 257 steps long.
/// In Pokémon Sword and Shield, and in Pokémon Scarlet and Violet, Egg cycles are 128 steps long.
/// </param>
/// <param name="HasGenderDifferences">Whether this Pokémon has visual gender differences.</param>
/// <param name="FormsSwitchable">Whether this Pokémon has multiple forms and can switch between them.</param>
/// <param name="GrowthRate">The rate at which this Pokémon species gains levels.</param>
/// <param name="PokedexNumbers">A list of Pokedexes and the indexes reserved within them for this Pokémon species.</param>
/// <param name="EggGroups">A list of egg groups this Pokémon species is a member of.</param>
/// <param name="Colour">The colour of this Pokémon for Pokédex search.</param>
/// <param name="Shape">The shape of this Pokémon for Pokédex search.</param>
/// <param name="EvolvesFromSpecies">The Pokémon species that evolves into this Pokémon species.</param>
/// <param name="EvolutionChain">The evolution chain this Pokémon species is a member of.</param>
/// <param name="Habitat">The habitat this Pokémon species can be encountered in.</param>
/// <param name="Generation">The generation that this Pokémon species was introduced in.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PalParkEncounters">A list of encounters that can be had with this Pokémon species in pal park.</param>
/// <param name="FlavourTextEntries">A list of flavour text entries for this Pokémon species.</param>
/// <param name="FormDescriptions">Descriptions of different forms Pokémon take on within the Pokémon species.</param>
/// <param name="Genera">The genus of this Pokémon species listed in multiple languages.</param>
/// <param name="Varieties">A list of the Pokémon that exist within this Pokémon species.</param>
[PokeApiResource("pokemon-species")]
public record PokemonSpecies(
    int Id,
    string Name,
    int Order,
    int GenderRate,
    int CaptureRate,
    int BaseHappiness,
    bool IsBaby,
    bool IsLegendary,
    bool IsMythical,
    int HatchCounter,
    bool HasGenderDifferences,
    bool FormsSwitchable,
    NamedApiResource<GrowthRate> GrowthRate,
    IEnumerable<PokemonSpeciesDexEntry> PokedexNumbers,
    IEnumerable<NamedApiResource<EggGroup>> EggGroups,
    [property: JsonPropertyName("color")]NamedApiResource<PokemonColour> Colour,
    NamedApiResource<PokemonShape> Shape,
    NamedApiResource<PokemonSpecies> EvolvesFromSpecies,
    ApiResource<EvolutionChain> EvolutionChain,
    NamedApiResource<PokemonHabitat> Habitat,
    NamedApiResource<Generation> Generation,
    IEnumerable<NameEntry> Names,
    IEnumerable<PalParkEncounterArea> PalParkEncounters,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<FlavourTextEntry> FlavourTextEntries,
    IEnumerable<DescriptionEntry> FormDescriptions,
    IEnumerable<GenusEntry> Genera,
    IEnumerable<PokemonSpeciesVariety> Varieties
);

/// <summary>
/// A genus entry for a Pokémon species.
/// </summary>
/// <param name="Genus">The localised genus for the referenced Pokémon species</param>
/// <param name="Language">The language this genus is in.</param>
public record GenusEntry(
    string Genus,
    NamedApiResource<Language> Language
);

/// <summary>
/// A dex entry for a Pokémon species.
/// </summary>
/// <param name="EntryNumber">The index number within the Pokédex.</param>
/// <param name="Pokedex">The Pokédex the referenced Pokémon species can be found in.</param>
public record PokemonSpeciesDexEntry(
    int EntryNumber,
    NamedApiResource<Pokedex> Pokedex
);

/// <summary>
/// A pal park encounter area for a Pokémon species.
/// </summary>
/// <param name="BaseScore">
/// The base score given to the player when the referenced
/// Pokémon is caught during a pal park run.
/// </param>
/// <param name="Rate">
/// The base rate for encountering the referenced Pokémon
/// in this pal park area.
/// </param>
/// <param name="Area">The pal park area where this encounter happens.</param>
public record PalParkEncounterArea(
    int BaseScore,
    int Rate,
    NamedApiResource<PalParkArea> Area
);

/// <summary>
/// A Pokémon species variety.
/// </summary>
/// <param name="IsDefault">Whether this variety is the default variety.</param>
/// <param name="Pokemon">The Pokémon variety.</param>
public record PokemonSpeciesVariety(
    bool IsDefault,
    NamedApiResource<Pokemon> Pokemon
);