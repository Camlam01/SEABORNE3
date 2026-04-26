using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class FullyLoadedSkillCard : SeaborneCard
{
    public override bool HasCastOrReel => true;
    public FullyLoadedSkillCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 3 : 2));
        await SeaborneCardTools.LoadCannonballsFromHand();
    }
}
