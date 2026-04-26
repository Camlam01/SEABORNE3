using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using BaseLib.Utils;
using Seaborne.Characters;
using Seaborne.Utils;

namespace Seaborne.Relics;

[Pool(typeof(SeaborneRelicPool))]
public sealed class PlaceholderRelic06HookedAnchorRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    public string Name => "Placeholder Relic 06";
    public string Description => "At the start of each turn, gain 2 Cast.";
    public override string PackedIconPath => "res://Assets/relic_placeholder.png";
    protected override string PackedIconOutlinePath => PackedIconPath;
    protected override string BigIconPath => PackedIconPath;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await SeaborneDiscardTools.AddCast(2);
    }
}
