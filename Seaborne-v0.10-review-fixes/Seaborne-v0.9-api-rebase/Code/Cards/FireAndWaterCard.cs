using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class FireAndWaterCard : SeaborneCard
{
    public override bool HasAttackDamage => true;

    public FireAndWaterCard() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(Upgraded ? SapphireGemPower.Id : RubyGemPower.Id);
        await DamageCmd.Attack(ModifyGemDamage(5m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await BlockCmd.GainBlock(5m).FromCard(this).Execute(choiceContext);
        await ApplyGemWetIfAny();
    }
}
