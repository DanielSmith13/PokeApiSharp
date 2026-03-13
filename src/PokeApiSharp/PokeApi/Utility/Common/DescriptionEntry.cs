namespace PokeApiSharp.PokeApi.Utility.Common;

public record DescriptionEntry(
    string Description,
    NamedApiResource<Language> Language
);