using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class WaterborneCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public WaterborneCard() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer(WetCardPower.Id, ModifyGemStacks(1));
        await SeaborneCardTools.GainWaterwall(ModifyGemStacks(Upgraded ? 12 : 8));
    }
}
