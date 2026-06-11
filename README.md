# PokeApiSharp

![Build and Test](https://github.com/DanielSmith13/PokeApiSharp/actions/workflows/dotnet.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/PokeApiSharp.svg)](https://www.nuget.org/packages/PokeApiSharp)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight, modern .NET wrapper for the [PokéAPI](https://pokeapi.co/), providing a simple and efficient way to consume Pokémon data in your .NET applications. Targets **net8.0**, **net9.0**, and **net10.0**.

## Features

- **Full PokéAPI Coverage**: Access all resources including Pokémon, Berries, Items, Moves, and more.
- **Strongly Typed Models**: Clean, record-based models using snake_case to PascalCase mapping.
- **Built-in Caching**: Integrated memory caching to optimize performance and respect PokéAPI's rate limits.
- **Async First**: All API interactions are fully asynchronous.
- **Lightweight**: Minimal dependencies, leveraging `System.Text.Json` and `Microsoft.Extensions.Caching.Memory`.
- **Flexible**: Easy to use with or without Dependency Injection.
- **Unmapped Property Detection**: Integrated warning logging to detect when PokéAPI returns properties not yet mapped in the models.

## Installation

Install the package via NuGet:

```bash
dotnet add package PokeApiSharp
```

## Quick Start

### Basic Usage

You can start using the client immediately with default settings:

```csharp
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp;

// Recommended: construct the client using an IHttpClientFactory (this example
// uses a small DI container to create the factory and cache used by the client).
var services = new ServiceCollection();
services.AddMemoryCache();
services.AddHttpClient("PokeApiSharp", c => c.BaseAddress = new Uri("https://pokeapi.co/api/v2/"));

using var provider = services.BuildServiceProvider();
var factory = provider.GetRequiredService<IHttpClientFactory>();
var cache = provider.GetRequiredService<IMemoryCache>();

using var client = new PokeApiClient(factory, "PokeApiSharp", cache);

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

Register an `IHttpClientFactory` and the named `HttpClient` that `PokeApiClient` will use. Then register an `IPokeApiClient` implementation that is constructed with the factory:

```csharp
// Program.cs / Startup.cs
builder.Services.AddMemoryCache();

// Configure a named HttpClient that the PokeApiClient will use
builder.Services.AddHttpClient("PokeApiSharp", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});

// Register the IPokeApiClient and construct it using the IHttpClientFactory
builder.Services.AddSingleton<IPokeApiClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var cache = sp.GetRequiredService<IMemoryCache>();
    var logger = sp.GetService<ILogger<PokeApiClient>>();
    return new PokeApiClient(factory, "PokeApiSharp", cache, logger);
});
```

Then inject `IPokeApiClient` into your services as usual:

```csharp
public class PokemonService
{
    private readonly IPokeApiClient _pokeApiClient;
    public PokemonService(IPokeApiClient pokeApiClient) => _pokeApiClient = pokeApiClient;

    public async Task<string?> GetPokemonTypeAsync(string name)
    {
        var pokemon = await _pokeApiClient.GetAsync<Pokemon>(name);
        return pokemon?.Types.FirstOrDefault()?.Type.Name;
    }
}
```

### Caching

By default, `PokeApiClient` creates its own internal memory cache with a one-hour TTL. You can supply your own cache instance and/or customise the TTL:

```csharp
// Custom cache instance
IMemoryCache myCache = new MemoryCache(new MemoryCacheOptions());
using var client = new PokeApiClient(cache: myCache);

// Custom TTL (e.g. 30 minutes)
using var client = new PokeApiClient(cacheDuration: TimeSpan.FromMinutes(30));

// Manual cache invalidation
client.ClearCache();
```

### Logging

The `PokeApiClient` can optionally accept an `ILogger<PokeApiClient>` to log warnings when it encounters JSON properties from the PokéAPI that are not mapped to the library's models. This is useful for identifying when the API has been updated with new data that the library isn't yet capturing.

```csharp
ILogger<PokeApiClient> logger = loggerFactory.CreateLogger<PokeApiClient>();
using var client = new PokeApiClient(logger: logger);
```

If a logger is provided, any unmapped properties will be logged as warnings, including the resource type and the JSON paths of the missing properties.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
*Powered by [PokéAPI](https://pokeapi.co/)*
