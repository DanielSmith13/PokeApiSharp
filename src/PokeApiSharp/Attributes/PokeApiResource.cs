using System;

namespace PokeApiSharp.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PokeApiResource(string path) : Attribute
{
    public string Path { get; } = path;
}