using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class MagicMeltCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;
    public MagicMeltCard() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(5m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await BlockCmd.GainBlock(5m).FromCard(this).Execute(choiceContext);
        await SeaborneCardTools.ApplyImbued(ModifyGemStacks(Upgraded ? 4 : 3));
        await ApplyGemWetIfAny();
    }
}
