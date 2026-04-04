using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Stats determine certain aspects of battles.
/// Each Pokémon has a value for each stat which
/// grows as they gain levels and can be altered
/// momentarily by effects in battles.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="GameIndex">ID the games use for this stat.</param>
/// <param name="IsBattleOnly">Whether this stat only exists within a battle.</param>
/// <param name="AffectingMoves">A detail of moves which affect this stat positively or negatively.</param>
/// <param name="AffectingNatures">A detail of natures which affect this stat positively or negatively.</param>
/// <param name="AffectingItems">A list of items which affect this stat positively or negatively.</param>
/// <param name="Characteristics">A list of characteristics that are set on a Pokémon when its highest base stat is this stat.</param>
/// <param name="MoveDamageClass">The class of damage this stat is directly related to.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("stat")]
public record Stat(
    int Id,
    string Name,
    int GameIndex,
    bool IsBattleOnly,
    MoveStatEffectSet AffectingMoves,
    NatureStatEffectSet AffectingNatures,
    IEnumerable<NamedApiResource<Item>> AffectingItems,
    IEnumerable<NamedApiResource<Characteristic>> Characteristics,
    NamedApiResource<MoveDamageClass> MoveDamageClass,
    IEnumerable<NameEntry> Names
);

/// <summary>
/// A set of moves that affect a stat positively or negatively.
/// </summary>
/// <param name="Increase">A list of moves and how they increase the referenced stat.</param>
/// <param name="Decrease">A list of moves and how they decrease the referenced stat.</param>
public record MoveStatEffectSet(
    IEnumerable<MoveStatAffect> Increase,
    IEnumerable<MoveStatAffect> Decrease
);

/// <summary>
/// A move that affects a stat positively or negatively.
/// </summary>
/// <param name="Change">The maximum amount of change to the referenced stat.</param>
/// <param name="Move">The move causing the change.</param>
public record MoveStatAffect(
    int Change,
    NamedApiResource<Move> Move
);


/// <summary>
/// A set of natures that affect a stat positively or negatively.
/// </summary>
/// <param name="Increase">A list of natures and how they increase the referenced stat.</param>
/// <param name="Decrease">A list of natures and how they decrease the referenced stat.</param>
public record NatureStatEffectSet(
    IEnumerable<NamedApiResource<Nature>> Increase,
    IEnumerable<NamedApiResource<Nature>> Decrease
);