using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Locations;
using PokeApiSharp.PokeApi.Pokemon;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Games;

[PokeApiResource("pokedex")]
public record Pokedex(
    int Id,
    string Name,
    bool IsMainSeries,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NameEntry> Names,
    IEnumerable<PokemonEntry> PokemonEntries,
    NamedApiResource<Region> Region,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups
);

public record PokemonEntry(
    int EntryNumber,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);