using PokeApiSharp.Interfaces;

namespace PokeApiSharp.PokeApi;

public record NamedApiResource<T>(string Name, string Url) : INamedApiResource;