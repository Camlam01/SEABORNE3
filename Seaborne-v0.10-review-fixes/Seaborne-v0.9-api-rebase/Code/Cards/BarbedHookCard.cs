using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class BarbedHookCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public BarbedHookCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer("Seaborne:BarbedHook", ModifyGemStacks(1));
    }

    private async Task OnReeled()
    {
        await SeabornePowerTools.ApplyPowerToPlayer("Strength", ModifyGemStacks(1));
    }
}
