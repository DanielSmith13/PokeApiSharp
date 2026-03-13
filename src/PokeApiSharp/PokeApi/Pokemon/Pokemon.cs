using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Items;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Utility.Common;
using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokemon")]
public record Pokemon(
    int Id,
    string Name,
    int BaseExperience,
    int Height,
    bool IsDefault,
    int Order,
    int Weight,
    IEnumerable<PokemonAbility> Abilities,
    IEnumerable<NamedApiResource<PokemonForm>> Forms,
    IEnumerable<VersionGameIndex> GameIndices,
    IEnumerable<PokemonHeldItem> HeldItems,
    string LocationAreaEncounters,
    IEnumerable<PokemonMove> Moves,
    IEnumerable<PokemonTypePast> PastTypes,
    IEnumerable<PokemonAbilityPast> PastAbilities,
    PokemonSprites Sprites,
    PokemonCries Cries,
    NamedApiResource<PokemonSpecies> Species,
    IEnumerable<PokemonStat> Stats,
    IEnumerable<PokemonType> Types
);

public record PokemonAbility(
    bool IsHidden,
    int Slot,
    NamedApiResource<Ability> Ability
);

public record PokemonHeldItem(
    NamedApiResource<Item> Item,
    IEnumerable<PokemonHeldItemVersion> VersionDetails
);

public record PokemonHeldItemVersion(
    int Rarity,
    NamedApiResource<Version> Version
);

public record PokemonMove(
    NamedApiResource<Move> Move,
    IEnumerable<PokemonMoveVersion> VersionGroupDetails
);

public record PokemonMoveVersion(
    NamedApiResource<MoveLearnMethod> MoveLearnMethod,
    NamedApiResource<VersionGroup> VersionGroup,
    int? LevelLearnedAt,
    int? Order
);

public record PokemonTypePast(
    IEnumerable<PokemonType> Types,
    NamedApiResource<Generation> Generation
);

public record PokemonType(
    int Slot,
    NamedApiResource<Type> Type
);

public record PokemonAbilityPast(
    IEnumerable<PokemonAbility> Abilities,
    NamedApiResource<Generation> Generation
);

public record PokemonSprites(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

public record PokemonCries(
    string Latest,
    string Legacy
);

public record PokemonStat(
    int BaseStat,
    int Effort,
    NamedApiResource<Stat> Stat
);