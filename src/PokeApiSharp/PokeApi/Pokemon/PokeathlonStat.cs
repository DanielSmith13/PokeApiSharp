using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("pokeathlon-stat")]
public record PokeathlonStat(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    NaturePokeathlonStatAffectSet AffectingNatures
);

public record NaturePokeathlonStatAffectSet(
    IEnumerable<NaturePokeathlonStatAffect> Increase,
    IEnumerable<NaturePokeathlonStatAffect> Decrease
);

public record NaturePokeathlonStatAffect(
    int MaxChange,
    NamedApiResource<Nature> Nature
);