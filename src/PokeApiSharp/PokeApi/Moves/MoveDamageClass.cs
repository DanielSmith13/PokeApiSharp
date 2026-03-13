using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-damage-class")]
public record MoveDamageClass(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NamedApiResource<Move>> Moves,
    IEnumerable<NameEntry> Names
);