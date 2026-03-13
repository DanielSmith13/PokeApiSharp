namespace PokeApiSharp.PokeApi.Utility.Common;

public record NameEntry(
    string Name,
    NamedApiResource<Language> Language
);