using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-target")]
public record MoveTarget(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names);