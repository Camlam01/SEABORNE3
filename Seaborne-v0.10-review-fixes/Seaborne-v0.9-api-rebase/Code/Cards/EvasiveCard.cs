using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EvasiveCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public EvasiveCard() : base(1, CardType.Power, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer(EvasivePower.Id, ModifyGemStacks(Upgraded ? 7 : 5));
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        
    }
}
