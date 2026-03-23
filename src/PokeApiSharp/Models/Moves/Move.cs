using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Moves are the skills of Pokémon in battle. In battle, a Pokémon uses one
/// move each turn. Some moves (including those learned by Hidden Machine) can
/// be used outside battle as well, usually to remove
/// obstacles or explore new areas.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Accuracy">The percentage value of how likely this move is to be successful.</param>
/// <param name="EffectChance">The percentage value of how likely it is this moves effect will happen.</param>
/// <param name="Pp">Power points. The number of times this move can be used.</param>
/// <param name="Priority">A value between -8 and 8. Sets the order in which moves are executed during battle. See <see href="https://bulbapedia.bulbagarden.net/wiki/Priority">Bulbapedia</see> for greater detail.</param>
/// <param name="Power">The base power of this move with a value of 0 if it does not have a base power.</param>
/// <param name="ContestCombos">A detail of normal and super contest combos that require this move.</param>
/// <param name="ContestType">The type of appeal this move gives a Pokémon when used in a contest.</param>
/// <param name="ContestEffect">The effect the move has when used in a contest.</param>
/// <param name="DamageClass">The type of damage the move inflicts on the target, e.g. physical.</param>
/// <param name="EffectEntries">The effect of this move listed in different languages.</param>
/// <param name="EffectChanges">The list of previous effects this move has had across version groups of the games.</param>
/// <param name="LearnedByPokemon">List of Pokémon that can learn the move</param>
/// <param name="FlavourTextEntries">The flavour text of this move listed in different languages.</param>
/// <param name="Generation">The generation in which this move was introduced.</param>
/// <param name="Machines">A list of the machines that teach this move.</param>
/// <param name="Meta">Metadata about this move.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="PastValues">A list of move resource value changes across version groups of the game.</param>
/// <param name="StatChanges">A list of stats this moves effects and how much it effects them.</param>
/// <param name="SuperContestEffect">The effect the move has when used in a super contest.</param>
/// <param name="Target">The type of target that will receive the effects of the attack.</param>
/// <param name="Type">The elemental type of this move.</param>
[PokeApiResource("move")]
public record Move(
    int Id,
    string Name,
    int Accuracy,
    int? EffectChance,
    int Pp,
    int Priority,
    int Power,
    ContestComboSets ContestCombos,
    NamedApiResource<ContestType> ContestType,
    ApiResource<ContestEffect> ContestEffect,
    NamedApiResource<MoveDamageClass> DamageClass,
    IEnumerable<VerboseEffect> EffectEntries,
    IEnumerable<AbilityEffectChange> EffectChanges,
    IEnumerable<NamedApiResource<Pokemon>> LearnedByPokemon,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<MoveFlavourText> FlavourTextEntries,
    NamedApiResource<Generation> Generation,
    IEnumerable<MachineVersionDetail> Machines,
    MoveMetaData Meta,
    IEnumerable<NameEntry> Names,
    IEnumerable<PastMoveStatValues> PastValues,
    IEnumerable<MoveStatChange> StatChanges,
    ApiResource<SuperContestEffect> SuperContestEffect,
    NamedApiResource<MoveTarget> Target,
    NamedApiResource<Type> Type);

/// <summary>
/// A detail of normal and super contest combos that require this move.
/// </summary>
/// <param name="Normal">A detail of moves that this move can be used before or after, granting additional appeal points in contests.</param>
/// <param name="Super">A detail of moves this move can be used before or after, granting additional appeal points in super contests.</param>
public record ContestComboSets(
    ContestComboDetail Normal,
    ContestComboDetail Super);

/// <summary>
/// A detail of moves that this move can be used before or after, granting additional appeal points in contests.
/// </summary>
/// <param name="UseBefore">A list of moves to use before this move.</param>
/// <param name="UseAfter">A list of moves to use after this move.</param>
public record ContestComboDetail(
    IEnumerable<NamedApiResource<Move>> UseBefore,
    IEnumerable<NamedApiResource<Move>> UseAfter);

/// <summary>
/// The flavour text of this move listed in different languages.
/// </summary>
/// <param name="FlavourText">The localized flavour text for an api resource in a specific language.</param>
/// <param name="Language">The language this name is in.</param>
/// <param name="VersionGroup">The version group that uses this flavour text.</param>
public record MoveFlavourText(
    [property: JsonPropertyName("flavor_text")]string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup);

/// <summary>
/// Metadata about this move.
/// </summary>
/// <param name="Ailment">The status ailment this move inflicts on its target.</param>
/// <param name="Category">The category of move this move falls under, e.g. damage or ailment.</param>
/// <param name="MinHits">The minimum number of times this move hits. Null if it always only hits once.</param>
/// <param name="MaxHits">The maximum number of times this move hits. Null if it always only hits once.</param>
/// <param name="MinTurns">The minimum number of turns this move continues to take effect. Null if it always only lasts one turn.</param>
/// <param name="MaxTurns">The maximum number of turns this move continues to take effect. Null if it always only lasts one turn.</param>
/// <param name="Drain">HP drain (if positive) or Recoil damage (if negative), in percentage of damage done.</param>
/// <param name="Healing">The amount of hp gained by the attacking Pokémon, in percentage of it's maximum HP.</param>
/// <param name="CritRate">Critical hit rate bonus.</param>
/// <param name="AilmentChance">The likelihood this attack will cause an ailment.</param>
/// <param name="FlinchChance">The likelihood this attack will cause the target Pokémon to flinch.</param>
/// <param name="StatChance">The likelihood this attack will cause a stat change in the target Pokémon.</param>
public record MoveMetaData(
    NamedApiResource<MoveAilment> Ailment,
    NamedApiResource<MoveCategory> Category,
    int? MinHits,
    int? MaxHits,
    int? MinTurns,
    int? MaxTurns,
    int Drain,
    int Healing,
    int CritRate,
    int AilmentChance,
    int FlinchChance,
    int StatChance);

/// <summary>
/// A list of stats this moves affects and how much it affects them.
/// </summary>
/// <param name="Change">The amount of change.</param>
/// <param name="Stat">The stat being affected.</param>
public record MoveStatChange(
    int Change,
    NamedApiResource<Stat> Stat);

/// <summary>
/// A list of move resource value changes across version groups of the game.
/// </summary>
/// <param name="Accuracy">The percentage value of how likely this move is to be successful.</param>
/// <param name="EffectChance">The percentage value of how likely it is this moves effect will take effect.</param>
/// <param name="Power">The base power of this move with a value of 0 if it does not have a base power.</param>
/// <param name="Pp">Power points. The number of times this move can be used.</param>
/// <param name="EffectEntries">The effect of this move listed in different languages.</param>
/// <param name="Type">The elemental type of this move.</param>
/// <param name="VersionGroup">The version group in which these move stat values were in effect.</param>
public record PastMoveStatValues(
    int? Accuracy,
    int? EffectChance,
    int Power,
    int? Pp,
    IEnumerable<VerboseEffect> EffectEntries,
    NamedApiResource<Type> Type,
    NamedApiResource<VersionGroup> VersionGroup);