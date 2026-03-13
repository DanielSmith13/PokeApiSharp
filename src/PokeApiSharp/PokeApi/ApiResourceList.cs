namespace PokeApiSharp.PokeApi;

public record ApiResourceList<T>(
    int Count,
    string? Next,
    string? Previous,
    IReadOnlyList<ApiResource<T>> Results
);