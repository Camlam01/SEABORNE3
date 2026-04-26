using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using BaseLib.Utils;
using Seaborne.Characters;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Relics;

[Pool(typeof(SeaborneRelicPool))]
public sealed class TrustyFishingRodRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    public string Name => "Trusty Fishing Rod";
    public string Description => "At the start of each turn, gain 1 Cast.";
    public override string PackedIconPath => "res://Assets/relic_fishing_rod_placeholder.png";
    protected override string PackedIconOutlinePath => PackedIconPath;
    protected override string BigIconPath => PackedIconPath;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(CastPower.Id, 1);
    }
}
