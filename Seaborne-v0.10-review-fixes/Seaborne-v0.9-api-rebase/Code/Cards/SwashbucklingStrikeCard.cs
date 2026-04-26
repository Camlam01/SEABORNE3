using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Seaborne.Cards;

public sealed class SwashbucklingStrikeCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    protected override IEnumerable<object> CanonicalVars => DamageVar(15m);

    public SwashbucklingStrikeCard() : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await DamageCmd.Attack(ModifyGemDamage(DynamicVars.Damage.BaseValue)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5m);
    }

    private async Task OnReeled()
    {
        await Task.CompletedTask;
    }
}
