namespace PokeApiSharp.PokeApi.Utility.Common;

public record EffectEntry(
    string Effect,
    NamedApiResource<Language> Language
);