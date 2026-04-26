using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class SplashingStrikeCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    protected override IEnumerable<object> CanonicalVars => DamageVar(5m);

    public SplashingStrikeCard() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.AddCast(ModifyGemCast(1));
        await DamageCmd.Attack(ModifyGemDamage(DynamicVars.Damage.BaseValue)).FromCard(this).Targeting(cardPlay.Targets).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(0m);
    }

    private async Task OnReeled()
    {
        await SeaborneDiscardTools.AddCast(ModifyGemCast(Upgraded ? 2 : 1));
    }
}
