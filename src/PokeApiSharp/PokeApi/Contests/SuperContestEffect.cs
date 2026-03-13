using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Contests;

[PokeApiResource("super-contest-effect")]
public record SuperContestEffect(
    int Id,
    int Appeal,
    IEnumerable<FlavourTextEntry> FlavourTextEntries,
    IEnumerable<NamedApiResource<Move>> Moves);