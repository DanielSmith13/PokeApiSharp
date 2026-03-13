using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Evolutions;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Locations;
using PokeApiSharp.PokeApi.Utility;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

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
    NamedApiResource<PokemonColour> Colour,
    NamedApiResource<PokemonShape> Shape,
    NamedApiResource<PokemonSpecies> EvolvesFromSpecies,
    ApiResource<EvolutionChain> EvolutionChain,
    NamedApiResource<PokemonHabitat> Habitat,
    NamedApiResource<Generation> Generation,
    IEnumerable<NameEntry> Names,
    IEnumerable<PalParkEncounterArea> PalParkEncounters,
    IEnumerable<FlavourTextEntry> FlavourTextEntries,
    IEnumerable<DescriptionEntry> FormDescriptions,
    IEnumerable<GenusEntry> Genera,
    IEnumerable<PokemonSpeciesVariety> Varieties
);

public record GenusEntry(
    string Genus,
    NamedApiResource<Language> Language
);

public record PokemonSpeciesDexEntry(
    int EntryNumber,
    NamedApiResource<Pokedex> Pokedex
);

public record PalParkEncounterArea(
    int BaseScore,
    int Rate,
    NamedApiResource<PalParkArea> Area
);

public record PokemonSpeciesVariety(
    bool IsDefault,
    NamedApiResource<Pokemon> Pokemon
);