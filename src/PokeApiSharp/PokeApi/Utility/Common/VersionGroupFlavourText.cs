using PokeApiSharp.PokeApi.Games;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record VersionGroupFlavourText(
    string Text,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup);