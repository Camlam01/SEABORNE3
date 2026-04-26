using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class XMarksTheSpotCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;

    public XMarksTheSpotCard() : base(1, CardType.Power, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(DiamondGemPower.Id);
        await SeabornePowerTools.ApplyPowerToPlayer("Seaborne:XMarksTheSpot", ModifyGemStacks(1));
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        Cost = 0;
    }
}
