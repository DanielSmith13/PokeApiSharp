using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-category")]
public record MoveCategory(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<DescriptionEntry> Descriptions);