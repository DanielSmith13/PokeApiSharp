namespace PokeApiSharp.Interfaces;

public interface INamedApiResource
{
    string Name { get; }
    string Url { get; }
}