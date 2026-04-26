using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EntrancingMelodyCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public EntrancingMelodyCard() : base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        int repeats = Upgraded ? 3 : 2;

        for (int index = 0; index < repeats; index++)
        {
            await SeaborneCardTools.ApplyTrance(cardPlay.Target!, ModifyGemStacks(3));
        }
    }
}
