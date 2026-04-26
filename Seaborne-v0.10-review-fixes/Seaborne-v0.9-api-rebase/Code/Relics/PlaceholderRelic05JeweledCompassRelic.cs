using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using BaseLib.Utils;
using Seaborne.Characters;
using Seaborne.Systems;

namespace Seaborne.Relics;

[Pool(typeof(SeaborneRelicPool))]
public sealed class PlaceholderRelic05JeweledCompassRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    public string Name => "Placeholder Relic 05";
    public string Description => "At the start of each turn, recharge your Gems.";
    public override string PackedIconPath => "res://Assets/relic_placeholder.png";
    protected override string PackedIconOutlinePath => PackedIconPath;
    protected override string BigIconPath => PackedIconPath;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        SeaborneGemSystem.RechargeAll();
        await Task.CompletedTask;
    }
}
