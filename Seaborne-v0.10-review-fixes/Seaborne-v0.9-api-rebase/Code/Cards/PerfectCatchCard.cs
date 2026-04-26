using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Systems;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class PerfectCatchCard : SeaborneCard
{
    public override bool HasCastOrReel => true;

    public PerfectCatchCard() : base(2, CardType.Skill, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 3 : 2));
        SeaborneGemSystem.RechargeAll();
        await ApplyGemWetIfAny();
    }
}
