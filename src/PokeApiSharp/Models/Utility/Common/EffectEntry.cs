namespace PokeApiSharp;

/// <summary>
/// Represents a localised effect entry for an API resource.
/// </summary>
/// <param name="Effect">The localised effect text for an API resource in a specific language.</param>
/// <param name="Language">The language this effect is in.</param>
public record EffectEntry(
    string Effect,
    NamedApiResource<Language> Language
);