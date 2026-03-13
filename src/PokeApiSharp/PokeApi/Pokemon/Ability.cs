using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Utility;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("ability")]
public record Ability(
    int Id,
    string Name,
    bool IsMainSeries,
    NamedApiResource<Generation> Generation,
    IEnumerable<NameEntry> Names,
    IEnumerable<VerboseEffect> EffectEntries,
    IEnumerable<AbilityEffectChange> EffectChanges,
    IEnumerable<AbilityFlavourText> FlavorTextEntries,
    IEnumerable<AbilityPokemon> Pokemon
);

public record AbilityEffectChange(
    IEnumerable<EffectEntry> EffectEntries,
    NamedApiResource<Language> Language
);

public record AbilityFlavourText(
    string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup
);

public record AbilityPokemon(
    bool IsHidden,
    int Slot,
    NamedApiResource<Pokemon> Pokemon
);