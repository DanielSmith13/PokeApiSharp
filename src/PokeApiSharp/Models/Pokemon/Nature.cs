using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Natures influence how a Pokémon's stats grow.
/// See <see href="https://bulbapedia.bulbagarden.net/wiki/Nature">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="IncreasedStat">The stat decreased by 10% in Pokémon of this nature.</param>
/// <param name="DecreasedStat">The stat increased by 10% in Pokémon of this nature.</param>
/// <param name="HatesFlavour">The flavour hated by Pokémon with this nature.</param>
/// <param name="LikesFlavour">The flavour liked by Pokémon with this nature.</param>
/// <param name="PokeathlonStatChanges">
/// A list of Pokéathlon stats this nature effects and how much it affects them.
/// </param>
/// <param name="MoveBattleStylePreferences">
/// A list of battle styles and how likely a Pokémon with this nature
/// is to use them in the Battle Palace or Battle Tent.
/// </param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("nature")]
public record Nature(
    int Id,
    string Name,
    NamedApiResource<Stat>? IncreasedStat,
    NamedApiResource<Stat>? DecreasedStat,
    [property: JsonPropertyName("hates_flavor")]NamedApiResource<BerryFlavour>? HatesFlavour,
    [property: JsonPropertyName("likes_flavor")]NamedApiResource<BerryFlavour>? LikesFlavour,
    IEnumerable<NatureStatChange> PokeathlonStatChanges,
    IEnumerable<MoveBattleStylePreference> MoveBattleStylePreferences,
    IEnumerable<NameEntry> Names
);

/// <summary>
/// A Pokéathlon stat and the amount of change in it caused by nature.
/// </summary>
/// <param name="MaxChange">The amount of change.</param>
/// <param name="PokeathlonStat">The stat being affected.</param>
public record NatureStatChange(
    int MaxChange,
    NamedApiResource<PokeathlonStat> PokeathlonStat
);

/// <summary>
/// A move battle style preference and the chance of using it in the Battle Palace or Battle Tent.
/// </summary>
/// <param name="LowHpPreference">Chance of using the move, in percent, if HP is under one half.</param>
/// <param name="HighHpPreference">Chance of using the move, in percent, if HP is over one half.</param>
/// <param name="MoveBattleStyle">The move battle style.</param>
public record MoveBattleStylePreference(
    int LowHpPreference,
    int HighHpPreference,
    NamedApiResource<MoveBattleStyle> MoveBattleStyle
);