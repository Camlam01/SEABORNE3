using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class TsunamiCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;
    public TsunamiCard() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        int amount = Upgraded ? 25 : 20;
        await DamageCmd.Attack(ModifyGemDamage(amount)).FromCard(this).Targeting(cardPlay.Target!).Execute(choiceContext);
        await SeaborneCardTools.GainWaterwall(ModifyGemStacks(amount));
    }
}
