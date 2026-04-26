using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using Seaborne.Cards;
using Seaborne.Relics;

namespace Seaborne.Characters;

public sealed class TheSeaborneCharacter : CustomCharacterModel
{
    public const string CharacterId = "TheSeaborne";
    public static readonly Color Color = new("1b7f9c");

    public override Color NameColor => Color;
    public override Color MapDrawingColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 75;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<FishCard>(),
        ModelDb.Card<FireCannonCard>(),
        ModelDb.Card<SplashingStrikeCard>(),
        ModelDb.Card<RodRamCard>(),
        ModelDb.Card<WhiplashCard>(),
        ModelDb.Card<PullThroughCard>(),
        ModelDb.Card<SirenSongCard>(),
        ModelDb.Card<TackleCard>(),
        ModelDb.Card<TackleCard>(),
        ModelDb.Card<RiptideCard>(),
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<TrustyFishingRodRelic>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<SeaborneCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<SeaborneRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<SeabornePotionPool>();

    public override List<string> GetArchitectAttackVfx() =>
    [
        "vfx/vfx_attack_slash",
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_bloody_impact",
    ];

    public override string CustomVisualPath => "res://seaborne/Characters/TheSeaborneCharacter/seaborne_character.tscn";
    public override string CustomTrailPath => "res://scenes/vfx/card_trail_ironclad.tscn";
    public override string CustomIconPath => "res://seaborne/Characters/TheSeaborneCharacter/seaborne_character_icon.tscn";
    public override string CustomIconTexturePath => "res://Assets/seaborne_placeholder.png";
    public override string CustomRestSiteAnimPath => "res://seaborne/Characters/TheSeaborneCharacter/seaborne_character_rest_site.tscn";
    public override string CustomMerchantAnimPath => "res://seaborne/Characters/TheSeaborneCharacter/seaborne_character_merchant.tscn";
    public override string CustomCharacterSelectBg => "res://seaborne/Characters/TheSeaborneCharacter/char_select_bg_seaborne_character.tscn";
    public override string CustomCharacterSelectIconPath => "res://Assets/seaborne_placeholder.png";
    public override string CustomCharacterSelectLockedIconPath => "res://Assets/seaborne_placeholder.png";
    public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/ironclad_transition_mat.tres";
    public override string CustomMapMarkerPath => "res://Assets/seaborne_placeholder.png";
    public override string? CustomEnergyCounterPath => "res://scenes/combat/energy_counters/ironclad_energy_counter.tscn";
}

public sealed class SeaborneCardPool : CustomCardPoolModel
{
    public override string Title => TheSeaborneCharacter.CharacterId;
    public override float H => 0.52f;
    public override float S => 0.75f;
    public override float V => 0.85f;
    public override Color DeckEntryCardColor => TheSeaborneCharacter.Color;
    public override bool IsColorless => false;
    public override string? BigEnergyIconPath => null;
    public override string? TextEnergyIconPath => null;
}

public sealed class SeaborneRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => TheSeaborneCharacter.CharacterId;
}

public sealed class SeabornePotionPool : CustomPotionPoolModel
{
    public override string EnergyColorName => TheSeaborneCharacter.CharacterId;
}
