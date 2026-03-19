using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Move Ailments are status conditions caused by moves used during battle.
/// See <see href="https://bulbapedia.bulbagarden.net/wiki/Status_condition">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Moves">A list of moves that cause this ailment.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("move-ailment")]
public record MoveAilment(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names);