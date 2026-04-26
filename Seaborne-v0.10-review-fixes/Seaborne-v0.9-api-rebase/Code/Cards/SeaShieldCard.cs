using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Seaborne.Cards;

public sealed class SeaShieldCard : SeaborneCard
{
    public SeaShieldCard() : base(1, CardType.Skill, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await BlockCmd.GainBlock(Upgraded ? 15 : 12).FromCard(this).Execute(choiceContext);
    }

    private Task OnReeled()
    {
        return Task.CompletedTask;
    }
}
