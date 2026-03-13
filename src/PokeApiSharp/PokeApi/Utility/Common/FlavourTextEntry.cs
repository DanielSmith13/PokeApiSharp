using PokeApiSharp.PokeApi.Games;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record FlavourTextEntry(
    string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<Version> Version
);