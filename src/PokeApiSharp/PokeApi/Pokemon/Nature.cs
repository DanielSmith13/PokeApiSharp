using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Berries;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("nature")]
public record Nature(
    int Id,
    string Name,
    NamedApiResource<Stat> IncreasedStat,
    NamedApiResource<Stat> DecreasedStat,
    NamedApiResource<BerryFlavour> HatesFlavour,
    NamedApiResource<BerryFlavour> LikesFlavour,
    IEnumerable<NatureStatChange> PokeathlonStatChanges,
    IEnumerable<MoveBattleStylePreference> MoveBattleStylePreferences,
    IEnumerable<NameEntry> Names
);

public record NatureStatChange(
    int MaxChange,
    NamedApiResource<PokeathlonStat> PokeathlonStat
);

public record MoveBattleStylePreference(
    int LowHpPreference,
    int HighHpPreference,
    NamedApiResource<MoveBattleStyle> MoveBattleStyle
);