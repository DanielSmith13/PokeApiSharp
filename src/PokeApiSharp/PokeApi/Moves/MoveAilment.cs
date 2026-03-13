using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-ailment")]
public record MoveAilment(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names);