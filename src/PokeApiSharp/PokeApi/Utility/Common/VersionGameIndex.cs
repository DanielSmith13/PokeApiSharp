using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record VersionGameIndex(
    int GameIndex,
    NamedApiResource<Version> Version
);