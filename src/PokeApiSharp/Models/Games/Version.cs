using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Versions of the games, e.g. Red, Blue, or Yellow.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="VersionGroup">The version group this version belongs to.</param>
[PokeApiResource("version")]
public record Version(
    int Id,
    string Name,
    IEnumerable<NameEntry> Names,
    NamedApiResource<VersionGroup> VersionGroup);