using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Moves;

[PokeApiResource("move-learn-method")]
public record MoveLearnMethod(
    int Id,
    string Name,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<NameEntry> Names,
    IEnumerable<NamedApiResource<VersionGroup>> VersionGroups);