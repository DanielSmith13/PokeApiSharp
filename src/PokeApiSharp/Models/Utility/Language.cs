using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Languages for translations of API resource information.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Official">Whether the games are published in this language.</param>
/// <param name="Iso639">The two-letter code of the country where this language is spoken. Note that it is not unique.</param>
/// <param name="Iso3166">The two-letter code of the language. Note that it is not unique.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("language")]
public record Language(
    int Id,
    string Name,
    bool Official,
    string Iso639,
    string Iso3166,
    IEnumerable<NameEntry> Names
    );