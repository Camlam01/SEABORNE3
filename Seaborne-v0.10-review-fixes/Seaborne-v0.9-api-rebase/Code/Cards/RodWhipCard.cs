using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class RodWhipCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasCastOrReel => true;
    public RodWhipCard() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(Upgraded ? 7m : 5m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await SeaborneDiscardTools.Reel(ModifyGemReel(1));
        await SeaborneCardTools.ApplyImbued(1);
        await ApplyGemWetIfAny();
    }
}
