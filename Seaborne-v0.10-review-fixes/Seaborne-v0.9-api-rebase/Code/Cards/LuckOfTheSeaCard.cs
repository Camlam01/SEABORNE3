using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class LuckOfTheSeaCard : SeaborneCard
{
    public override bool HasCastOrReel => true;
    public LuckOfTheSeaCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(2));
        await SeaborneCardTools.AcquireGem(Upgraded ? DiamondGemPower.Id : OpalGemPower.Id);
        await ApplyGemWetIfAny();
    }
}
