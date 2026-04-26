using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class AncientCurseCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public AncientCurseCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        int stacks = Upgraded ? 2 : 1;
        foreach (object enemy in cardPlay.Targets)
        {
            await SeaborneCardTools.ApplyVulnerable(enemy, ModifyGemStacks(stacks));
            await SeaborneCardTools.ApplyWeak(enemy, ModifyGemStacks(stacks));
        }
    }
}
