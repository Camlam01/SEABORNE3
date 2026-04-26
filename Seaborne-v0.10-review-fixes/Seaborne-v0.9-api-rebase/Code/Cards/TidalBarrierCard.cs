using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class TidalBarrierCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public TidalBarrierCard() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        int waves = Upgraded ? 4 : 3;
        for (int index = 0; index < waves; index++)
        {
            await SeaborneCardTools.GainWaterwall(ModifyGemStacks(4));
        }
    }
}
