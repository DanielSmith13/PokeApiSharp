using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Utility;

public record Language(
    int Id,
    string Name,
    bool Official,
    string Iso639,
    string Iso3166,
    IEnumerable<NameEntry> Names
    );