using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using BaseLib.Utils;
using Seaborne.Characters;
using Seaborne.Utils;

namespace Seaborne.Relics;

[Pool(typeof(SeaborneRelicPool))]
public sealed class PlaceholderRelic01GemBarnacleRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public string Name => "Placeholder Relic 01";
    public string Description => "At the start of each turn, gain 1 Gem Slot this combat.";
    public override string PackedIconPath => "res://Assets/relic_placeholder.png";
    protected override string PackedIconOutlinePath => PackedIconPath;
    protected override string BigIconPath => PackedIconPath;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await SeaborneCardTools.GainGemSlot(1);
    }
}
