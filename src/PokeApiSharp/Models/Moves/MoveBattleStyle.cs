using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Styles of moves when used in the Battle Palace.
/// See <see href="https://bulbapedia.bulbagarden.net/wiki/Battle_Frontier_(Generation_III)">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("move-battle-style")]
public record MoveBattleStyle(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names);