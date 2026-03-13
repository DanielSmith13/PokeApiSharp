using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("stat")]
public record Stat(
    int Id,
    string Name,
    int GameIndex,
    bool IsBattleOnly,
    MoveStatEffectSet AffectingMoves,
    NatureStatEffectSet AffectingNatures,
    IEnumerable<NamedApiResource<Characteristic>> Characteristics,
    NamedApiResource<MoveDamageClass> MoveDamageClass,
    IEnumerable<NameEntry> Names
);

public record MoveStatEffectSet(
    IEnumerable<MoveStatAffect> Increase,
    IEnumerable<MoveStatAffect> Decrease
);

public record MoveStatAffect(
    int Change,
    NamedApiResource<Move> Move
);

public record NatureStatEffectSet(
    IEnumerable<NamedApiResource<Nature>> Increase,
    IEnumerable<NamedApiResource<Nature>> Decrease
);