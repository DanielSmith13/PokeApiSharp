using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Contests;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Pokemon;
using PokeApiSharp.PokeApi.Utility;
using PokeApiSharp.PokeApi.Utility.Common;
using Type = PokeApiSharp.PokeApi.Pokemon.Type;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move")]
public record Move(
    int Id,
    string Name,
    int Accuracy,
    int EffectChance,
    int Pp,
    int Priority,
    int Power,
    ContestComboSets ContestCombos,
    NamedApiResource<Contest> ContestType,
    ApiResource<ContestEffect> ContestEffect,
    NamedApiResource<MoveDamageClass> DamageClass,
    IEnumerable<VerboseEffect> EffectEntries,
    IEnumerable<AbilityEffectChange> EffectChanges,
    IEnumerable<Pokemon.Pokemon> LearnedByPokemon,
    IEnumerable<MoveFlavourText> FlavourTextEntries,
    NamedApiResource<Generation> Generation,
    IEnumerable<MachineVersionDetail> Machines,
    MoveMetaData MetaData,
    IEnumerable<NameEntry> Names,
    IEnumerable<PastMoveStatValues> PastValues,
    IEnumerable<MoveStatChange> StatChanges,
    ApiResource<SuperContestEffect> SuperContestEffect,
    NamedApiResource<MoveTarget> Target,
    NamedApiResource<Type> Type);

public record ContestComboSets(
    ContestComboDetail Normal,
    ContestComboDetail Super);

public record ContestComboDetail(
    IEnumerable<NamedApiResource<Move>> UseBefore,
    IEnumerable<NamedApiResource<Move>> UseAfter);

public record MoveFlavourText(
    string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup);

public record MoveMetaData(
    NamedApiResource<MoveAilment> Ailment,
    NamedApiResource<MoveCategory> Category,
    int MinHits,
    int MaxHits,
    int MinTurns,
    int MaxTurns,
    int Drain,
    int Healing,
    int CritRate,
    int AilmentChance,
    int FlinchChance,
    int StatChance);

public record MoveStatChange(
    int Change,
    NamedApiResource<Stat> Stat);

public record PastMoveStatValues(
    int? Accuracy,
    int? EffectChance,
    int Power,
    int? Pp,
    IEnumerable<VerboseEffect> EffectEntries,
    NamedApiResource<Type> Type,
    NamedApiResource<VersionGroup> VersionGroup);