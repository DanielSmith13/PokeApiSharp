# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-04-03

### Added
- Initial release of PokeApiSharp.
- `IPokeApiClient` and `PokeApiClient` providing full coverage of the PokéAPI v2 endpoints.
- Support for fetching resources by name, ID, `ApiResource<T>`, and `NamedApiResource<T>`.
- `ListAsync<T>` for paginated resource listings.
- `GetAsync<T>()` (no parameters) for automatically paginating through all resources of a type.
- `GetPokemonLocationAreasAsync` for the `/pokemon/{id}/encounters` endpoint.
- Built-in `IMemoryCache`-backed caching with a configurable TTL (`cacheDuration`, defaulting to one hour).
- Unmapped JSON property detection via `ILogger<PokeApiClient>` warnings to surface API schema drift.
- `IPokeApiClient` extends `IDisposable`; `PokeApiClient` disposes its owned `HttpClient` and cache.
- Multi-target support: `net8.0`, `net9.0`, and `net10.0`.
- Symbol package (`.snupkg`) published alongside the main package for source debugging.
- Strongly typed, record-based models with snake_case → PascalCase mapping via `System.Text.Json`.
- `Either<TSuccess, TFailure>` monad used internally for cache hit/miss signalling.
