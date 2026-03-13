using PokeApiSharp.Attributes;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("gender")]
public record Gender(
    int Id,
    string Name,
    IEnumerable<PokemonSpeciesGender> PokemonSpeciesDetails,
    IEnumerable<PokemonSpecies> RequiredForEvolution
);

public record PokemonSpeciesGender(
    int Rate,
    NamedApiResource<PokemonSpecies> PokemonSpecies
);