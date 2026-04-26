using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class HarpoonCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasCastOrReel => true;
    public HarpoonCard() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(15m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await SeaborneDiscardTools.AddCast(ModifyGemCast(6));
        await SeaborneDiscardTools.Reel(ModifyGemReel(6));
        await ApplyGemWetIfAny();
    }
}
