namespace PokeApiSharp.PokeApi.Utility.Common;

public record VerboseEffect(
    string Effect,
    string ShortEffect,
    NamedApiResource<Language> Language
);