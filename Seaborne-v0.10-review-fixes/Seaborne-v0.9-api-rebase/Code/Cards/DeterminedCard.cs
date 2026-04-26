using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class DeterminedCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public DeterminedCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.ApplyPowerToAllEnemies("Strength", ModifyGemStacks(3));
        await SeaborneCardTools.GainSlippery(ModifyGemStacks(1));
        await ApplyGemWetIfAny();
    }
}
