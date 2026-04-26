using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class SirenSongCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public SirenSongCard() : base(2, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToTarget(cardPlay.Target!, "Seaborne:Trance", Upgraded ? 3 : 2);
    }
}
