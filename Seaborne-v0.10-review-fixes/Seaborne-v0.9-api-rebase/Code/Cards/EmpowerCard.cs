using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EmpowerCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public EmpowerCard() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.ApplyTrance(cardPlay.Target!, ModifyGemStacks(Upgraded ? 6 : 5));
        await SeabornePowerTools.ApplyPowerToTarget(cardPlay.Target!, "Strength", ModifyGemStacks(3));
    }
}
