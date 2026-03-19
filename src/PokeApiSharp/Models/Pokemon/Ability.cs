using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Abilities provide passive effects for Pokémon in battle or in the overworld.
/// Pokémon have multiple possible abilities but can have only one ability at a time.
/// Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Ability">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="IsMainSeries">Whether this ability originated in the main series of the video games.</param>
/// <param name="Generation">The generation this ability originated in.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="EffectEntries">The effect of this ability listed in different languages.</param>
/// <param name="EffectChanges">The list of previous effects this ability has had across version groups.</param>
/// <param name="FlavourTextEntries">The flavour text of this ability listed in different languages.</param>
/// <param name="Pokemon">A list of Pokémon that could potentially have this ability.</param>
[PokeApiResource("ability")]
public record Ability(
    int Id,
    string Name,
    bool IsMainSeries,
    NamedApiResource<Generation> Generation,
    IEnumerable<NameEntry> Names,
    IEnumerable<VerboseEffect> EffectEntries,
    IEnumerable<AbilityEffectChange> EffectChanges,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<AbilityFlavourText> FlavourTextEntries,
    IEnumerable<AbilityPokemon> Pokemon
);

/// <summary>
/// The effect of this ability listed in different languages.
/// </summary>
/// <param name="EffectEntries">The previous effect of this ability listed in different languages.</param>
/// <param name="VersionGroup">The version group in which the previous effect of this ability originated.</param>
public record AbilityEffectChange(
    IEnumerable<EffectEntry> EffectEntries,
    NamedApiResource<VersionGroup> VersionGroup
);

/// <summary>
/// The flavour text of this ability listed in different languages.
/// </summary>
/// <param name="FlavourText">The localized name for an API resource in a specific language.</param>
/// <param name="Language">The language this text resource is in.</param>
/// <param name="VersionGroup">The version group that uses this flavour text.</param>
public record AbilityFlavourText(
    [property: JsonPropertyName("flavor_text")]string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup
);

/// <summary>
/// 
/// </summary>
/// <param name="IsHidden">Whether this a hidden ability for the referenced Pokémon.</param>
/// <param name="Slot">
/// Pokémon have 3 ability 'slots' which hold references to possible abilities
/// they could have. This is the slot of this ability for the referenced Pokémon.
/// </param>
/// <param name="Pokemon">The Pokémon this ability could belong to.</param>
public record AbilityPokemon(
    bool IsHidden,
    int Slot,
    NamedApiResource<Pokemon> Pokemon
);