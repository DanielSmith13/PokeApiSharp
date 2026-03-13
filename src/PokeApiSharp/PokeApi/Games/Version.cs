using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Games;

[PokeApiResource("version")]
public record Version(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    NamedApiResource<VersionGroup> VersionGroup);