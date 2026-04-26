using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class FishCard : SeaborneCard
{
    public override bool HasCastOrReel => true;
    public FishCard() : base(0, CardType.Skill, CardRarity.Basic, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 2 : 1));
    }
}
