namespace PokeApiSharp;

/// <summary>
/// Represents a paginated list of API resources returned by the PokeAPI.
/// This record contains metadata about the total count of resources,
/// as well as links to the next and previous pages of results,
/// along with the actual list of resources.
/// </summary>
/// <param name="Count"></param>
/// <param name="Next"></param>
/// <param name="Previous"></param>
/// <param name="Results"></param>
/// <typeparam name="T"></typeparam>
public record ApiResourceList<T>(
    int Count,
    string? Next,
    string? Previous,
    IReadOnlyList<ApiResource<T>> Results
);