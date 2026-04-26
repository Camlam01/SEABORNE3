using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class MurkyWaterCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public MurkyWaterCard() : base(2, CardType.Skill, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.GainWaterwall(ModifyGemStacks(Upgraded ? 30 : 25));
    }
}
