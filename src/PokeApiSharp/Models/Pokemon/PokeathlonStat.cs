using PokeApiSharp.Attributes;

namespace PokeApiSharp;


/// <summary>
/// Pokeathlon Stats are different attributes of a Pokémon's performance in Pokéathlons.
/// In Pokéathlons, competitions happen on different courses; one for each of the
/// different Pokéathlon stats. See
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9athlon">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="AffectingNatures">A detail of natures which affect this Pokéathlon stat positively or negatively.</param>
[PokeApiResource("pokeathlon-stat")]
public record PokeathlonStat(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    NaturePokeathlonStatAffectSet AffectingNatures
);

/// <summary>
/// A detail of natures which affect this Pokéathlon stat positively or negatively.
/// </summary>
/// <param name="Increase">A list of natures and how they change the referenced Pokéathlon stat.</param>
/// <param name="Decrease">A list of natures and how they change the referenced Pokéathlon stat.</param>
public record NaturePokeathlonStatAffectSet(
    IEnumerable<NaturePokeathlonStatAffect> Increase,
    IEnumerable<NaturePokeathlonStatAffect> Decrease
);

/// <summary>
/// A detail of a nature and how it changes the referenced Pokéathlon stat.
/// </summary>
/// <param name="MaxChange">The maximum amount of change to the referenced Pokéathlon stat.</param>
/// <param name="Nature">The nature causing the change.</param>
public record NaturePokeathlonStatAffect(
    int MaxChange,
    NamedApiResource<Nature> Nature
);