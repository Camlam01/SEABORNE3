using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class FireCannonCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public FireCannonCard() : base(2, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.FireCannon(choiceContext, cardPlay, this);
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
