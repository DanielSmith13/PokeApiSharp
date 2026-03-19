namespace PokeApiSharp;

/// <summary>
/// Represents a paginated list of named API resources, commonly used in the PokeAPI
/// to provide a structured way to access collections of resources. This record includes
/// metadata about the total count of resources, pagination links for navigating through
/// the list, and a collection of results, each being a reference to a specific resource.
/// </summary>
/// <param name="Count">The total number of resources available in the list.</param>
/// <param name="Next">The URL to the next page of results, if available.</param>
/// <param name="Previous">The URL to the previous page of results, if available.</param>
/// <param name="Results">A collection of references to specific resources.</param>
/// <typeparam name="T">The type of the resource that this reference points to, for example, a specific Pokémon species or move.</typeparam>
public record NamedApiResourceList<T>(
    int Count,
    string? Next,
    string? Previous,
    IReadOnlyList<NamedApiResource<T>> Results
);