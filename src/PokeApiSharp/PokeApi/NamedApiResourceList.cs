namespace PokeApiSharp.PokeApi;

public record NamedApiResourceList<T>(
    int Count,
    string? Next,
    string? Previous,
    IReadOnlyList<NamedApiResource<T>> Results
);