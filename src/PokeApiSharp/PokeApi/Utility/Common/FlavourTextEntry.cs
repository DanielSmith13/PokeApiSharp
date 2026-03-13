using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record FlavourTextEntry(
    string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<Version> Version
);