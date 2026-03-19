namespace PokeApiSharp;

/// <summary>
/// Represents a detailed effect entry for an API resource in a specific language.
/// </summary>
/// <param name="Effect">The localised effect text for an API resource in a specific language.</param>
/// <param name="ShortEffect">The localised effect text in brief.</param>
/// <param name="Language">The language this effect is in.</param>
public record VerboseEffect(
    string Effect,
    string ShortEffect,
    NamedApiResource<Language> Language
);