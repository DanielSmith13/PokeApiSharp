# PokeApiSharp

![Build and Test](https://github.com/DanielSmith13/PokeApiSharp/actions/workflows/dotnet.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/PokeApiSharp.svg)](https://www.nuget.org/packages/PokeApiSharp)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight, modern .NET 10 wrapper for the [PokéAPI](https://pokeapi.co/), providing a simple and efficient way to consume Pokémon data in your .NET applications.

## Features

- **Full PokéAPI Coverage**: Access all resources including Pokémon, Berries, Items, Moves, and more.
- **Strongly Typed Models**: Clean, record-based models using snake_case to PascalCase mapping.
- **Built-in Caching**: Integrated memory caching to optimize performance and respect PokéAPI's rate limits.
- **Async First**: All API interactions are fully asynchronous.
- **Lightweight**: Minimal dependencies, leveraging `System.Text.Json` and `Microsoft.Extensions.Caching.Memory`.
- **Flexible**: Easy to use with or without Dependency Injection.

## Installation

Install the package via NuGet:

```bash
dotnet add package PokeApiSharp
```

## Quick Start

### Basic Usage

You can start using the client immediately with default settings:

```csharp
using PokeApiSharp;

// Initialize the client
using var client = new PokeApiClient();

// Fetch a Pokémon by name
var pikachu = await client.GetAsync<Pokemon>("pikachu");
Console.WriteLine($"Name: {pikachu?.Name}, Weight: {pikachu?.Weight}");

// Fetch a Pokémon by ID
var mew = await client.GetAsync<Pokemon>(151);
```

### Listing Resources

You can list resources with pagination:

```csharp
// List the first 20 Pokémon
var pokemonList = await client.ListAsync<Pokemon>(limit: 20, offset: 0);

foreach (var resource in pokemonList.Results)
{
    Console.WriteLine($"Found: {resource.Name} at {resource.Url}");
}
```

### Fetching All Resources

The library provides a way to fetch all resources of a specific type, handling pagination automatically:

```csharp
// Warning: This will make many API calls for large resource sets
IEnumerable<Pokemon?> allPokemon = await client.GetAsync<Pokemon>();

foreach (var p in allPokemon)
{
    Console.WriteLine(p?.Name);
}
```

## Advanced Usage

### Dependency Injection

Register `PokeApiClient` in your `Program.cs` or `Startup.cs`:

```csharp
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IPokeApiClient, PokeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
```

Then inject it into your services:

```csharp
public class PokemonService(IPokeApiClient pokeApiClient)
{
    public async Task<string?> GetPokemonTypeAsync(string name)
    {
        var pokemon = await pokeApiClient.GetAsync<Pokemon>(name);
        return pokemon?.Types.FirstOrDefault()?.Type.Name;
    }
}
```

### Caching

By default, `PokeApiClient` creates its own internal memory cache. If you want to share a cache or configure it:

```csharp
IMemoryCache myCache = new MemoryCache(new MemoryCacheOptions());
using var client = new PokeApiClient(cache: myCache);

// Manual cache management
client.ClearCache();
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
*Powered by [PokéAPI](https://pokeapi.co/)*
