using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Seaborne.Cards;

public sealed class TackleCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    protected override IEnumerable<object> CanonicalVars => DamageVar(3m);

    public TackleCard() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        int hits = Upgraded ? 3 : 2;
        for (int i = 0; i < hits; i++)
        {
            await DamageCmd.Attack(ModifyGemDamage(DynamicVars.Damage.BaseValue)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        }
    }
}
