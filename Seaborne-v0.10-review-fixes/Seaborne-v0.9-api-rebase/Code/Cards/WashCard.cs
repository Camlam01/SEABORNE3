using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Systems;

namespace Seaborne.Cards;

public sealed class WashCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;

    public WashCard() : base(-1, CardType.Skill, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        SeaborneGemSystem.RechargeAll();
        await ApplyGemWetIfAny();
    }
}
