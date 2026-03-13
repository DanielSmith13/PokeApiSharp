using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Locations;

[PokeApiResource("location")]
public record Location(
    int Id,
    string Name,
    NamedApiResource<Region> Region,
    IEnumerable<NameEntry> Names,
    IEnumerable<GenerationGameIndex> GameIndices,
    IEnumerable<NamedApiResource<LocationArea>> Areas);