using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class YinAndYangCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;

    public YinAndYangCard() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(Upgraded ? OpalGemPower.Id : DiamondGemPower.Id);

        foreach (object enemy in cardPlay.Targets)
        {
            await DamageCmd.Attack(ModifyGemDamage(8m)).FromCard(this).Targeting(enemy).Execute(choiceContext);
            await SeaborneCardTools.ApplyWeak(enemy, ModifyGemStacks(1));
        }

        await ApplyGemWetIfAny();
    }
}
