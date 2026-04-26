using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EmeraldEbbCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public EmeraldEbbCard() : base(2, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(EmeraldGemPower.Id);
        await SeaborneCardTools.GainWaterwall(ModifyGemStacks(Upgraded ? 12 : 8));
    }
}
