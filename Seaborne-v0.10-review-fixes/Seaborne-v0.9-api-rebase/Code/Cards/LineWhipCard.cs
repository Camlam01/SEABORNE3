using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class LineWhipCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    protected override IEnumerable<object> CanonicalVars => DamageVar(5m);

    public LineWhipCard() : base(2, CardType.Attack, CardRarity.Common, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(2));
        await DamageCmd.Attack(ModifyGemDamage(DynamicVars.Damage.BaseValue)).FromCard(this).Targeting(cardPlay.Targets).Execute(choiceContext);
        foreach (object enemy in cardPlay.Targets)
        {
            await SeabornePowerTools.ApplyPowerToTarget(enemy, "Vulnerable", Upgraded ? 2 : 1);
        }
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
