using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class MucusMaskCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public MucusMaskCard() : base(1, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.GainSlippery(Upgraded ? 2 : 1);
    }
}
