using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Some Pokémon may appear in one of multiple, visually different forms.
/// These differences are purely cosmetic. For variations within a Pokémon
/// species, which do differ in more than just visuals, the 'Pokémon' entity
/// is used to represent such a variety.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Order">
/// The order in which forms should be sorted within all forms.
/// Multiple forms may have equal order, in which case they should
/// fall back on sorting by name.
/// </param>
/// <param name="FormOrder">The order in which forms should be sorted within a species' forms.</param>
/// <param name="IsDefault">True for exactly one form used as the default for each Pokémon.</param>
/// <param name="IsBattleOnly">Whether this form can only happen during battle.</param>
/// <param name="IsMega">Whether this form requires mega evolution.</param>
/// <param name="FormName">The name of this form.</param>
/// <param name="Pokemon">The Pokémon that can take on this form.</param>
/// <param name="Types">A list of details showing types this Pokémon form has.</param>
/// <param name="Sprites">A set of sprites used to depict this Pokémon form in the game.</param>
/// <param name="VersionGroup">The version group this Pokémon form was introduced in.</param>
/// <param name="Names">The form specific full name of this Pokémon form, or empty if the form does not have a specific name.</param>
/// <param name="FormNames">The form-specific form name of this Pokémon form, or empty if the form does not have a specific name.</param>
[PokeApiResource("pokemon-form")]
public record PokemonForm(
    int Id,
    string Name,
    int Order,
    int FormOrder,
    bool IsDefault,
    bool IsBattleOnly,
    bool IsMega,
    string FormName,
    NamedApiResource<Pokemon> Pokemon,
    IEnumerable<PokemonFormType> Types,
    PokemonSprites Sprites,
    NamedApiResource<VersionGroup> VersionGroup,
    IEnumerable<NameEntry> Names,
    IEnumerable<NameEntry> FormNames
);

/// <summary>
/// A representation of a Pokémon's type in a specific form.
/// </summary>
/// <param name="Slot">The order the Pokémon's types are listed in.</param>
/// <param name="Type">The type the referenced Form has.</param>
public record PokemonFormType(
    int Slot,
    NamedApiResource<PokemonType> Type
);