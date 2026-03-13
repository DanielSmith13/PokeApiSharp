using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-battle-style")]
public record MoveBattleStyle(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names);