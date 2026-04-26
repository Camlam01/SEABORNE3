using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EnchantedCutlassCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;
    public EnchantedCutlassCard() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(25m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await SeaborneCardTools.ApplyImbued(ModifyGemStacks(Upgraded ? 3 : 2));
        await ApplyGemWetIfAny();
    }
}
