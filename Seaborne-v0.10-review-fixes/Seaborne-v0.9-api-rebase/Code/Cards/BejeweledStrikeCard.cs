using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class BejeweledStrikeCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;

    public BejeweledStrikeCard() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(Upgraded ? 15m : 10m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await SeaborneCardTools.GainGemSlot(ModifyGemStacks(1));
        await SeaborneCardTools.AcquireGem(RubyGemPower.Id);
        await ApplyGemWetIfAny();
    }
}
