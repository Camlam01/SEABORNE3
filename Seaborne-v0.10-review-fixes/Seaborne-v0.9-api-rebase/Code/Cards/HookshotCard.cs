using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class HookshotCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public HookshotCard() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.FireCannon(choiceContext, cardPlay, this, reloadFired: true);
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
