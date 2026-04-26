using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Seaborne.Cards;

public sealed class TideTearCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    protected override IEnumerable<object> CanonicalVars => DamageVar(8m);

    public TideTearCard() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(DynamicVars.Damage.BaseValue)).FromCard(this).Targeting(cardPlay.Targets).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(0m);
    }
}
