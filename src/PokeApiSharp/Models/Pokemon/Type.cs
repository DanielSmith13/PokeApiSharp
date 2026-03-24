using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Types are properties for Pokémon and their moves.
/// Each type has three properties: which types of Pokémon
/// it is super effective against, which types of Pokémon
/// it is not very effective against, and which types of Pokémon
/// it is completely ineffective against.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="DamageRelations">A detail of how effective this type is toward others and vice versa.</param>
/// <param name="PastDamageRelations">
/// A list of details of how effective this type was
/// toward others and vice versa in previous generations
/// </param>
/// <param name="GameIndices">A list of game indices relevant to this item by generation.</param>
/// <param name="Generation">The generation that this type was introduced in.</param>
/// <param name="MoveDamageClass">The class of damage inflicted by this type.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="Pokemon">A list of details of Pokémon that have this type.</param>
/// <param name="Moves">A list of moves that have this type.</param>
/// <param name="Sprites">A set of sprites used to depict this type in the game.</param>
[PokeApiResource("type")]
public record Type(
    int Id,
    string Name,
    TypeRelation DamageRelations,
    IEnumerable<TypeRelationsPast> PastDamageRelations,
    IEnumerable<GenerationGameIndex> GameIndices,
    NamedApiResource<Generation> Generation,
    NamedApiResource<MoveDamageClass> MoveDamageClass,
    IEnumerable<NameEntry> Names,
    IEnumerable<TypePokemon> Pokemon,
    IEnumerable<NamedApiResource<Move>> Moves,
    TypeSprites Sprites);

/// <summary>
/// A set of sprites used to depict this type in the game.
/// </summary>
/// <param name="GenerationIII">Sprites used in Generation III.</param>
/// <param name="GenerationIV">Sprites used in Generation IV.</param>
/// <param name="GenerationIX">Sprites used in Generation IX.</param>
/// <param name="GenerationV">Sprites used in Generation V.</param>
/// <param name="GenerationVI">Sprites used in Generation VI.</param>
/// <param name="GenerationVII">Sprites used in Generation VII.</param>
/// <param name="GenerationVIII">Sprites used in Generation VIII.</param>
public record TypeSprites(
    [property: JsonPropertyName("generation-iii")] TypeSpritesGenerationIII GenerationIII,
    [property: JsonPropertyName("generation-iv")] TypeSpritesGenerationIV GenerationIV,
    [property: JsonPropertyName("generation-ix")] TypeSpritesGenerationIX GenerationIX,
    [property: JsonPropertyName("generation-v")] TypeSpritesGenerationV GenerationV,
    [property: JsonPropertyName("generation-vi")] TypeSpritesGenerationVI GenerationVI,
    [property: JsonPropertyName("generation-vii")] TypeSpritesGenerationVII GenerationVII,
    [property: JsonPropertyName("generation-viii")] TypeSpritesGenerationVIII GenerationVIII
);

/// <summary>
/// Sprites for a specific game version.
/// </summary>
/// <param name="NameIcon">A URL pointing to the name icon for this type.</param>
/// <param name="SymbolIcon">A URL pointing to the symbol icon for this type.</param>
public record TypeSpritesGame(
    [property: JsonPropertyName("name_icon")] string? NameIcon,
    [property: JsonPropertyName("symbol_icon")] string? SymbolIcon
);

/// <summary>
/// Sprites used in Generation III.
/// </summary>
/// <param name="Colosseum">Sprites used in Pokémon Colosseum.</param>
/// <param name="Emerald">Sprites used in Pokémon Emerald.</param>
/// <param name="FireRedLeafGreen">Sprites used in Pokémon FireRed and LeafGreen.</param>
/// <param name="RubySapphire">Sprites used in Pokémon Ruby and Sapphire.</param>
/// <param name="Xd">Sprites used in Pokémon XD: Gale of Darkness.</param>
public record TypeSpritesGenerationIII(
    TypeSpritesGame Colosseum,
    TypeSpritesGame Emerald,
    [property: JsonPropertyName("firered-leafgreen")] TypeSpritesGame FireRedLeafGreen,
    [property: JsonPropertyName("ruby-sapphire")] TypeSpritesGame RubySapphire,
    TypeSpritesGame Xd
);

/// <summary>
/// Sprites used in Generation IV.
/// </summary>
/// <param name="DiamondPearl">Sprites used in Pokémon Diamond and Pearl.</param>
/// <param name="HeartGoldSoulSilver">Sprites used in Pokémon HeartGold and SoulSilver.</param>
/// <param name="Platinum">Sprites used in Pokémon Platinum.</param>
public record TypeSpritesGenerationIV(
    [property: JsonPropertyName("diamond-pearl")] TypeSpritesGame DiamondPearl,
    [property: JsonPropertyName("heartgold-soulsilver")] TypeSpritesGame HeartGoldSoulSilver,
    TypeSpritesGame Platinum
);

/// <summary>
/// Sprites used in Generation IX.
/// </summary>
/// <param name="ScarletViolet">Sprites used in Pokémon Scarlet and Violet.</param>
public record TypeSpritesGenerationIX(
    [property: JsonPropertyName("scarlet-violet")] TypeSpritesGame ScarletViolet
);

/// <summary>
/// Sprites used in Generation V.
/// </summary>
/// <param name="Black2White2">Sprites used in Pokémon Black 2 and White 2.</param>
/// <param name="BlackWhite">Sprites used in Pokémon Black and White.</param>
public record TypeSpritesGenerationV(
    [property: JsonPropertyName("black-2-white-2")] TypeSpritesGame Black2White2,
    [property: JsonPropertyName("black-white")] TypeSpritesGame BlackWhite
);

/// <summary>
/// Sprites used in Generation VI.
/// </summary>
/// <param name="OmegaRubyAlphaSapphire">Sprites used in Pokémon Omega Ruby and Alpha Sapphire.</param>
/// <param name="XY">Sprites used in Pokémon X and Y.</param>
public record TypeSpritesGenerationVI(
    [property: JsonPropertyName("omega-ruby-alpha-sapphire")] TypeSpritesGame OmegaRubyAlphaSapphire,
    [property: JsonPropertyName("x-y")] TypeSpritesGame XY
);

/// <summary>
/// Sprites used in Generation VII.
/// </summary>
/// <param name="LetsGoPikachuLetsGoEevee">Sprites used in Pokémon Let's Go, Pikachu! and Let's Go, Eevee!.</param>
/// <param name="SunMoon">Sprites used in Pokémon Sun and Moon.</param>
/// <param name="UltraSunUltraMoon">Sprites used in Pokémon Ultra Sun and Ultra Moon.</param>
public record TypeSpritesGenerationVII(
    [property: JsonPropertyName("lets-go-pikachu-lets-go-eevee")] TypeSpritesGame LetsGoPikachuLetsGoEevee,
    [property: JsonPropertyName("sun-moon")] TypeSpritesGame SunMoon,
    [property: JsonPropertyName("ultra-sun-ultra-moon")] TypeSpritesGame UltraSunUltraMoon
);

/// <summary>
/// Sprites used in Generation VIII.
/// </summary>
/// <param name="BrilliantDiamondShiningPearl">Sprites used in Pokémon Brilliant Diamond and Shining Pearl.</param>
/// <param name="LegendsArceus">Sprites used in Pokémon Legends: Arceus.</param>
/// <param name="SwordShield">Sprites used in Pokémon Sword and Shield.</param>
public record TypeSpritesGenerationVIII(
    [property: JsonPropertyName("brilliant-diamond-shining-pearl")] TypeSpritesGame BrilliantDiamondShiningPearl,
    [property: JsonPropertyName("legends-arceus")] TypeSpritesGame LegendsArceus,
    [property: JsonPropertyName("sword-shield")] TypeSpritesGame SwordShield
);

/// <summary>
/// A reference to Pokémon that have the referenced type.
/// </summary>
/// <param name="Slot">The order the Pokémon's types are listed in.</param>
/// <param name="Pokemon">The Pokémon that has the referenced type.</param>
public record TypePokemon(
    int Slot,
    NamedApiResource<Pokemon> Pokemon
);

/// <summary>
/// A detail of how effective this type is toward others and vice versa.
/// </summary>
/// <param name="NoDamageTo">A list of types this type has no effect on.</param>
/// <param name="HalfDamageTo">A list of types this type is not very effective against.</param>
/// <param name="DoubleDamageTo">A list of types this type is very effective against.</param>
/// <param name="NoDamageFrom">A list of types that have no effect against this type.</param>
/// <param name="HalfDamageFrom">A list of types that are not very effective against this type.</param>
/// <param name="DoubleDamageFrom">A list of types that are very effective against this type.</param>
public record TypeRelation(
    IEnumerable<NamedApiResource<Type>> NoDamageTo,
    IEnumerable<NamedApiResource<Type>> HalfDamageTo,
    IEnumerable<NamedApiResource<Type>> DoubleDamageTo,
    IEnumerable<NamedApiResource<Type>> NoDamageFrom,
    IEnumerable<NamedApiResource<Type>> HalfDamageFrom,
    IEnumerable<NamedApiResource<Type>> DoubleDamageFrom
);

/// <summary>
/// A detail of how effective this type was toward
/// others and vice versa in previous generations.
/// </summary>
/// <param name="Generation">The last generation in which the referenced type had the listed damage relations</param>
/// <param name="DamageRelations">The damage relations the referenced type had up to and including the listed generation</param>
public record TypeRelationsPast(
    NamedApiResource<Generation> Generation,
    TypeRelation DamageRelations
);