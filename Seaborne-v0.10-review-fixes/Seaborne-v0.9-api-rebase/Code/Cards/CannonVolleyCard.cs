using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class CannonVolleyCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public CannonVolleyCard() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.FireCannon(choiceContext, cardPlay, this, false, 0.2m);
        await ApplyGemWetIfAny();
    }
}
